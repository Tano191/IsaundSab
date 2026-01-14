using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float health;

    // Update is called once per frame
    private void Update()
    {
        if (health <= 0)
            Destroy(gameObject);
    }
}
