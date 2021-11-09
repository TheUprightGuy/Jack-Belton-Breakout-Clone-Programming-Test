using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreMaster : MonoBehaviour
{

    public GameController GC;
    int TotalScore = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (EmptyCheck())
        {
            foreach (Transform item in transform)
            {
                item.gameObject.SetActive(true);
            }
        }
    }

    bool EmptyCheck()
    {
        foreach (Transform item in transform)
        {
            if (item.gameObject.activeInHierarchy)
            {
                return false;
            }
        }

        return true;
    }
}
