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
        Vector2 movementInputSnapped = ControlUtils.SnapToDir(movementInput, 8).normalized;
        transform.position += new Vector3(movementInput.x, movementInput.y, 0) * speed * Time.deltaTime;

        Vector2 movementDir = ControlUtils.SnapToDir(movementInput, 4).normalized;
        if (movementDir == Vector2.zero) return;

        float angle = Mathf.Atan2(movementDir.x, movementDir.y)*Mathf.Rad2Deg;
        angle = Mathf.Repeat(angle, 360f);
        if (angle == 360) angle = 0;

        transform.rotation = Quaternion.Euler(0, 0, -angle);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Building"))
        {
            GameManager.instance.SwitchBoatBuild();
        }
    }
}
