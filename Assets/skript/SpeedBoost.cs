using UnityEngine;
using TMPro;

public class SpeedBoost : MonoBehaviour
{
    [Header("Settings")]
    public float detectionRadius = 15f;
    public float speedMultiplier = 1.5f; // 1.5x speed boost

    [Header("UI")]
    public TextMeshProUGUI messageText;
    public string boostMessage = "The Nearby RuneStone boosts your speed!";

    private PlayerMovementTutorial player;
    private bool playerInRange = false;
    private bool boostActive = false;

    private float originalWalkSpeed;
    private float originalSprintSpeed;

    private void Start()
    {
        if (PlayerManager.Instance != null && PlayerManager.Instance.PlayerTransform != null)
        {
            player = PlayerManager.Instance.PlayerTransform.GetComponent<PlayerMovementTutorial>();
        }

        if (player == null)
            Debug.LogError("Player or PlayerMovment component not found!");

        // Hide message at start
        if (messageText != null)
            messageText.gameObject.SetActive(false);
    }

    void Update()
    {
        if (player == null) return;

        // Check distance 
        float distance = Vector3.Distance(transform.position, player.transform.position);

        if (distance <= detectionRadius && !playerInRange)
        {
            // Player entered range
            EnterRange();
        }
        else if (distance > detectionRadius && playerInRange)
        {
            // Player left range
            ExitRange();
        }
    }

    void EnterRange()
    {
        playerInRange = true;


        if (messageText != null)
        {
            messageText.text = boostMessage;
            messageText.gameObject.SetActive(true);
        }

        if (!boostActive)
        {
            originalWalkSpeed = player.walkSpeed;
            originalSprintSpeed = player.sprintSpeed;

            player.walkSpeed *= speedMultiplier;
            player.sprintSpeed *= speedMultiplier;

            boostActive = true;
            Debug.Log("✓ Speed boost activated!");
        }
    }

    void ExitRange()
    {
        playerInRange = false;

     
        if (messageText != null)
            messageText.gameObject.SetActive(false);

  
        if (boostActive)
        {
            player.walkSpeed = originalWalkSpeed;
            player.sprintSpeed = originalSprintSpeed;

            boostActive = false;
            Debug.Log("Speed boost deactivated");
        }
    }


    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
}

