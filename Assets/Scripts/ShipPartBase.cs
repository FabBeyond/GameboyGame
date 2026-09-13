using UnityEngine;

public class ShipPartBase : MonoBehaviour
{
    public string tileID;
    SpriteRenderer sr;
    public Vector2 position;
    public Sprite tileSprite;
    public bool active;
    public float resistance;
    public float weight;

    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        Vector2 basePosition = new Vector2(-1.5f, 2);
        transform.localPosition = new Vector3(basePosition.x+position.x, basePosition.y-position.y, 0);
    }
    private void Update()
    {
        //sr.enabled = active;
    }
}
