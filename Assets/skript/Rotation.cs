using UnityEngine;

public class RotateAndBounce : MonoBehaviour
{
    [Header("Rotation")]
    public Vector3 rotationSpeed = new Vector3(0f, 100f, 0f);

    [Header("Bounce")]
    public float amplitude = 0.5f;
    public float frequency = 2f;

    private float startY;

    void Start()
    {
        startY = transform.position.y;
    }

    void Update()
    {
        // Rotation
        transform.Rotate(rotationSpeed * Time.deltaTime);

        // Bounce
        float newY = startY + Mathf.Sin(Time.time * frequency) * amplitude;
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);
    }
}