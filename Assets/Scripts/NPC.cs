using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
    public Conversation conversation;

    public List<GameObject> interactionPrompt;

    private void Start()
    {
        if (interactionPrompt != null && interactionPrompt.Count > 0)
        {
            foreach (GameObject prompt in interactionPrompt)
            {
                if (prompt != null)
                {
                    prompt.SetActive(false);
                }
            }
        }
    }
    public void ShowPrompt(bool show)
    {
        if (interactionPrompt != null && interactionPrompt.Count > 0)
        {
            foreach (GameObject prompt in interactionPrompt)
            {
                if (prompt != null)
                {
                    prompt.SetActive(show);
                }
            }
        }
    }
}