using System.Collections;
using UnityEngine;
using Packages;
using Pathfinding;
using JetBrains.Annotations;

public class Main_Enemy : MonoBehaviour
{
    [Header("Health")]
    public float maxHp = 5;
    private float hp = 0;

    [Header("Knockback")]
    public float unstuckTime = 0.5f;
    private bool isKnocbacked = false;
    private Vector3 startPos = Vector3.zero;
    private Vector3 targetPos = Vector3.zero;
    private Vector2 kbDir = Vector2.zero;
    private float kbSpeed = 0;
    private float knockbackedtimer = 0;

    [Header("Movement")]
    public float moveSpeed = 6;
    public float runSpeed = 9;
    private float speed = 0;

    AIPath aiPath;
    Rigidbody2D rb;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        hp = maxHp;
        rb = GetComponent<Rigidbody2D>();
        aiPath = GetComponent<AIPath>();
        speed = moveSpeed;
        aiPath.maxSpeed = speed;
    }

    // Update is called once per frame
    void Update()
    {
        KnockChecks();
        if(hp < 1)
        {
            Death();
        }
            aiPath.canMove = !isKnocbacked;
        ControlUtils.SnapToDir(rb.linearVelocity, 8);
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
            if(knockbackedtimer > unstuckTime)
            {
                isKnocbacked = false;
                knockbackedtimer = 0;
            }
            else
            {
                knockbackedtimer += Time.deltaTime;
            }

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

    Vector2 Crunch8Dir(Vector2 vec)
    {
        if (vec == Vector2.zero)
        {
            return Vector2.zero; 
        }

        float angle = Mathf.Atan2(vec.y, vec.x);
        float crunchAngle = Mathf.Round(angle / (Mathf.PI / 4)) * (Mathf.PI / 4);

        return new Vector2(Mathf.Cos(crunchAngle),Mathf.Sin(crunchAngle)) * vec.magnitude;
    }

}
