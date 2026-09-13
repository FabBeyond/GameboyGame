using System.Collections.Generic;
using UnityEngine;

public class UISelector : MonoBehaviour
{
    InputActions input;
    bool resetInput;
    public UIObject selection;
    public bool setup = false;

    private void Start()
    {
        input = BoatController.instance.input;
    }
    private void Update()
    {
        if (!setup) return;

        if (input.UI.Up.WasPressedThisFrame()) MoveSelection(selection.neighbours[0]);
        else if (input.UI.Right.WasPressedThisFrame()) MoveSelection(selection.neighbours[1]);
        else if (input.UI.Down.WasPressedThisFrame()) MoveSelection(selection.neighbours[2]);
        else if (input.UI.Left.WasPressedThisFrame()) MoveSelection(selection.neighbours[3]);

        if (input.UI.Click.WasPressedThisFrame()) selection.onClick.Invoke();
    }
    public void Setup(UIObject startSelection)
    {
        setup = true;
        selection = startSelection;
        transform.position = selection.transform.position;
    }
    void MoveSelection(UIObject newSelection)
    {
        if (newSelection == null) return;

        selection = newSelection;
        transform.position = selection.transform.position;
    }
}
