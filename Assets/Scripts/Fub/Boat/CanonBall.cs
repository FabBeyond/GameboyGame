using UnityEngine;

public class CanonBall : MonoBehaviour
{
    public Vector2 direction;
    public float speed = 5f;

    void Start()
    {
        
    }

    void Update()
    {
        if (direction == null) return;

        transform.position += (Vector3)direction * speed * Time.deltaTime;
    }
}
