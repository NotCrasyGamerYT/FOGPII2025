using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
// Removed: using NUnit.Framework; (Not needed for runtime game logic)

public class NPC : MonoBehaviour, IInteractable
{
    // Existing public fields
    public NPCDialogue dialogueData;
    public GameObject dialoguePanel;
    public TMP_Text dialogueText, nameText;
    public Image portraitImage;
    
    // NEW: Audio References
    [Header("Audio Settings")]
    [Tooltip("The sound played when dialogue starts.")]
    [SerializeField] private AudioClip startDialogueClip;
    
    [Tooltip("The looping sound played while text is typing.")]
    [SerializeField] private AudioClip typingLoopClip;

    // Components
    private AudioSource audioSource; // Used for all non-typing sounds (Start/End)
    private AudioSource typingAudioSource; // Dedicated AudioSource for the looping typing sound

    private int dialogueIndex;
    private bool isTyping, isDialogueActive;

    [Header("Choice UI")]
    [Tooltip("The parent object for the choice buttons.")]
    public GameObject choicePanel; 

    [Tooltip("The button prefab or template used for choices.")]
    public GameObject choiceButtonPrefab;

    void Awake()
    {
        // Get or add the AudioSource components
        AudioSource[] sources = GetComponents<AudioSource>();
        if (sources.Length < 2)
        {
            Debug.LogWarning("NPC needs at least two AudioSource components: one for SFX (index 0) and one for typing loop (index 1). Adding them automatically.", this);
            audioSource = gameObject.AddComponent<AudioSource>();
            audioSource.playOnAwake = false;
            typingAudioSource = gameObject.AddComponent<AudioSource>();
            typingAudioSource.playOnAwake = false;
            typingAudioSource.loop = true;
        }
        else
        {
            audioSource = sources[0];
            typingAudioSource = sources[1];
            typingAudioSource.loop = true;
        }
    }

    public bool CanInteract()
    {
        return !isDialogueActive;
    }

    // --- Interaction ---
    public void Interact()
    {
       if (isDialogueActive)
       {
           NextLine();
       }
       else
       {
           StartDialogue();
       }
    }

    void StartDialogue()
    {
        isDialogueActive = true;
        dialogueIndex = 0;
        dialoguePanel.SetActive(true);
        nameText.SetText(dialogueData.npcName);
        portraitImage.sprite = dialogueData.npcPortrait;
        

        if (audioSource != null && startDialogueClip != null)
        {
            audioSource.PlayOneShot(startDialogueClip);
        }

        StartCoroutine(TypeLine());
    }

    void NextLine()
    {
        if (isTyping)
        {

            StopAllCoroutines();
            dialogueText.SetText(dialogueData.dialogueLines[dialogueIndex]);
            isTyping = false;
            StopTypingSound();
        }
        else if (dialogueIndex < dialogueData.dialogueLines.Length - 1)
        {
            bool hasChoicesDefined = 
        // 1. Check if the top-level choices array exists and is long enough for the current line
        dialogueData.choices != null && 
        dialogueData.choices.Length > dialogueIndex && 
        
        // 2. Check if the specific DialogueChoices wrapper object exists
        dialogueData.choices[dialogueIndex] != null && 
        
        // 3. Check if the inner array of choice options exists and has at least one choice
        dialogueData.choices[dialogueIndex].choices != null &&
        dialogueData.choices[dialogueIndex].choices.Length > 0;
    
    
    if (hasChoicesDefined) // Use the new, safe boolean check
    {
        StopTypingSound();
        ShowChoices(dialogueData.choices[dialogueIndex].choices);
    }
    else
    {
        // No choices, proceed to the next line
        dialogueIndex++;
        StartCoroutine(TypeLine());
    }
    }
    }

    IEnumerator TypeLine()
    {
        isTyping = true;
        dialogueText.SetText("");

        StartTypingSound();

        foreach (char letter in dialogueData.dialogueLines[dialogueIndex])
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(dialogueData.typingSpeed);
            
        }

        StopTypingSound();

        isTyping = false;
        
        if(dialogueData.autoProgressLines.Length > dialogueIndex && dialogueData.autoProgressLines[dialogueIndex])
        {
            yield return new WaitForSeconds(dialogueData.autoProgressDelay);
            NextLine();
        }
    }
    
    public void EndDialogue()
    {
        StopAllCoroutines();
        StopTypingSound();
        isDialogueActive = false;
        
        dialoguePanel.SetActive(false);
        dialogueIndex = 0; 
    }
    

    private void StartTypingSound()
    {
        if (typingAudioSource != null && typingLoopClip != null && !typingAudioSource.isPlaying)
        {
            typingAudioSource.clip = typingLoopClip;
            typingAudioSource.Play();
        }
    }

    private void StopTypingSound()
    {
        if (typingAudioSource != null && typingAudioSource.isPlaying)
        {
            typingAudioSource.Stop();
        }
    }

    void ShowChoices(DialogueChoice[] choices)
    {
        // Clear any existing buttons
        foreach (Transform child in choicePanel.transform)
        {
            Destroy(child.gameObject);
        }
    
        choicePanel.SetActive(true);

        for (int i = 0; i < choices.Length; i++)
        {
            DialogueChoice choice = choices[i];
        
            // Instantiate the button prefab
            GameObject buttonGO = Instantiate(choiceButtonPrefab, choicePanel.transform);
        
            // Set the button's text
            TMP_Text buttonText = buttonGO.GetComponentInChildren<TMP_Text>();
            if (buttonText != null)
            {
                buttonText.SetText(choice.choiceText);
            }

            // Add the click listener
            Button button = buttonGO.GetComponent<Button>();
            if (button != null)
            {
                // Pass the entire choice structure to the handler
                button.onClick.AddListener(() => HandleChoiceSelected(choice));
           }
        }
    }

    public void HandleChoiceSelected(DialogueChoice choice)
    {
        // Hide the choice panel immediately
        choicePanel.SetActive(false);
    
        // Clear out the buttons for the next use
        foreach (Transform child in choicePanel.transform)
        {
            Destroy(child.gameObject);
        }

        // --- Choice Logic ---
        if (choice.loadNewDialogue && choice.newDialogueAsset != null)
        {
            // Load a completely new dialogue script
            dialogueData = choice.newDialogueAsset;
            StartDialogue(); // This resets the index and starts the new script
        }
        else if (choice.nextLineIndex >= 0)
        {
            // Jump to a specific line in the current script
            dialogueIndex = choice.nextLineIndex;
            StartCoroutine(TypeLine());
        }
        else // index is -1 or invalid, meaning End Dialogue
        {
            EndDialogue();
        }
    }
}