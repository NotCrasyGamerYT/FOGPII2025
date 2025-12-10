using UnityEngine;

public class PauseController : MonoBehaviour
{
    public static bool IsGamePaused {get; private set;} = false;
    
    public static void SetPauseState(bool isPaused)
    {
        IsGamePaused = isPaused;
        Time.timeScale = isPaused ? 0f : 1f;
    }
}
