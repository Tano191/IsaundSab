using UnityEngine;
using UnityEngine.SceneManagement;

public class RetryButton : MonoBehaviour
{
    // Restart the main game scene
    public void RestartGame()
    {
        SceneManager.LoadScene("Mechanics"); // game scene name
    }
}