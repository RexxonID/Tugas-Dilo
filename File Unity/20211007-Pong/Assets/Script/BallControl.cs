using UnityEngine;

public class BallControl : MonoBehaviour
{
    private Rigidbody2D rigitBody2D;

    public float xInitialForce;
    public float yInitialForce;
    public float force = 10;

    private Vector2 trajectoryOrigin;

    void Start()
    {
        trajectoryOrigin = transform.position;
        rigitBody2D = GetComponent<Rigidbody2D>();

        Invoke("PushBall", 5);
    }
    void ResetBall()
    {
        transform.position = Vector2.zero;
        rigitBody2D.velocity = Vector2.zero;
    }

    void PushBall()
    {
        float randomDirection = Random.Range(0, 2);
        float yRandomInitialForce = Random.Range(-yInitialForce, yInitialForce);
        Debug.Log(yRandomInitialForce);
        if (randomDirection < 1.0f)
        {
            rigitBody2D.AddForce(new Vector2(-xInitialForce, yRandomInitialForce).normalized* force);
        }
        else
        {
            rigitBody2D.AddForce(new Vector2(xInitialForce, yRandomInitialForce).normalized* force);
        }
    }
    void RestartGame()
    {
        ResetBall();
        Invoke("PushBall", 2);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        trajectoryOrigin = transform.position;
    }

    public Vector2 TrajectoryOrigin
    {
        get { return trajectoryOrigin; }
    }
}
