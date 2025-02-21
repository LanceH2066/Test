using UnityEngine;

public class Pipe : MonoBehaviour
{
    public float speed = 5f;
    private Rigidbody2D rb;
    private PipeSpawner pipeSpawner;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        pipeSpawner = GetComponentInParent<PipeSpawner>();
    }

    void FixedUpdate()
    {
        if(!pipeSpawner.isGameOver)
        {
            rb.linearVelocityX = speed * -1;
            
            if(gameObject.transform.position.x < -20f)
            {
                Destroy(gameObject);
            }
        }
        else
        {
            rb.linearVelocityX = 0f;
        }
    }
}
