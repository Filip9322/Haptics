using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class obtRotate : MonoBehaviour
{
    public double speedRotationY;
    public double speedRotationZ;
    public bool   sphereAnimate = false;
    void Start()
    {   
        speedRotationY = 0.0f;
        speedRotationZ = 0.0f;
    }

    public void animateSphere(double spRotationY){
        speedRotationY = spRotationY;
        if (speedRotationY >= 0)
            sphereAnimate = true;
        else
            sphereAnimate = false;
    }
    
    // Update is called once per frame
    void Update()
    {
        if(sphereAnimate == true)
        {
            transform.Rotate(0, (float)speedRotationY, 0);
        }
    }
}
