using System.Collections;
using UnityEngine;

public class Boom_Barrel : MonoBehaviour
{
    public GameObject BoomSize;
    public float detoTime = 1;
    public int dmg = 2;
    public float kbSpeed = 10;
    public float kbDis = 5;
    public LayerMask TargetLayer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator Explode()
    {
        yield return new WaitForSeconds(detoTime);
        Collider2D[] targets = Physics2D.OverlapCircleAll(BoomSize.transform.position, BoomSize.transform.lossyScale.x /2, TargetLayer); 
        for(int i  = 0; i < targets.Length; i++)
        {
            if (targets[i].gameObject.CompareTag("Player"))
            {
                targets[i].GetComponent<Main_Player>().TakeDmg(dmg);
                targets[i].GetComponent<Main_Player>().AddKnockback((targets[i].transform.position - transform.position).normalized, kbDis, kbSpeed);
            }
            else if (targets[i].CompareTag("Enemy"))
            {
                targets[i].GetComponent<Main_Enemy>().TakeDmg(dmg);
                targets[i].GetComponent<Main_Enemy>().AddKnockback((targets[i].transform.position - transform.position).normalized, kbDis, kbSpeed);
            }
            else if (targets[i].CompareTag("ExBarrel"))
            {
                targets[i].GetComponent<Boom_Barrel>().Explode();
            }
        }
        Destroy(gameObject);
    }
}
