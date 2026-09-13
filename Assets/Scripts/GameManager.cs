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

            SetTile(pd.boatTiles[i], x, y);
        }
    }
    public void SetTile(char id, int x, int y, float scale = 1)
    {
        int idx = x + y * 4;
        if (pd.boatTiles[idx] != '-')
        {
            pd.SetBoatTile(idx, '-');

            Destroy(pd.shipParts[idx]?.gameObject);
        }

        GameObject tile = pd.GetTile(id);
        GameObject shipPart = Instantiate(tile);
        shipPart.transform.parent = BoatController.instance.transform;
        shipPart.transform.localScale = new Vector3(1, 1, 1);

        ShipPartBase shipPartBase = shipPart.GetComponent<ShipPartBase>();
        shipPartBase.position = new Vector2(x, y);

        pd.shipParts[idx] = shipPartBase;
        pd.SetBoatTile(idx, id);
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
