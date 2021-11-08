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
        thisMaster.TotalScore += BrickScore;
        gameObject.SetActive(false);
    }
}
