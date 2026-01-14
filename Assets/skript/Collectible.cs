using UnityEngine;

public class Collectible : MonoBehaviour
{ 
    // Update is called once per frame
    void Update()
    {
        transform.localRotation = Quaternion.Euler(90f, Time.time * 100f, 0);
    }
}
