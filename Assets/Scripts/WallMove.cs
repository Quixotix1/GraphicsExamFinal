using UnityEngine;

public class WallMove : MonoBehaviour
{
    [SerializeField] private Transform wallTransform;
    [SerializeField] private float speed;

    void Update()
    {
        wallTransform.position += new Vector3(Time.deltaTime * speed, 0, 0);
        if (wallTransform.position.x > 10)
        {
            wallTransform.position = new Vector3(-10, wallTransform.position.y, wallTransform.position.z);
        }
    }
}
