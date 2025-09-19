using UnityEngine;

public enum PortraitLocation { Left, Right }

[System.Serializable]
public class Dialogue
{
    public string speaker;
    public string sentence;
    public PortraitLocation location;
    public Sprite portrait;
}
