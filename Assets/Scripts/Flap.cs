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
    [SerializeField]
    private AudioClip flapSound;
    [SerializeField]
    private AudioClip hitSound;
    [SerializeField]
    private AudioClip pointSound;
    [SerializeField]
    private AudioClip deadSound;

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
        AudioSource.PlayClipAtPoint(flapSound, Vector2.zero);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Pipe") && !isDead)     // Not dead & hit pipe
        {
            controls.Disable();
            rb.linearVelocityX = 0f;
            animator.enabled = false;
            isDead = true;
            AudioSource.PlayClipAtPoint(hitSound, transform.position);
        }
        else if(!isDead && other.CompareTag("Point"))   // Not dead & hit middle
        {
            score++;
            text.text = score + "";
            AudioSource.PlayClipAtPoint(pointSound, transform.position);
        }
        else if(isDead && other.CompareTag("Ground"))   // Dead and hit ground
        {
            Time.timeScale = 0f;
            pipeSpawner.gameOver();
            animator.enabled = false;
            AudioSource.PlayClipAtPoint(deadSound, transform.position);
        }
        else if(!isDead && other.CompareTag("Ground"))  // Not dead but fall into ground
        {
            AudioSource.PlayClipAtPoint(hitSound, transform.position);
            Time.timeScale = 0f;
            pipeSpawner.gameOver();
            animator.enabled = false;
            AudioSource.PlayClipAtPoint(deadSound, transform.position);
        }
    }

    private void FixedUpdate()
    {
        RotateBird();
    }

    private void RotateBird()
    {
        float tiltAngle = Mathf.Clamp(rb.linearVelocity.y * 5f, -90f, 30f);
        transform.rotation = Quaternion.Euler(0, 0, tiltAngle);
    }

}
