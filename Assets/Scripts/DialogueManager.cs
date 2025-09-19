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
    private Queue<Dialogue> lines;
    public static DialogueManager instance;

    private void Awake()
    {
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

    // Call this method from another script to start a conversation
    public void StartDialogue(Conversation conversation)
    {
        dialoguePanel.SetActive(true); // Show the dialogue box
        lines.Clear(); // Clear any previous conversation

        // Add all lines from the conversation asset to our queue
        foreach (Dialogue line in conversation.lines)
        {
            lines.Enqueue(line);
        }

        DisplayNextLine();
    }

    // Displays the next line in the queue
    public void DisplayNextLine()
    {
        // If there are no more lines, end the dialogue
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

        // Set the speaker name once
        speakerNameText.text = currentLine.speaker;

        // Start the typewriter effect
        typingCoroutine = StartCoroutine(TypeSentence(currentLine.sentence));
    }

    IEnumerator TypeSentence(string sentence)
    {
        dialogueText.text = ""; // Clear the text box
        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
    }

    // Hides the dialogue box
    void EndDialogue()
    {
        dialoguePanel.SetActive(false);
    }

    // Check for player input to advance dialogue
    void Update()
    {
        // Only check for input if the dialogue panel is active
        if (dialoguePanel.activeInHierarchy && Input.GetKeyDown(KeyCode.Space))
        {
            DisplayNextLine();
        }
    }
}
