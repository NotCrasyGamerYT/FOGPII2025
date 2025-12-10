using UnityEngine;


[System.Serializable]
public struct DialogueChoice
{
    public string choiceText;

    [Tooltip("The index of the dialogueLines array to jump to (e.g., 5). Set to -1 to End Dialogue.")]
    public int nextLineIndex; 
    
    [Tooltip("If checked, the choice will immediately load a new dialogue asset instead of jumping index.")]
    public bool loadNewDialogue; 
    public NPCDialogue newDialogueAsset;
}
[CreateAssetMenu(fileName = "New NPC Dialogue", menuName = "Dialogue/NPC Dialogue")]
public class NPCDialogue : ScriptableObject

{
    public string npcName;
    public Sprite npcPortrait;
    public string[] dialogueLines;
    public DialogueChoice[][] choicesPerLine;
    public float typingSpeed = 0.05f;
    public AudioClip typingSound;
    public float voiceVolume = 1.0f;
    public bool[] autoProgressLines;
    public float autoProgressDelay = 1.5f;
    public AudioClip voiceSound;
    public float voicePitch = 1.0f;

    public class DialogueChoices
    {
        public DialogueChoice[] choices;
    }
    public DialogueChoices[] choices;

}
