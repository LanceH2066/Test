using UnityEngine;
using UnityEngine.InputSystem;

public class Move : MonoBehaviour
{
    private PlayerControls controls;
    private Rigidbody2D rb;
    public float speed = 5f;
    public float jumpHeight = 5f;
    private float moveInput;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        controls = new PlayerControls();
    }

    private void OnEnable()
    {
        controls.Enable();

        controls.Player.Move.performed += ctx => moveInput = ctx.ReadValue<float>();
        controls.Player.Move.canceled += ctx => moveInput = 0; // Stop movement when key is released

        controls.Player.Jump.performed += ctx => Jump();

    }

    private void OnDisable()
    {
        controls.Disable();
    }

    private void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(moveInput * speed, rb.linearVelocity.y);
    }

    private void Jump()
    {
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpHeight);
    }
}
