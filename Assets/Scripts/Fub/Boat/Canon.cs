using UnityEngine;

public class Canon : MonoBehaviour
{
    public GameObject canonBallPrefab;
    public InputActions input;
    private void Start()
    {
        input = BoatController.instance.input;
    }
    private void Update()
    {
        if (!PlayerData.Instance.canShoot) return;

        if (input.Player.A.WasPressedThisFrame())
        {
            GameObject canonBall = Instantiate(canonBallPrefab, transform.position, transform.rotation);
        }
    }
}
