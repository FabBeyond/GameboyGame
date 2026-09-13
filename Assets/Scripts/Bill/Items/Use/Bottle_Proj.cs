using UnityEngine;

public class Bottle_Proj : MonoBehaviour
{

    public float throwRange = 0;
    public float hitDmg = 0;
    public float kbSpeed = 0;
    public float kbDis = 0;
    public float throwSpeed = 0;
    private Vector3 goalPos = new Vector3(100, 100, 100);

    Rigidbody2D rb;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        goalPos = transform.position + (new Vector3(Move_Player.lastInput.x, Move_Player.lastInput.y, 0) * throwRange);
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Vector3.Distance(transform.position, goalPos) < 1)
        {
            Destroy(gameObject);
        }
        rb.linearVelocity = Move_Player.lastInput.normalized * throwSpeed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Enemy"))
        {
            collision.GetComponent<Main_Enemy>().TakeDmg(hitDmg);
            collision.GetComponent<Main_Enemy>().AddKnockback((collision.transform.position - transform.position).normalized, kbDis, kbSpeed);
            Destroy(gameObject);
        }
        else if(collision.CompareTag("Wall"))
        {
            Destroy(gameObject);
        }
    }
}
