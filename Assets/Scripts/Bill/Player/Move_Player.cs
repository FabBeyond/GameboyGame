using UnityEngine.InputSystem;
using UnityEngine;
using Unity;
using UnityEngine.Windows;

public class Move_Player : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 5;
    static public bool canMove = true;
    private float speed = 0;
    static public Vector2 inputVector = Vector2.zero;
    static public Vector2 lastInput = Vector2.zero;
    static public Vector3 playerPos = Vector2.zero;


    InputActions input;
    Rigidbody2D rb;

    private void Awake()
    {
        input = new();
        input.Enable();
    }

    private void OnDisable()
    {
        input.Disable();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        speed = moveSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        Checks();
        Move();
        ControlUtils.SnapToDir(rb.linearVelocity, 8);
        playerPos = transform.position;
    }

    public void Checks()
    {
        inputVector = input.Player.Movement.ReadValue<Vector2>().normalized;
        if(inputVector != Vector2.zero)
        {
            lastInput = inputVector;
        }
    }

    public void Move()
    {
        if (canMove)
        {
            rb.linearVelocity = inputVector * speed;
        }
    }
}
