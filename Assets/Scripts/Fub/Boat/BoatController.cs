using UnityEditor;
using UnityEngine;

public class BoatController : MonoBehaviour
{
    public static BoatController instance;
    public InputActions input;
    public float speed;
    public float minSpeed;
    Transform lastBoatSpawn;
    public float rotateSpeed;

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

        float calcedSpeed = minSpeed + (speed - minSpeed) * (1 - PlayerData.Instance.weight / 200);

        Vector2 movementInput = input.Player.Movement.ReadValue<Vector2>();
        Vector2 movementInputSnapped = ControlUtils.SnapToDir(movementInput, 8).normalized;
        Vector2 move = new Vector3(0, movementInputSnapped.y, 0) * calcedSpeed * Time.deltaTime;
        transform.Translate(move);

        float angle = Mathf.Atan2(movementInputSnapped.x, movementInputSnapped.y) * Mathf.Rad2Deg;
        if (angle == 180) angle = 0;

        transform.Rotate(0, 0, -angle * Time.deltaTime * rotateSpeed);
    }

    public void ResetFromBuild()
    {
        transform.position = lastBoatSpawn.position;
        transform.rotation = Quaternion.Euler(0, 0, 0);
        transform.localScale = Vector3.one;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Building"))
        {
            lastBoatSpawn = collision.transform.GetChild(0);
            GameManager.instance.SwitchBoatBuild();
        }
    }
}
