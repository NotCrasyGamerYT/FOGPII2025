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

    // --- State Control ---
    private bool isTyping = false;

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

    public void StartDialogue(Conversation conversation)
    {
        dialoguePanel.SetActive(true); 
        lines.Clear();

        foreach (Dialogue line in conversation.lines)
        {
            lines.Enqueue(line);
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
        typingCoroutine = StartCoroutine(TypeSentence(currentLine.sentence));
    }

    IEnumerator TypeSentence(string sentence)
    {
        isTyping = true; // Lock input
        dialogueText.text = ""; 
        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
        isTyping = false; // Unlock input
    }

    void EndDialogue()
    {
        dialoguePanel.SetActive(false);
    }

    void Update()
    {
        // Only advance dialogue if the panel is active, space is pressed, AND we are not currently typing.
        if (dialoguePanel.activeInHierarchy && Input.GetKeyDown(KeyCode.Space) && !isTyping)
        {
            DisplayNextLine();
        }
    }
}