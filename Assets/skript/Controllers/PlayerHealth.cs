using UnityEngine;

public class PlayerHealth : MonoBehaviour, IDamageable
{
    [SerializeField] private int maxHealth = 100;
    private int currentHealth;

    public HealthBar healthBar;
    public Transform respawnPoint;

    private CharacterController characterController;
    private Rigidbody rb;
    private bool isRespawning = false;

    private void Awake()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);

        characterController = GetComponent<CharacterController>();
        rb = GetComponent<Rigidbody>();
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        healthBar.SetHealth(currentHealth);

        if (currentHealth <= 0)
            Die();
    }

    private void Die()
    {
        StartCoroutine(Respawn());
    }

    private System.Collections.IEnumerator Respawn()
    {
        isRespawning = true;

        if (characterController != null)
            characterController.enabled = false;

        if (rb != null)
        {
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }

        yield return null; // wait one frame

        transform.SetPositionAndRotation(
            respawnPoint.position,
            respawnPoint.rotation
        );

        if (characterController != null)
            characterController.enabled = true;

        currentHealth = maxHealth;
        healthBar.SetHealth(currentHealth);
    }
}