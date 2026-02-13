using UnityEngine;
using UnityEngine.SceneManagement;

public class GameFinish : MonoBehaviour
{

    public CollectCounter count;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Portal");
        if (count.GetCount() >= Collectible.total)
        {
            SceneManager.LoadScene("Level Finished"); // game scene name
        }
    }
}