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

        if (leftPortraitImage != null) leftPortraitImage.gameObject.SetActive(false);
        if (rightPortraitImage != null) rightPortraitImage.gameObject.SetActive(false);
        if (generalImagePlaceholder != null) generalImagePlaceholder.gameObject.SetActive(false);

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

        if (currentLine.portrait != null)
        {
            if (currentLine.location == PortraitLocation.Left)
            {
                if (leftPortraitImage != null)
                {
                    leftPortraitImage.sprite = currentLine.portrait;
                    leftPortraitImage.gameObject.SetActive(true);
                }
                if (rightPortraitImage != null)
                {
                    rightPortraitImage.gameObject.SetActive(false);
                }
            }
            else // It's PortraitLocation.Right
            {
                if (rightPortraitImage != null)
                {
                    rightPortraitImage.sprite = currentLine.portrait;
                    rightPortraitImage.gameObject.SetActive(true);
                }
                if (leftPortraitImage != null)
                {
                    leftPortraitImage.gameObject.SetActive(false);
                }
            }
        }
        else 
        {
            if (leftPortraitImage != null) leftPortraitImage.gameObject.SetActive(false);
            if (rightPortraitImage != null) rightPortraitImage.gameObject.SetActive(false);
        }
        
        if (currentLine.portrait != null)
        {
            if (generalImagePlaceholder != null)
            {
                generalImagePlaceholder.sprite = currentLine.portrait;
                generalImagePlaceholder.gameObject.SetActive(true);
            }
        }
        else
        {
            if (generalImagePlaceholder != null) generalImagePlaceholder.gameObject.SetActive(false);
        }

        typingCoroutine = StartCoroutine(TypeSentence(currentLine.sentence));
    }

    IEnumerator TypeSentence(string sentence)
    {
        isTyping = true; 
        dialogueText.text = ""; 
        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
        isTyping = false;
    }

    void EndDialogue()
    {
        dialoguePanel.SetActive(false);

        if (leftPortraitImage != null) leftPortraitImage.gameObject.SetActive(false);
        if (rightPortraitImage != null) rightPortraitImage.gameObject.SetActive(false);
        if (generalImagePlaceholder != null) generalImagePlaceholder.gameObject.SetActive(false);
    }

    void Update()
    {
        if (dialoguePanel.activeInHierarchy && Input.GetKeyDown(KeyCode.Space) && !isTyping)
        {
            DisplayNextLine();
        }
    }
}