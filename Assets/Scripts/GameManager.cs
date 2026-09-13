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
        for (int i = 0; i < 5; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                GameObject shipPart = Instantiate(pd.GetTile("basic"));
                shipPart.transform.parent = BoatController.instance.transform;

                ShipPartBase shipPartBase = shipPart.GetComponent<ShipPartBase>();
                shipPartBase.position = new Vector2(j, i);
                if (!(j == 0 || j == 3 || i == 0 || i == 4))
                {
                    shipPartBase.active = true;
                }
                pd.shipParts.Add(shipPartBase);
            }
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
        }
    }
}
