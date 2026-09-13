using UnityEngine;

public class BoatPlaceSelector : MonoBehaviour
{
    InputActions input;
    bool resetInput;
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
