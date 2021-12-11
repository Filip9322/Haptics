using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class copyTranslation : MonoBehaviour
{
    private Vector3    actualPosition;
    private Vector3    rePosicion;
    private Vector3    reRotation;
    private GameObject vrPlayerRig;
    private GameObject vrTrackerDevice;
    private GameObject emptyTrackHaptic;

    // Start is called before the first frame update
    void Start()
    {
        vrPlayerRig      = GameObject.Find("[CameraRig]");
        vrTrackerDevice  = GameObject.Find("[CameraRig]/vrTrackerDevice");
        emptyTrackHaptic = GameObject.Find("[CameraRig]/vrTrackerDevice/baseHapticTracker");
        actualPosition   = this.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (vrTrackerDevice == null)
        {
            Debug.LogError("ViveTracker was not find");
        }

        rePosicion = emptyTrackHaptic.transform.position;
        reRotation = vrTrackerDevice.transform.localRotation.eulerAngles;

        calibratePosition();
        calibrateRotation();
        //Debug.Log(reRotation.eulerAngles.x); 
    }

    void calibratePosition()
    {
        var xRounded = (Mathf.Round(rePosicion.x*100))/100;
        var yRounded = (Mathf.Round(rePosicion.y*100))/100;
        var zRounded = (Mathf.Round(rePosicion.z*100))/100;

        this.transform.position = new Vector3(xRounded, yRounded, zRounded);
    }

    void calibrateRotation()
    {
        var xRounded = (Mathf.Round(reRotation.x*100))/100;
        var yRounded = (Mathf.Round(reRotation.y*100))/100;
        var zRounded = (Mathf.Round(reRotation.z*100))/100;
        this.transform.eulerAngles = new Vector3( xRounded, yRounded, zRounded);
    }
}
