using UnityEngine;

public enum PortraitLocation { Left, Right }

[System.Serializable]
public class Dialogue
{
    public string speaker;
    public string sentence;
    public PortraitLocation location;
    public Sprite portrait;

    // --- New additions for Choices ---
    [Tooltip("Check this box if this line should stop and present choices to the player.")]
    public bool isChoicePoint = false; 
    public Choice[] choices; 
}

// New class to define a single choice
[System.Serializable]
public class Choice
{
    public string choiceText;

    [Tooltip("If checked, selecting this choice will load the next scene.")]
    public bool triggersSceneLoad = false;
}