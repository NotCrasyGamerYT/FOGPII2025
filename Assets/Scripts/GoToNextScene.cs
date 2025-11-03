using UnityEngine;

public class GoToNextScene : MonoBehaviour
{
    private NPC currentNPC;

    public GameObject interactionPrompt;

    private void Start()
    {
        if (interactionPrompt != null)
        {
            interactionPrompt.SetActive(false);
        }
    }

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
        if (currentNPC != null && Input.GetKeyDown(KeyCode.R))
        {
            currentNPC.ShowPrompt(false);

            UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex + 1);


        }
    }
    
    public void ShowPrompt(bool show)
    {
        if (interactionPrompt != null)
        {
            interactionPrompt.SetActive(show);
        }
    }
}
