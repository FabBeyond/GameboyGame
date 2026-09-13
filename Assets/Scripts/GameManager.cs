using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public PlayerData pd;
    private void Start()
    {
        instance = this;
        pd = PlayerData.Instance;
        SetupBoat();
    }
    void SetupBoat()
    {
        for (int i = 0; i < pd.boatTiles.Length; i++)
        {
            int x = i % 4;
            int y = i / 4;

            if (pd.boatTiles[i] == '-') continue;

            GameObject tile = pd.GetTile(pd.alphabetToID[pd.boatTiles[i]]);
            GameObject shipPart = Instantiate(tile);
            shipPart.transform.parent = BoatController.instance.transform;

            ShipPartBase shipPartBase = shipPart.GetComponent<ShipPartBase>();
            shipPartBase.position = new Vector2(x, y);

            pd.shipParts.Add(shipPartBase);
        }
    }
    public void SwitchBoatBuild()
    {
        if (!pd.boatBuildMode)
        {
            pd.boatCanMove = false;
            pd.boatBuildMode = true;

            BoatBuild.instance.StartBoatBuild();
        }
        else
        {
            pd.boatCanMove = true;
            pd.boatBuildMode = false;

            BoatBuild.instance.EndBoatBuild();
            BoatController.instance.ResetFromBuild();
        }
    }
}
