using UnityEngine;
using TMPro;

public class Flap : MonoBehaviour
{
    public Rigidbody2D rb;
    public float jumpForce = 5f;
    private InputSystem_Actions controls;
    public TMP_Text text;
    private int score = 0;
    public PipeSpawner pipeSpawner;
    private Animator  animator;
    bool isDead = false;
    private void Awake()
    {
        controls = new InputSystem_Actions();
        animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        controls.Enable();
        controls.Player.Jump.performed += ctx => Jump();
    }

    private void OnDisable()
    {
        controls.Disable();
    }

    private void Jump()
    {
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Pipe"))
        {
            controls.Disable();
            rb.linearVelocityX = 0f;
            animator.enabled = false;
            isDead = true;
        }
        else if(!isDead)
        {
            score++;
            text.text = score + "";
        }
    }

    private void FixedUpdate()
    {
        RotateBird();
        if(gameObject.transform.position.y <= -3.25f)
        {
            Time.timeScale = 0f;
            pipeSpawner.gameOver();
            animator.enabled = false;
        }
    }

    private void RotateBird()
    {
        float tiltAngle = Mathf.Clamp(rb.linearVelocity.y * 5f, -90f, 30f);
        transform.rotation = Quaternion.Euler(0, 0, tiltAngle);
    }

}
