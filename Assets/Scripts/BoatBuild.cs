using System;
using System.Collections.Generic;
using UnityEngine;

public class BoatBuild : MonoBehaviour
{
    public static BoatBuild instance;
    public GameObject contents;
    public Vector2 boatPosition;
    public float scale;
    public GameObject uiSelectorPrefab;
    public UIObject tileSelectionStart;
    public UIObject placementSelectionStart;
    public string currentMode;
    public GameObject uiSelector;
    public GameObject rightSide;
    public GameObject gridObjects;
    List<List<UIObject>> placementPositions = new List<List<UIObject>>();
    List<(int, int)> neighbourOffset = new List<(int, int)>()
    {
        (-1, 0),
        (0, 1),
        (1, 0),
        (0, -1)
    };

    private void Awake()
    {
        instance = this;
    }

    private void Update()
    {
        if (currentMode == "tileSelect")
        {
            if (!BoatController.instance.input.UI.Back.WasPressedThisFrame()) return;

            GameManager.instance.SwitchBoatBuild();
        }
        else if (currentMode == "tilePlacement")
        {
            if (!BoatController.instance.input.UI.Back.WasPressedThisFrame()) return;

            currentMode = "tileSelect";
            uiSelector.GetComponent<UISelector>().Setup(tileSelectionStart, Vector2.one);
        }
    }

    public void SelectTile(string id)
    {
        currentMode = "tilePlacement";
        uiSelector.GetComponent<UISelector>().Setup(placementSelectionStart, Vector2.one*2);
    }
    
    void SetupBuildGrid()
    {
        gridObjects = new GameObject("Grid Objects");
        gridObjects.transform.parent = rightSide.transform;

        for (int i = 0; i < 5; i++)
        {
            placementPositions.Add(new List<UIObject>());
            for (int j = 0; j < 4; j++)
            {
                GameObject shipPart = new GameObject();
                shipPart.transform.parent = gridObjects.transform;

                Vector2 startingPos = new Vector2(1, 4);
                shipPart.transform.position = new Vector2(startingPos.x + (j * 2), startingPos.y - (i * 2));

                UIObject uiObject = shipPart.AddComponent<UIObject>();
                placementPositions[i].Add(uiObject);
            }
        }
        for (int i = 0; i < placementPositions.Count; i++)
        {
            List<UIObject> placementList = placementPositions[i];
            for (int j = 0; j < placementList.Count; j++)
            {
                UIObject uiObject = placementList[j];
                uiObject.neighbours = new List<UIObject>();

                foreach ((int, int) offset in  neighbourOffset)
                {
                    int newX = i + offset.Item1;
                    int newY = j + offset.Item2;

                    if (newX < 0 || newY < 0 || newX >= placementPositions.Count || newY >= placementPositions[newY].Count)
                    {
                        uiObject.neighbours.Add(null);
                        continue;
                    }
                    uiObject.neighbours.Add(placementPositions[newX][newY]);
                }
            }
        }

        placementSelectionStart = placementPositions[0][0];
    }

    public void StartBoatBuild()
    {
        currentMode = "tileSelect";

        contents.SetActive(true);

        SetupBuildGrid();

        uiSelector = Instantiate(uiSelectorPrefab);
        uiSelector.GetComponent<UISelector>().Setup(tileSelectionStart, Vector2.one);
    }
    public void EndBoatBuild()
    {
        currentMode = "";
        contents.SetActive(false);
        placementPositions.Clear();
        Destroy(uiSelector);
        Destroy(gridObjects);
    }
}
