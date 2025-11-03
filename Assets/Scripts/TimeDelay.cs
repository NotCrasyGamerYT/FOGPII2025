using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class TimeDelay : MonoBehaviour
{
    [Tooltip("The UI element or other GameObject you want to show.")]
    public GameObject objectToShow;

    [Tooltip("The time in seconds to wait before showing the object.")]
    public float delayInSeconds = 10f;

    void Start()
    {
        if (objectToShow != null)
        {
            objectToShow.SetActive(false);

            StartCoroutine(ShowObjectCoroutine());
        }
        else
        {
            Debug.LogWarning("ShowObjectAfterDelay: No object has been assigned to show.");
        }
    }

    private IEnumerator ShowObjectCoroutine()
    {
        yield return new WaitForSeconds(delayInSeconds);

        if (objectToShow != null)
        {
            objectToShow.SetActive(true);
        }
    }
}