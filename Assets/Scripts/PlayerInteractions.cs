using UnityEngine;
public class PlayerInteractions : MonoBehaviour
{

    private NPC currentNPC;

    private void OnTriggerEnter2D(Collider2D other)
    {
        NPC npc = other.GetComponent<NPC>();
        if (npc != null)
        {
            currentNPC = npc;
            currentNPC.ShowPrompt(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.GetComponent<NPC>() == currentNPC)
        {
            if (currentNPC != null)
            {
                currentNPC.ShowPrompt(false);
            }
            currentNPC = null;
        }
    }

    void Update()
    {
        if (currentNPC != null && Input.GetKeyDown(KeyCode.E))
        {
            currentNPC.ShowPrompt(false);

            if (DialogueManager.instance != null)
                DialogueManager.instance.StartDialogue(currentNPC.conversation);

        }

        if (currentNPC != null && Input.GetKeyDown(KeyCode.R))
        {
            currentNPC.ShowPrompt(false);

            UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex + 1);
        }
        
    }
}