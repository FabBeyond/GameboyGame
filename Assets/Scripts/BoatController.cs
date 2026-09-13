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
        if (!PlayerData.Instance.boatCanMove) return;

        Vector2 movementInput = input.Player.Movement.ReadValue<Vector2>();
        movementInput = ControlUtils.SnapToDir(ControlUtils.DeadzoneCheck(movementInput), 8).normalized * speed * Time.deltaTime;
        transform.position += new Vector3(movementInput.x, movementInput.y, 0);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Building"))
        {
            GameManager.instance.SwitchBoatBuild();
        }
    }
}
