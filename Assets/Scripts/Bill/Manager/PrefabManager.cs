using UnityEngine;

public class PrefabManager : MonoBehaviour
{

    public GameObject ThrowBottle;
    static public GameObject StaticThrowBottle;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StaticThrowBottle = ThrowBottle;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
