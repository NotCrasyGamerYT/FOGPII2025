using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AutoDiaStarter : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
[Tooltip("The NPC whose dialogue will be started automatically.")]
    public NPC targetNPC;
    
    [Tooltip("If true, this component will destroy itself after successfully starting the dialogue.")]
    public bool destroyAfterUse = true;

    void Start()
    {
        // Check for null reference errors before proceeding.
        if (targetNPC == null)
        {
            Debug.LogError("AutoDialogueStarter is missing a reference to the Target NPC in the Inspector!", this);
            return;
        }

        // Start the dialogue using the existing Interact() method on the NPC.
        // We use Interact() because the NPC script handles the initial setup (isDialogueActive = true, etc.).
        targetNPC.Interact();
        
        Debug.Log($"Auto-starting dialogue for: {targetNPC.dialogueData.npcName}");

        // Clean up the trigger component if requested.
        if (destroyAfterUse)
        {
            Destroy(this);
        }
    }
}
