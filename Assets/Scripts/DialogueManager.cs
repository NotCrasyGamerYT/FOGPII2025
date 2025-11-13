using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    // --- UI References ---
    public TextMeshProUGUI speakerNameText;
    public TextMeshProUGUI dialogueText;
    public float typingSpeed = 0.02f;
    private Coroutine typingCoroutine;
    public GameObject dialoguePanel; 
    public Image leftPortraitImage;
    public Image rightPortraitImage;
    public Image generalImagePlaceholder;
    
    // --- NEW UI References for Choices ---
    public GameObject choicePanel;         // Container for choice buttons
    public GameObject choiceButtonPrefab;  // Prefab for a single choice button


    private Queue<Dialogue> lines;
    public static DialogueManager instance;

    // --- State Control ---
    private bool isTyping = false;
    // New state to prevent spacebar advance while choices are open
    private bool awaitingChoice = false; 

    private void Awake()
    {
        // ... (existing Awake code)
        if (instance == null)
        {
            instance = this;
            lines = new Queue<Dialogue>();
        }
        else
        {
            Destroy(gameObject);
        }
    }
    
public void StartDialogue(Conversation conversation)
{
    dialoguePanel.SetActive(true);

    if (leftPortraitImage != null) leftPortraitImage.gameObject.SetActive(false);
    if (rightPortraitImage != null) rightPortraitImage.gameObject.SetActive(false);
    if (generalImagePlaceholder != null) generalImagePlaceholder.gameObject.SetActive(false);

        lines.Clear();
    
    if (conversation.lines != null)
    {
        foreach (Dialogue line in conversation.lines)
        {
            lines.Enqueue(line);
        }
    }
    
    DisplayNextLine();
}

    public void DisplayNextLine()
    {
        if (lines.Count == 0)
        {
            EndDialogue();
            return;
        }

        Dialogue currentLine = lines.Dequeue();

        if (typingCoroutine != null)
        {
            StopCoroutine(typingCoroutine);
        }

        speakerNameText.text = currentLine.speaker;

        // ... (Portrait/Image handling code remains the same)

        typingCoroutine = StartCoroutine(TypeSentence(currentLine, currentLine.sentence));
    }

    // UPDATED: Now takes the full Dialogue object
    IEnumerator TypeSentence(Dialogue currentLine, string sentence)
    {
        isTyping = true; 
        dialogueText.text = ""; 
        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
        isTyping = false;
        
        if (currentLine.isChoicePoint && currentLine.choices != null && currentLine.choices.Length > 0)
        {
            ShowChoices(currentLine.choices);
        }
    }
    
    private void ShowChoices(Choice[] choices)
    {
        foreach (Transform child in choicePanel.transform)
        {
            Destroy(child.gameObject);
        }

        awaitingChoice = true;
        
        foreach (Choice choice in choices)
        {
            GameObject buttonObject = Instantiate(choiceButtonPrefab, choicePanel.transform);
            ChoiceButton choiceButton = buttonObject.GetComponent<ChoiceButton>();
            
            if (choiceButton != null)
            {
                choiceButton.Setup(this, choice);
            }
        }
    }
    
    public void OnChoiceSelected(Choice selectedChoice)
    {
        foreach (Transform child in choicePanel.transform)
        {
            Destroy(child.gameObject);
        }

        awaitingChoice = false;

        if (selectedChoice.triggersSceneLoad)
        {
            // Automatically loads the next scene when the player selects this option
            EndDialogue(); // Close the dialogue UI
            UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex + 1);
            return;
        }
        DisplayNextLine();
    }

    void EndDialogue()
    {
        dialoguePanel.SetActive(false);
        // ... (existing EndDialogue code to hide portraits)
    }

    // UPDATED: Now checks for the new state 'awaitingChoice'
    void Update()
    {
        if (dialoguePanel.activeInHierarchy && (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0)) && !isTyping && !awaitingChoice)
        {
            DisplayNextLine();
        }
    }
}