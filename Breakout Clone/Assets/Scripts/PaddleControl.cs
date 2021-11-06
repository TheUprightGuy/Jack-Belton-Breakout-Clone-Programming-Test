using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaddleControl : MonoBehaviour
{
    public float XLimit = 8;
    public float PaddleSpeed = 1;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        MovePaddle();
    }

    void MovePaddle()
    {
        float currentX = transform.position.x;
        float leftX = currentX - (transform.localScale.x / 2);
        float rightX = currentX + (transform.localScale.x / 2);

        if (leftX > -XLimit &&
            Input.GetKey(KeyCode.A))
        {
            currentX -= PaddleSpeed;
        }

        if (rightX < XLimit &&
           Input.GetKey(KeyCode.D))
        {
            currentX += PaddleSpeed;
        }

        transform.position = new Vector3(currentX, transform.position.y, transform.position.z);

    }
}
