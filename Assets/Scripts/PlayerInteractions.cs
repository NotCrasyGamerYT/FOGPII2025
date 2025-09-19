using UnityEngine;
using UnityEngine.UI;

public class PlayerInteractions : MonoBehaviour
{
    public Button talkButton;
    private NPC currentNPC;

    void Start()
    {
        talkButton.gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        NPC npc = other.GetComponent<NPC>();
        if (npc != null)
        {
            currentNPC = npc;
            talkButton.gameObject.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.GetComponent<NPC>() == currentNPC)
        {
            currentNPC = null;
            talkButton.gameObject.SetActive(false);
        }
    }

    public void OnTalkButtonPressed()
    {
        if (currentNPC != null)
        {
            DialogueManager.instance.StartDialogue(currentNPC.conversation);
            talkButton.gameObject.SetActive(false);
        }
    }
}
