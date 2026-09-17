using UnityEditor.Rendering.Canvas.ShaderGraph;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.UIElements;
using Microsoft.Unity.VisualStudio.Editor;

public class Main_Player : MonoBehaviour
{
    [Header("Health")]
    public int maxHp = 5;
    public GameObject Hearts1;
    public GameObject Hearts2;
    public GameObject Hearts3;
    private int hp = 0;

    [Header("Gold")]
    public TextMeshProUGUI goldText;
    public int maxGold = 99;
    static public int gold = 0;

    [Header("Knockback")]
    public float unstuckTime = 0.3f;
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
        hp = maxHp;
        UpdateHP();
        //Image uiHp = Instantiate(uiHearts, transform.position, Quaternion.identity, Canvas.transform);
    }

    // Update is called once per frame
    void Update()
    {
        Checks();
        KnockChecks();
        print(hp);
    }

    public void Checks()
    {
        if(gold>maxGold)
        {
            gold = maxGold;
        }
    }

    public void TakeDmg(int dmg)
    {
        hp -= dmg;
        UpdateHP();
    }

    public void AddKnockback(Vector3 dir, float distance, float speed)
    {
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
            if (knockbackedtimer > unstuckTime)
            {
                isPlayerKB = false;
                Move_Player.canMove = true;
                Attack_Player.canAtk = true;
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
                isPlayerKB = false;
                Move_Player.canMove = true;
                Attack_Player.canAtk = true;
            }
        }
    }

    public void UpdateHP()
    {
        if(hp == 1)
        {
            Hearts2.SetActive(false);
            Hearts3.SetActive(false);
        }
        else if(hp == 2)
        {
            Hearts3.SetActive(false);
            Hearts2.SetActive(true);
        }
        else if(hp == 3)
        {
            Hearts2.SetActive(true);
            Hearts3.SetActive(true);
        }
    }
}
