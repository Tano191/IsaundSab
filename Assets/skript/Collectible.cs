using System;
using UnityEngine;

public class Collectible : MonoBehaviour
{
    public static event Action OnCollected;
    public static int total;
    public AudioClip collectSound;

    [Header("Rotation")]
    public bool enableRotation = true;
    public Vector3 rotationSpeed = new Vector3(0f, 100f, 0f);

    [Header("Bounce")]
    public bool enableBounce = true;
    public float amplitude = 0.5f;
    public float frequency = 2f;

    private float startY;

    void Awake() => total++;

    void Start()
    {
        startY = transform.position.y;
    }

    void Update()
    {
        if (enableRotation)
        {
            transform.Rotate(rotationSpeed * Time.deltaTime);
        }

        if (enableBounce)
        {
            float newY = startY + Mathf.Sin(Time.time * frequency) * amplitude;
            transform.position = new Vector3(transform.position.x, newY, transform.position.z);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            AudioSource.PlayClipAtPoint(collectSound, transform.position);
            OnCollected?.Invoke();
            Destroy(gameObject);
        }
    }
}