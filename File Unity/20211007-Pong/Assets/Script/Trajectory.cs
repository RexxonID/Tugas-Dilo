using UnityEngine;

public class Trajectory : MonoBehaviour
{
    public BallControl ball;
    CircleCollider2D ballCollider;
    Rigidbody2D ballRigidbody;

    public GameObject ballAtCollison;
    
    void Start()
    {
        ballRigidbody = ball.GetComponent<Rigidbody2D>();
        ballCollider = ball.GetComponent<CircleCollider2D>();
    }
    void Update()
    {
        bool drawBallAtCollison = false;
        Vector2 offsetHitPoint = new Vector2();
        RaycastHit2D[] circleCastHit2DArray = 
            Physics2D.CircleCastAll(ballRigidbody.position, ballCollider.radius,
            ballRigidbody.velocity.normalized);

        foreach (RaycastHit2D circleCaseHit2D in circleCastHit2DArray)
        {
            if (circleCaseHit2D.collider != null && circleCaseHit2D.collider.GetComponent<BallControl>() == null)
            {
                Vector2 hitPoint = circleCaseHit2D.point;
                Vector2 hitNormal = circleCaseHit2D.normal;

                offsetHitPoint = hitPoint + hitNormal * ballCollider.radius;

                DottedLine.DottedLine.Instance.DrawDottedLine(ball.transform.position, offsetHitPoint);

                if (circleCaseHit2D.collider.GetComponent<SideWall>() == null)
                {
                    Vector2 inVector = (offsetHitPoint - ball.TrajectoryOrigin).normalized;
                    Vector2 outVector = Vector2.Reflect(inVector, hitNormal);

                    float outDot = Vector2.Dot(outVector, hitNormal);
                    if (outDot > -1.0f && outDot < 1.0f)
                    {
                        DottedLine.DottedLine.Instance.DrawDottedLine( offsetHitPoint, offsetHitPoint + outVector * 10.0f);

                        drawBallAtCollison = true;
                    }
                }

                if (drawBallAtCollison)
                {
                    ballAtCollison.transform.position = offsetHitPoint;
                    ballAtCollison.SetActive(true);
                }
                else
                {
                    ballAtCollison.SetActive(false);
                }
                return;
            }
        }
    }
}
