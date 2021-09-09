using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Raytest : MonoBehaviour
{
    public GameObject lastHit;
    public LineRenderer laserLineRenderer;
    public pullEffectSpring effectSpring;

    public float laserWidth     = 0.1f;
    public float laserMaxLength = 5f; 
    public Vector3 collision    = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {
        effectSpring = GameObject.Find("effectsHapticDevice").GetComponent<pullEffectSpring>();
        laserLineRenderer.SetWidth(laserWidth, laserWidth);
    }

    // Update is called once per frame
    void Update()
    {
        var ray = new Ray(this.transform.position, this.transform.forward);
        RaycastHit hit;

        if(Physics.Raycast(ray, out hit, laserMaxLength))
        {

            Debug.DrawRay(transform.position, transform.forward, Color.green);
            lastHit   = hit.transform.gameObject;

            if (lastHit == hit.transform.gameObject)
            {
                collision = hit.point;
                laserLineRenderer.enabled = true;
                
                effectSpring.laserPos   = transform.position;
                effectSpring.pullDir    = transform.forward;
                effectSpring.pullingOn  = true;
            }
        }
        else
        {
            laserLineRenderer.enabled = false;
            effectSpring.pullingOn  = false;
        }

        laserLineRenderer.SetPosition(0, transform.position);
        laserLineRenderer.SetPosition(1, collision);

    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(collision, 0.2f);
    }
}
