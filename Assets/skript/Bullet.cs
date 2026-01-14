using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float damage = 5;
    public float lifeTime = 5;

    private void Update()
    {
        lifeTime -= Time.deltaTime;

        if (lifeTime < 0)
            Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other);
        if (other.GetComponent<Enemy>() != null)
            other.GetComponent<Enemy>().health -= damage;

        


        Destroy(gameObject);
    }



}
