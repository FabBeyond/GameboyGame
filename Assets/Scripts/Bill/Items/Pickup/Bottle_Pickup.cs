using UnityEngine;

public class Bottle_Pickup : Essential_Items
{

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void itemFunction()
    { 
        GameObject ThrownBottle = Instantiate(PrefabManager.StaticThrowBottle, GameObject.FindGameObjectWithTag("Player").transform.position, Quaternion.identity);
    }
}
