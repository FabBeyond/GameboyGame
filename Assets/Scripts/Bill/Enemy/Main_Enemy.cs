using System.Collections;
using UnityEngine;

public class Main_Enemy : MonoBehaviour
{
    [Header("Health")]
    public float maxHp = 5;
    private float hp = 0;

    Rigidbody2D rb;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        hp = maxHp;
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(hp < 1)
        {
            Death();
        }
    }

    public void TakeDmg(float dmg)
    {
        hp -= dmg;
        print("ouchi bouchi");
    }

    public IEnumerator Knockback(Vector3 dir, float distance, float speed)
    {
        Vector3 startPos = transform.position;
        Vector3 targetPos = startPos + dir.normalized * distance;

        while (Vector3.Distance(transform.position, targetPos) > 1)
        {
            rb.linearVelocity = dir.normalized * speed;

            yield return null;
        }

        rb.linearVelocity = Vector3.zero;
    }

    public void Death()
    {
        Destroy(gameObject);
    }
}
