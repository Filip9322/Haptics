using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class copyTranslation : MonoBehaviour
{
    private Vector3    actualPosition;
    private GameObject vrPlayerRig;
    private GameObject vrTrackerDevice;

    // Start is called before the first frame update
    void Start()
    {
        vrPlayerRig     = GameObject.Find("[CameraRig]");
        vrTrackerDevice = GameObject.Find("[CameraRig]/vrTrackerDevice");
        actualPosition  = this.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (vrTrackerDevice == null)
        {
            Debug.LogError("ViveTracker was not find");
        }

        this.transform.position = vrTrackerDevice.transform.position;
        this.transform.rotation = vrTrackerDevice.transform.localRotation;
        calibratePosition();
        calibrateRotation();
       //Debug.Log(actualPosition); 
    }

    void calibratePosition()
    {

    }
    
    void calibrateRotation()
    {
        
    }
}
