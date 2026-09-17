using Unity.VisualScripting;
using UnityEngine;

public class Knifeguy : MonoBehaviour
{
    [Header("Atking")]
    public GameObject atkHitBox;
    public int dmg = 1;
    public LayerMask PlayerLayer;
    public float kbDistance = 5;
    public float kbSpeed = 10;
    [HideInInspector]
    public bool canAtk = false;

    Rigidbody2D rb;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {

        atkHitBox.transform.up = rb.linearVelocity.normalized;

        if (Physics2D.OverlapBox(atkHitBox.transform.position, atkHitBox.transform.lossyScale, 0, PlayerLayer) != null && !gameObject.GetComponent<Main_Enemy>().isKnocbacked)
        {
            GameObject player = Physics2D.OverlapBox(atkHitBox.transform.position, atkHitBox.transform.lossyScale, 0, PlayerLayer).gameObject;
            player.GetComponent<Main_Player>().TakeDmg(dmg);
            player.GetComponent<Main_Player>().AddKnockback((player.transform.position - transform.position).normalized, kbDistance, kbSpeed);
            gameObject.GetComponent<Main_Enemy>().AddKnockback((transform.position - player.transform.position).normalized, kbDistance/2, kbSpeed/2);
            //Destroy(gameObject);
        }
    }

    /*private void OnCollisionEnter2D(Collision2D collision)
    {
        //meObject player = Physics2D.OverlapBox(atkHitBox.transform.position, atkHitBox.transform.lossyScale, 0, PlayerLayer).gameObject;
        collision.gameObject.GetComponent<Main_Player>().TakeDmg(dmg);
        collision.gameObject.GetComponent<Main_Player>().AddKnockback((collision.gameObject.transform.position - transform.position).normalized, kbDistance, kbSpeed);
        gameObject.GetComponent<Main_Enemy>().AddKnockback((transform.position - collision.gameObject.transform.position).normalized, kbDistance, kbSpeed);
    }*/
}
