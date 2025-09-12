using UnityEngine;

public class Parallax : MonoBehaviour
{
    public float speed = 0.1f;   // movement speed for this layer
    private Vector3 startPos;
    private float newPos;

    void Start()
    {
        startPos = transform.position;
    }

    void Update()
    {
        // Looping scroll
        newPos = Mathf.Repeat(Time.time * speed, 20); // "20" = reset offset (tweak for your image size)
        transform.position = startPos + Vector3.left * newPos;
    }
}
