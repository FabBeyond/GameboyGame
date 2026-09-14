using UnityEditor.Rendering.Canvas.ShaderGraph;
using UnityEngine;
using TMPro;

public class Main_Player : MonoBehaviour
{
    [Header("Health")]
    public float maxHp = 5;
    private float hp = 0;

    [Header("Gold")]
    public TextMeshProUGUI goldText;
    public int maxGold = 99;
    static public int gold = 0;

    [Header("Knockback")]
    static public bool isPlayerKB = false;
    private Vector3 startPos = Vector3.zero;
    private Vector3 targetPos = Vector3.zero;
    private Vector2 kbDir = Vector2.zero;
    private float kbSpeed = 0;
    private float knockbackedtimer = 0;

    Rigidbody2D rb;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Checks();
        KnockChecks();
    }

    public void Checks()
    {
        if(gold>maxGold)
        {
            gold = maxGold;
        }
    }

    public void TakeDmg(float dmg)
    {
        hp -= dmg;
    }

    public void AddKnockback(Vector3 dir, float distance, float speed)
    {
        print("asas");
        isPlayerKB = true;
        Move_Player.canMove = false;
        Attack_Player.canAtk = false;
        startPos = transform.position;
        targetPos = startPos + dir.normalized * distance;
        kbDir = dir;
        kbSpeed = speed;

        rb.linearVelocity = Vector3.zero;
    }

    public void KnockChecks()
    {
        if (isPlayerKB)
        {
            if (Vector3.Distance(transform.position, targetPos) >= 1)
            {
                rb.linearVelocity = (targetPos - transform.position).normalized * kbSpeed;
            }
            else
            {
                rb.linearVelocity = Vector2.zero;
                isPlayerKB = false;
                Move_Player.canMove = true;
                Attack_Player.canAtk = true;
            }
        }
    }
}
