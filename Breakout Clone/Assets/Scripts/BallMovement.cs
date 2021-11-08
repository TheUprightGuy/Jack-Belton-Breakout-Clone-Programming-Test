using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallMovement : MonoBehaviour
{
    [Header("Launch Settings")]
    public float minStartAngle = 0;
    public float maxStartAngle = 90;
    
    [Header("Move Settings")]
    public float BallSpeed = 1;

    Vector2 velocity = Vector2.zero;
    Vector3 localOrigin;
    Transform parentPaddle;

    bool WaitingForLaunch = true;

    Rigidbody2D thisRB;

    
    public int PlayerIndex;
    private void Awake()
    {
        localOrigin = transform.localPosition;
        parentPaddle = transform.parent;
        thisRB = GetComponent<Rigidbody2D>();
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    private void Update()
    {
        if (WaitingForLaunch &&
               Input.GetKeyDown(KeyCode.Space))
        {
            LaunchBall();
        }
    }

    private void FixedUpdate()
    {
        if(!WaitingForLaunch)
        {
            UpdatePosition();
        }
    }
    /// <summary>
    /// Update the position every frame to move the ball
    /// </summary>
    void UpdatePosition()
    {
        if (velocity == Vector2.zero)
        {
            return;
        }

        Vector3 vel3D = new Vector3(velocity.x, velocity.y, 0.0f);
        transform.position += (vel3D * Time.deltaTime);

    }

    /// <summary>
    /// Launch the ball based on a speed and min/max angle
    /// </summary>
    void LaunchBall()
    {
        WaitingForLaunch = false;
        velocity = GetVectorAtAngle(Random.Range(minStartAngle, maxStartAngle)) * BallSpeed;
        transform.parent = null;
    }

    /// <summary>
    /// Reset the ball from a moving state to its origin with 0 velocity
    /// </summary>
    void ResetBall()
    {
        transform.parent = parentPaddle;
        transform.localPosition = localOrigin;
        velocity = Vector2.zero;
        WaitingForLaunch = true;
    }

    
    /// <summary>
    /// Helper func for getting a vector from an angle
    /// </summary>
    /// <param name="_angle">Angle for the vector, 0 Degrees is considered Vector2.up</param>
    /// <returns>The direction at angle</returns>
    Vector2 GetVectorAtAngle(float _angle)
    {
        return new Vector2(Mathf.Sin(_angle * Mathf.Deg2Rad), Mathf.Cos(_angle * Mathf.Deg2Rad));
    }

    /// <summary>
    /// Called when the ball hits a collider tagged with Wall
    /// </summary>
    /// <param name="hitNormal"></param>
    void ReboundBallWall(Vector2 hitNormal)
    {
        float hitAngle = Vector2.SignedAngle(-velocity.normalized, hitNormal);
        hitAngle = (hitAngle * 2) * Mathf.Deg2Rad;
        velocity = new Vector2((-velocity.x) * Mathf.Cos(hitAngle) - (-velocity.y) * Mathf.Sin(hitAngle),
                                (-velocity.x) * Mathf.Sin(hitAngle) + (-velocity.y) * Mathf.Cos(hitAngle));
        velocity.Normalize();
        velocity *= BallSpeed;
    }

    /// <summary>
    /// Called when the ball hits the paddle
    /// </summary>
    /// <param name="_contactPoint"></param>
    void ReboundBallPaddle(Collision2D _collision)
    {
        float rightMostAngle = 80.0f; //Magic numbers, cause I couldn't think of a better place to set these vars
        float leftMostAngle = -rightMostAngle;

        Transform paddleTransform = _collision.transform;
        float leftX = paddleTransform.position.x - (paddleTransform.localScale.x / 2);
        float rightX = paddleTransform.position.x + (paddleTransform.localScale.x / 2);

        float leftToRightDist = rightX - leftX;
        float leftToContactPoint = _collision.GetContact(0).point.x - leftX;

        float lerpAngle = Mathf.Lerp(leftMostAngle,rightMostAngle,leftToContactPoint / leftToRightDist);

        velocity = GetVectorAtAngle(lerpAngle).normalized * BallSpeed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "Wall")
        {
            ReboundBallWall(collision.GetContact(0).normal);
        }
        else if(collision.transform.tag == "Paddle")
        {
            ReboundBallPaddle(collision);
        }
        else if(collision.transform.tag == "ThePit")
        {
            ResetBall();
        }
    }
  

    private void OnDrawGizmos()
    {
        Vector2 offset = new Vector2(Mathf.Sin(minStartAngle * Mathf.Deg2Rad), Mathf.Cos(minStartAngle * Mathf.Deg2Rad));
        Gizmos.DrawLine(transform.position, transform.position + (Vector3)offset);

        offset = new Vector2(Mathf.Sin(maxStartAngle * Mathf.Deg2Rad), Mathf.Cos(maxStartAngle * Mathf.Deg2Rad));
        Gizmos.DrawLine(transform.position, transform.position + (Vector3)offset);
    }
}
