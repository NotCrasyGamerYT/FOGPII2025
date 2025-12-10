using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public NPC targetNPC; 
    
    public NPCDialogue dialogueToUse; 

    public void Interact()
    {
        if (targetNPC == null || dialogueToUse == null) return;
        
        targetNPC.dialogueData = dialogueToUse; 
        
        targetNPC.Interact();
    }

    public bool CanInteract()
    {
        return targetNPC != null && targetNPC.CanInteract();
    }
}
