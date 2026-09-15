using System;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Events;

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
    public GameObject uiGridObjects;
    public string selectedTile;
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
        selectedTile = id;
        uiSelector.GetComponent<UISelector>().Setup(placementSelectionStart, Vector2.one*2);
    }
    public void PlaceTile(string position)
    {
        string[] pos = position.Split("|");
        int x = int.Parse(pos[0]);
        int y = int.Parse(pos[1]);
        int idx = x + y * 4;

        GameManager.instance.SetTile(char.Parse(selectedTile), x, y, scale);
    }
    
    void SetupBuildGrid()
    {
        for (int i = 0; i < 5; i++)
        {
            placementPositions.Add(new List<UIObject>());
            for (int j = 0; j < 4; j++)
            {
                GameObject shipPart = new GameObject();
                shipPart.transform.parent = uiGridObjects.transform;

                Vector2 startingPos = new Vector2(1, 4);
                shipPart.transform.position = new Vector2(startingPos.x + (j * 2), startingPos.y - (i * 2));

                UIObject uiObject = shipPart.AddComponent<UIObject>();
                uiObject.onClick = new UnityEvent<string>();
                uiObject.onClick.AddListener(PlaceTile);
                uiObject.arg = $"{j}|{i}";

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
        BoatController bc = BoatController.instance;
        bc.transform.position = boatPosition;
        bc.transform.localScale = Vector3.one * scale;
        bc.transform.rotation = Quaternion.Euler(0, 0, 0);

        uiSelector = Instantiate(uiSelectorPrefab);
        uiSelector.GetComponent<UISelector>().Setup(tileSelectionStart, Vector2.one);
    }
    public void EndBoatBuild()
    {
        currentMode = "";
        contents.SetActive(false);
        placementPositions.Clear();
        Destroy(uiSelector);
        for (int i = 0; i < uiGridObjects.transform.childCount; i++)
        {
            Destroy(uiGridObjects.transform.GetChild(i).gameObject);
        }
    }
}
