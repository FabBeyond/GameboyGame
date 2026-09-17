using UnityEngine;

public class CanonBall : MonoBehaviour
{
    public float speed = 5f;

    void Update()
    {
        transform.position += transform.right * speed * Time.deltaTime;
    }
}
