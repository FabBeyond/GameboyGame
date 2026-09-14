using UnityEngine;

public class Gold : MonoBehaviour
{
    [Header("Gold")]
    public int amount = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Main_Player.gold += amount;
            Destroy(gameObject);
        }
    }
}
