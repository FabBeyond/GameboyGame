using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class UIObject : MonoBehaviour
{
    public List<UIObject> neighbours = new List<UIObject>();
    public UnityEvent<string> onClick = new UnityEvent<string>();
    public string arg;
}
