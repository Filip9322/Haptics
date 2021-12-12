using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class raycastLaser : MonoBehaviour
{
    private GameObject lastHit;
    public LineRenderer laserLineRenderer;
   
    public bool  modeResizing   = false;
    public Vector3 HDPosition   = new Vector3(0.0f,0.0f,0.0f);
    public float   HDMagnitude  ;

    private float distanceBetween;
    private float initialCalculation;
    private float secondCalculation;

    public float laserWidth     = 0.1f;
    public float laserMaxLength = 5f; 
    public Vector3 collision    = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {
        lastHit = null;
        laserLineRenderer.SetWidth(laserWidth, laserWidth);
    }

    // Update is called once per frame
    void Update()
    {
        var ray = new Ray(this.transform.position, this.transform.forward);
        RaycastHit hit;

        Vector3 selectedPosition = new Vector3(0.0f,0.0f,0.0f) ;

        if(Physics.Raycast(ray, out hit, laserMaxLength))
        {
            var objectRayPointing = hit.transform;
            selectedPosition  = objectRayPointing.position;
            
            Debug.DrawRay(transform.position, transform.forward, Color.green);
            lastHit   = hit.transform.gameObject;

            if (lastHit == hit.transform.gameObject && selectedPosition != null)
            {
                collision = hit.point;
                laserLineRenderer.enabled = true;
            } 
        }
        else
        {
            laserLineRenderer.enabled = false;
        }

        laserLineRenderer.SetPosition(0, transform.position);
        laserLineRenderer.SetPosition(1, collision);

        /* - Activate Mode Resizing - */
        if(modeResizing == true)
        {
            resizeObject(selectedPosition);
        }
    }

    void resizeObject(Vector3 positionObject){
        //Debug.Log("Im gonna resize the world"+positionObject);
        //Debug.Log("PencilPosition: "+ HDPosition);
        //Debug.Log("Magnitude: "+ HDMagnitude);
        var directionMagnitude = 0;
        calculateDistance(positionObject, HDPosition);
        if(initialCalculation != null){
            Debug.Log("pos1: "+positionObject+"pos2: "+HDPosition);
            Debug.Log("ical: "+initialCalculation+"db: "+distanceBetween);
            if(initialCalculation >= distanceBetween){
                directionMagnitude = -1;
            }else
                directionMagnitude = 1;
            initialCalculation = distanceBetween;
        }
        //if(initialCalculation != )
        var magx = 0.001f*directionMagnitude;
        var magy = 0.001f*directionMagnitude;
        var magz = 0.001f*directionMagnitude;
        if(lastHit != null){
            lastHit.transform.localScale += new Vector3(magx,magy,magz);
        }
    }

    /* - Pos1: Initial position 
     * - Pos2: Final position 
     */
    void calculateDistance(Vector3 pos1, Vector3 pos2){
        var pos1x = pos1.x; var pos1y = pos1.y; var pos1z = pos1.z;
        var pos2x = pos2.x; var pos2y = pos2.y; var pos2z = pos2.z;

        var deltax = (pos2x-pos1x);
        var deltay = (pos2y-pos1y);
        var deltaz = (pos2z-pos1z);
        var totalDistance = Mathf.Sqrt((deltax*deltax)+(deltay*deltay)+(deltaz*deltaz));
        distanceBetween = totalDistance;
    }
}
