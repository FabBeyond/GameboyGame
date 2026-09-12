using UnityEngine;

public class Attack_Player : MonoBehaviour
{
    [Header("Attack")]
    public GameObject atkObj;
    public GameObject atkPivot;
    public float windupSpeed = 0.1f;
    public float atkCd = 0.3f;
    public float atkDamage = 1f;
    public float kbDistance = 5;
    public float kbSpeed = 10;
    public LayerMask enemyLayer;

    private bool isAttacking = false;
    private bool canAtk = true;
    private float cDTimer = 0f;
    private float windupTimer = 0f;

    Move_Player move;
    Rigidbody2D rb;
    InputActions input;

    private void Awake()
    {
        input = new();
        input.Enable();
    }

    private void OnDisable()
    {
        input.Disable();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        move = GetComponent<Move_Player>(); 
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Checks();
        Attack();
    }

    public void Checks()
    {
        atkPivot.transform.right = Move_Player.lastInput;
        atkPivot.transform.position = transform.position;
    }

    public void Attack()
    {
        if (canAtk && input.Player.A.WasPerformedThisFrame())
        {
            isAttacking = true;
        }

        if (isAttacking)
        {
            canAtk = false;
            move.canMove = false;
            rb.linearVelocity = Vector2.zero;
            if(windupTimer > windupSpeed)
            {
                Collider2D[] targets = Physics2D.OverlapBoxAll(atkObj.transform.position, atkObj.transform.lossyScale * 2, 0, enemyLayer);
                if(targets.Length > 0)
                {
                    for (int i = 0; i < targets.Length; i++)
                    {
                        targets[i].GetComponent<Main_Enemy>().TakeDmg(atkDamage);
                        StartCoroutine(targets[i].GetComponent<Main_Enemy>().Knockback((targets[i].transform.position - transform.position).normalized, kbDistance, kbSpeed));
                    }
                }
                isAttacking = false;
                move.canMove = true;
                canAtk = true;
                cDTimer = 0f;
            }
            else
            {
                windupTimer += Time.deltaTime;
            }
        }
        else
        {
            windupTimer = 0;
            cDTimer += Time.deltaTime;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(atkObj.transform.position, atkObj.transform.lossyScale * 2);
    }

}
