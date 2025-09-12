using UnityEngine;

public class LightningManager : MonoBehaviour
{
    public Animator lightningAnimator; // assign Lightning Animator
   // public AudioSource thunderSound;   // assign thunder audio clip

    public float minDelay = 5f;
    public float maxDelay = 10f;

    private float timer;

    void Start()
    {
        ResetTimer();
    }

    void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0f)
        {
            TriggerLightning();
            ResetTimer();
        }
    }

    void ResetTimer()
    {
        timer = Random.Range(minDelay, maxDelay);
    }

    void TriggerLightning()
    {
        lightningAnimator.Play("LightningFlash");
    //    if (thunderSound != null)
        {
   //         thunderSound.PlayDelayed(0.3f); // thunder after the flash
        }
    }
}
