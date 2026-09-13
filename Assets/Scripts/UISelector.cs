using System.Collections.Generic;
using UnityEngine;

public class UISelector : MonoBehaviour
{
    InputActions input;
    bool resetInput;
    public List<UIObject> uiObjects = new List<UIObject>();
    private void Start()
    {
        input = BoatController.instance.input;
    }
    private void Update()
    {
        Vector2 cursorMovement = ControlUtils.DeadzoneCheck(input.Player.Movement.ReadValue<Vector2>());
        cursorMovement = ControlUtils.SnapToDir(cursorMovement, 4);
        if (cursorMovement == Vector2.zero)
        {
            resetInput = true;
            return;
        }

        cursorMovement = ControlUtils.DeadzoneCheck(cursorMovement);

        if (resetInput)
        {
            resetInput = false;
            transform.Translate(cursorMovement.normalized);
        }
    }
}
