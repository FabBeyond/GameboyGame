using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
    public static PlayerData Instance;
    public List<GameObject> shipPrefabs = new List<GameObject>();

    private void Start()
    {
        Instance = this;
        DontDestroyOnLoad(this);
        SetupBoat();
    }

    void SetupBoat()
    {
        for (int i = 0; i < 5; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                GameObject shipPart = Instantiate(shipPrefabs[0]);
                shipPart.transform.parent = BoatController.instance.transform;

                ShipPartBase shipPartBase = shipPart.GetComponent<ShipPartBase>();
                shipPartBase.position = new Vector2(j, i);
                if (!(j == 0 || j == 3 || i == 0 || i == 4))
                {
                    shipPartBase.active = true;
                }
                shipParts.Add(shipPartBase);
            }
        }
    }

    public List<ShipPartBase> shipParts = new List<ShipPartBase>();
}
