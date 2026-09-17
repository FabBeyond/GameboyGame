using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public Camera Cam;

    static public bool canScreen = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        GameObject[] anchors = GameObject.FindGameObjectsWithTag("Anchors");
        GameObject nearAnchor = null;
        for(int i = 0; i < anchors.Length; i++)
        {
            if(nearAnchor != null)
            {
                if ((anchors[i].transform.position - Move_Player.playerPos).magnitude < (nearAnchor.transform.position - Move_Player.playerPos).magnitude)
                {
                    nearAnchor = anchors[i];
                }
            }
            else
            {
                nearAnchor = anchors[0];
            }
        }

        if(canScreen)
        {
            Cam.transform.position = new Vector3(nearAnchor.transform.position.x, nearAnchor.transform.position.y, -10);
        }
    }
}
