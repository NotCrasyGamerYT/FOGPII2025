using UnityEngine;
using System.Collections; // Required for Coroutines

public class AutoDialogueStrter : MonoBehaviour
{
    // Drag the Conversation asset you want to start automatically here
    public Conversation conversationToStart;

    // This function is called when the object is created, after Awake()
    void Start()
    {
        // Start a coroutine to ensure the DialogueManager is ready
        StartCoroutine(StartDialogueRoutine());
    }

    private IEnumerator StartDialogueRoutine()
    {
        // Wait for the end of the first frame
        // This gives the DialogueManager's Awake() method time to run
        yield return new WaitForEndOfFrame();

        if (conversationToStart != null && DialogueManager.instance != null)
        {
            DialogueManager.instance.StartDialogue(conversationToStart);
        }
        else
        {
            Debug.LogWarning("AutoDialogueStarter: No Conversation or DialogueManager found!");
        }
    }

}