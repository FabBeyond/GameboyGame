using UnityEngine;

public class BoatBuild : MonoBehaviour
{
    public static BoatBuild instance;
    public GameObject contents;
    public Vector2 boatPosition;
    public float scale;

    private void Awake()
    {
        instance = this;
    }

    public void StartBoatBuild()
    {
        contents.SetActive(true);
        BoatController.instance.transform.position = boatPosition;
        BoatController.instance.transform.localScale = Vector3.one*scale;
    }
}
