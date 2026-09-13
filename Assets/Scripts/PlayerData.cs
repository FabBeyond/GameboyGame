using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[Serializable]
public struct ShipTiles
{
    public string tileId;
    public GameObject prefab;
}

public class PlayerData : MonoBehaviour
{
    public static PlayerData Instance;
    public List<ShipTiles> shipPrefabs;
    Dictionary<string, GameObject> lookup;
    public List<ShipPartBase> shipParts = new List<ShipPartBase>();
    public float deadzone = 0.5f;
    public bool boatCanMove = true;
    public bool boatBuildMode = false;
    public string boatTiles; // starting config -----bb--cc--bb-----
    public Dictionary<char, string> alphabetToID = new Dictionary<char, string>()
    {
        {'a', "basic"},
        {'b', "sail"}
    };

    private void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(this);
        lookup = new Dictionary<string, GameObject>();

        foreach (var entry in shipPrefabs)
            lookup[entry.tileId] = entry.prefab;
    }

    public GameObject GetTile(string tileId)
    {
        return lookup.TryGetValue(tileId, out GameObject tile) ? tile : null;
    }
}
