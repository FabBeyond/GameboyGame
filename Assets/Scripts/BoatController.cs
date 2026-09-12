using UnityEngine;

public class BoatController : MonoBehaviour
{
    public InputActions input;
    public float speed;

    private void OnEnable()
    {
        input = new InputActions();
        input.Enable();
    }
    private void OnDisable()
    {
        input.Disable();
    }

    private void Update()
    {
        Vector2 movementInput = input.Player.Movement.ReadValue<Vector2>().normalized * speed * Time.deltaTime;
        transform.position += new Vector3(movementInput.x, movementInput.y, 0);
    }
}
