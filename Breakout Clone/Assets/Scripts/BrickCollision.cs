using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickCollision : MonoBehaviour
{
    ScoreMaster thisMaster;

    public int BrickScore = 100;
    // Start is called before the first frame update
    void Start()
    {
        thisMaster = GetScoreMaster();
    }

    ScoreMaster GetScoreMaster()
    {
        ScoreMaster thisMaster = null;
        Transform currentParent = transform.parent;
        while(currentParent != null)
        {
            if (currentParent.GetComponent<ScoreMaster>())
            {
                thisMaster = currentParent.GetComponent<ScoreMaster>();
                break;
            }
            else
            {
                currentParent = currentParent.parent;
            }
        }

        return thisMaster;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        BallMovement hitBall = collision.gameObject.GetComponent<BallMovement>();
        if (hitBall != null)
        {
            thisMaster.GC.Players[hitBall.PlayerIndex].Score += BrickScore;
        }
        
        gameObject.SetActive(false);
    }
}
