using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
    public static PlayerData Instance;
    public List<GameObject> shipPrefabs = new List<GameObject>();
    public List<ShipPartBase> shipParts = new List<ShipPartBase>();
    public float deadzone = 0.5f;
    public bool boatCanMove = true;
    public bool boatBuildMode = false;

    private void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(this);
    }
}
