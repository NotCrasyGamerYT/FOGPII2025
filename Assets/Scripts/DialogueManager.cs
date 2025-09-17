using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    // --- UI References ---
    public TextMeshProUGUI speakerNameText;
    public TextMeshProUGUI dialogueText;
    public GameObject dialoguePanel; // The entire dialogue box panel

    // --- Dialogue Data ---
    private Queue<Dialogue> lines; // A queue to hold the current conversation lines

    // --- Singleton Pattern ---
    // This makes the DialogueManager easily accessible from other scripts
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

        // Get the next line from the queue
        Dialogue currentLine = lines.Dequeue();

        // Update the UI elements
        speakerNameText.text = currentLine.speaker;
        dialogueText.text = currentLine.sentence;
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
