using UnityEngine;

public class BoatController : MonoBehaviour
{
    public static BoatController instance;
    public InputActions input;
    public float speed;

    private void OnEnable()
    {
        instance = this;
        input = new InputActions();
        input.Enable();
    }
    private void OnDisable()
    {
        input.Disable();
    }

    private void Update()
    {
        Vector2 movementInput = input.Player.Movement.ReadValue<Vector2>().normalized;
        movementInput = ControlUtils.SnapToDir(movementInput, 8) * speed * Time.deltaTime;
        transform.position += new Vector3(movementInput.x, movementInput.y, 0);
    }
}
