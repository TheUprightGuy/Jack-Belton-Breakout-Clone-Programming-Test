using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallMovement : MonoBehaviour
{

    public float minStartAngle = 0;
    public float maxStartAngle = 90;
    public float BallSpeed = 1;

    Vector2 velocity = Vector2.zero;
    Vector3 localOrigin;
    Transform parentPaddle;

    bool WaitingForLaunch = true;

    private void Awake()
    {
        localOrigin = transform.localPosition;
        parentPaddle = transform.parent;
    }

    // Start is called before the first frame update
    void Start()
    {
        //velocity = RandomStartVector(-45.0f, 45.0f) * BallSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        if (WaitingForLaunch &&
            Input.GetKeyDown(KeyCode.Space))
        {
            LaunchBall();
        }
        else
        {
            UpdatePosition();
        }
    }

    void UpdatePosition()
    {
        if (velocity == Vector2.zero)
        {
            return;
        }

        Vector3 vel3D = new Vector3(velocity.x, velocity.y, 0.0f);

        transform.position += vel3D;
    }

    void LaunchBall()
    {
        WaitingForLaunch = false;
        velocity = RandomStartVector(minStartAngle, maxStartAngle) * BallSpeed;
        transform.parent = null;
    }

    void ResetBall()
    {
        transform.parent = parentPaddle;
        transform.localPosition = localOrigin;
        velocity = Vector2.zero;
        WaitingForLaunch = true;
    }

    Vector2 RandomStartVector(float _min, float _max)
    {
        var randomAngle = Random.Range(_min, _max);

        return new Vector2(Mathf.Sin(randomAngle * Mathf.Deg2Rad), Mathf.Cos(randomAngle * Mathf.Deg2Rad));
    }

    void ReboundBall(Vector2 hitNormal)
    {
        float hitAngle = Vector2.SignedAngle(-velocity.normalized, hitNormal);
        hitAngle = (hitAngle * 2) * Mathf.Deg2Rad;
        velocity = new Vector2((-velocity.x) * Mathf.Cos(hitAngle) - (-velocity.y) * Mathf.Sin(hitAngle),
                                (-velocity.x) * Mathf.Sin(hitAngle) + (-velocity.y) * Mathf.Cos(hitAngle));

    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "Wall" ||
            collision.transform.tag == "Paddle")
        {
            ReboundBall(collision.GetContact(0).normal);
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
