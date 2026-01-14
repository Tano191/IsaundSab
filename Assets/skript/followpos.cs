using UnityEngine;

public class followpos : MonoBehaviour
{
    public Transform TargetTransform;
    public Vector3 Offset;


    // Update is called once per frame
    void Update()
    {
        transform.position = TargetTransform.position + Offset;


    }
}
