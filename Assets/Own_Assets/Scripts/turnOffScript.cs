using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class turnOffScript : MonoBehaviour
{

    private bool      scriptState = false;
    public GameObject objectToFind = null;
    public Component  objectScript = null;

    // Start is called before the first frame update
    void Start()
    {
        /*if (objectToFind == null)
            objectToFind = GameObject.Find();*/

        if (objectToFind /* STILL */ == null)
            Debug.LogError("GameObject not found");

    }

    // Update is called once per frame
    void Update()
    {
        /*if (objectToFind != null)
            objectScript = objectToFind.GetComponent<objectScript>;*/
        if (objectScript /* STILL */ == null)
            Debug.LogError("Script not found");

        /*if (componentToOff != null )
            scriptState = objectScript.enabled;*/
        print(scriptState);
    }
}
