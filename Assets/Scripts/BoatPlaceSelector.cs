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
        Vector2 cursorMovement = ControlUtils.SnapToDir(input.Player.Movement.ReadValue<Vector2>(), 4);
        if (cursorMovement == Vector2.zero)
        {
            resetInput = true;
            return;
        }

        if (resetInput)
        {
            resetInput = false;
            transform.Translate(cursorMovement.normalized);
        }
    }
}
