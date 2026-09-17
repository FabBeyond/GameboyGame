using System.Collections.Generic;
using UnityEngine;

public class ShipEnemy : MonoBehaviour
{
    public float seeRadius;
    public float speed;
    public float resistance;
    public float weight;
    public GameObject player;

    void Start()
    {
        player = GameObject.FindWithTag("Player");
        Dictionary<string, float> stats = GameManager.instance.CalculateShipValues(gameObject);
        speed = stats["speed"];
        resistance = stats["resistance"];
        weight = stats["weight"];
    }

    void Update()
    {
        float distance = Vector2.Distance(transform.position, player.transform.position);
        if (distance <= seeRadius)
        {
            Vector2 direction = (player.transform.position - transform.position).normalized;
            direction = ControlUtils.SnapToDir(direction, 8);

            transform.position += (Vector3)direction * speed * Time.deltaTime;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, seeRadius);
    }
}
