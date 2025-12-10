using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    /// <summary>
    /// Public function to load the next scene in the Build Settings index.
    /// This is designed to be called by a UI Button's OnClick() event.
    /// </summary>
    public void LoadNextScene()
    {
        // 1. Get the index of the current active scene
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        // 2. Calculate the index of the next scene
        int nextSceneIndex = currentSceneIndex + 1;

        // 3. Get the total number of scenes in the Build Settings
        int sceneCount = SceneManager.sceneCountInBuildSettings;

        // 4. Check if the next index is valid
        if (nextSceneIndex < sceneCount)
        {
            // Load the scene by its index
            SceneManager.LoadScene(nextSceneIndex);
            Debug.Log($"Loading next scene: Index {nextSceneIndex}");
        }
        else
        {
            // If the next index is out of bounds, you've reached the last scene.
            Debug.LogWarning("Attempted to load next scene, but current scene is the last in the build list. Returning to first scene (Index 0).");
        }
    }
}

