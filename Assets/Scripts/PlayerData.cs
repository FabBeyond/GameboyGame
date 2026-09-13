using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[Serializable]
public struct ShipTiles
{
    public char tileId;
    public GameObject prefab;
}

public class PlayerData : MonoBehaviour
{
    public static PlayerData Instance;
    public List<ShipTiles> shipPrefabs;
    Dictionary<char, GameObject> lookup;
    public ShipPartBase[] shipParts;
    public float deadzone = 0.5f;
    public bool boatCanMove = true;
    public bool boatBuildMode = false;
    public string boatTiles; // starting config -----bb--cc--bb-----

    private void Awake()
    {
        shipParts = new ShipPartBase[20];
        Instance = this;
        DontDestroyOnLoad(this);
        lookup = new Dictionary<char, GameObject>();

        foreach (var entry in shipPrefabs)
            lookup[entry.tileId] = entry.prefab;
    }

    public GameObject GetTile(char tileId)
    {
        return lookup.TryGetValue(tileId, out GameObject tile) ? tile : null;
    }
    public void SetBoatTile(int idx, char tileId)
    {
        char[] charArr = boatTiles.ToCharArray();
        charArr[idx] = tileId;
        boatTiles = new string(charArr);
    }
}
