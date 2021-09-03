using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class obtRotate : MonoBehaviour
{
    public double speedRotationY;
    public double speedRotationZ;
    void Start()
    {
        speedRotationY = 0.5f;
        speedRotationZ = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxis("Vertical") < 0)
        {
            speedRotationY -= 0.03;
            if (speedRotationY <= 0.0)
            {
                speedRotationY = 0;
            }
        }
        else if (Input.GetAxis("Vertical") > 0)
        {
            speedRotationY += 0.03;
            if (speedRotationY >= 6.0)
            {
                speedRotationY = 6.0;
            }
        }
        if(Input.GetAxis("Horizontal") < 0)
        {
            speedRotationZ -= 0.01;
            if (speedRotationZ <= 0.0)
            {
                speedRotationZ = 0;
            }
        }
        else if (Input.GetAxis("Horizontal") > 0)
        {
            speedRotationZ += 0.01;
            if (speedRotationZ >= 3.0)
            {
                speedRotationZ = 6.0;
            }
        }
        transform.Rotate(0, (float)speedRotationY, (float)speedRotationZ);
    }
}
