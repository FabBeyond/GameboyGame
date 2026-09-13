using System.Collections;
using UnityEngine;

public class Main_Enemy : MonoBehaviour
{
    [Header("Health")]
    public float maxHp = 5;
    private float hp = 0;

    [Header("Knockback")]
    private bool isKnocbacked = false;
    private Vector3 startPos = Vector3.zero;
    private Vector3 targetPos = Vector3.zero;
    private Vector2 kbDir = Vector2.zero;
    private float kbSpeed = 0;


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
        print(Vector3.Distance(transform.position, targetPos) > 0);
        KnockChecks();
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

    public void AddKnockback(Vector3 dir, float distance, float speed) 
    {
        isKnocbacked = true;
        startPos = transform.position; 
        targetPos = startPos + dir.normalized * distance;
        kbDir = dir;
        kbSpeed = speed;

        rb.linearVelocity = Vector3.zero; 
    }

    public void KnockChecks()
    {
        if(isKnocbacked)
        {
            if (Vector3.Distance(transform.position, targetPos) >= 1)
            {
                rb.linearVelocity = (targetPos - transform.position).normalized * kbSpeed;
            }
            else
            {
                rb.linearVelocity = Vector2.zero;
                isKnocbacked = false;
            }
        }
    }

    public void Death()
    {
        Destroy(gameObject);
    }
}
