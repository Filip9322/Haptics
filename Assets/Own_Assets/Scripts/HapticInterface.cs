using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HapticInterface : MonoBehaviour
{

    public HapticPlugin HapticDevice = null;
    public GameObject Drums = null;
    public Image depthMeter = null;

    private GameObject laserPointerComp;
    private GameObject button1ImageHg = null;
    private GameObject button2ImageHg = null;

    private GameObject toggleMode1 = null;
    private GameObject toggleMode2 = null;
    private GameObject labelToggle1 = null;
    private GameObject labelToggle2 = null;

    private float depthMax  = 25.0f;
    private int   behavior  = 0;
    private bool  pressBtn2 = false;
    private Color Color1    = new Color32(238, 90, 36,255);
    private Color Color2    = new Color32(255,255,255,255);

    // Start is called before the first frame update
    void Start()
    {
        if (HapticDevice == null)
            HapticDevice = (HapticPlugin)FindObjectOfType(typeof(HapticPlugin));

        if( HapticDevice /* STILL */ == null )
            Debug.LogError("This script requires that Haptic Device be assigned.");

        if (depthMeter == null)
            Debug.LogError("This script requires a UI Image be linked to 'depthMeter'");

        /* - Find Component with Raycast - */
        laserPointerComp = GameObject.Find("laserPointer");

        /* - Find Highligh Images over buttons - */
        button1ImageHg = GameObject.Find("Img_HighLight_Bt1");
        button2ImageHg = GameObject.Find("Img_HighLight_Bt2");

        /* - Find Tooge and Labels - */
        toggleMode1  = GameObject.Find("Toggle_Mode_1");
        toggleMode2  = GameObject.Find("Toggle_Mode_2");
        labelToggle1 = GameObject.Find("Label_toggle_1");
        labelToggle2 = GameObject.Find("Label_toggle_2");

        setBehaviorPencil(0);  // Start with Drag and Touch behavior
    }

    // Update is called once per frame
    void Update()
    {
        if (HapticDevice == null)   return;

        /*
         *  "touchingDepth" is a property of haptic devices.
         *  It indicates how far the user has pushed the stylus intot he slighly rubbery surfaces 
         *  of a 'touchable' object.
         */
        if (depthMeter != null)
        {
            // If we're touching the Drums...
            if (HapticDevice.touching == Drums)
            {
                float depth = (1.0f / depthMax) * HapticDevice.touchingDepth;
                depthMeter.fillAmount = depth;
            } 
            else
                depthMeter.fillAmount = 0;
        }

        /*
         * Pressing Button 1 : Vibration
         */
        if (HapticDevice.Buttons [0] == 1)
            button1ImageHg.SetActive(true);
        else
            button1ImageHg.SetActive(false);

        /*
         * Pressing Button 2 : Resizing
         */
        if (HapticDevice.Buttons [1] == 1)
        {
            setBehaviorPencil(1); 
            button2ImageHg.SetActive(true);
        }
        else
        {   
            setBehaviorPencil(0);
            button2ImageHg.SetActive(false);
        }

    }

    void setBehaviorPencil (int mode){

        if (behavior != mode){
            modifyToggle(mode);
            behavior = mode;
        }
    }

    void modifyToggle(int mode){
        if (mode == 0) 
        {
            toggleMode1.GetComponent<Toggle>().isOn = true;
            toggleMode2.GetComponent<Toggle>().isOn = false;
            labelToggle1.GetComponent<Text>().color = Color1;
            labelToggle2.GetComponent<Text>().color = Color2;
            modeResizing(false);
        }
        if (mode == 1)
        {
            toggleMode1.GetComponent<Toggle>().isOn = false;
            toggleMode2.GetComponent<Toggle>().isOn = true;
            labelToggle1.GetComponent<Text>().color = Color2;
            labelToggle2.GetComponent<Text>().color = Color1;
            modeResizing(true);
        }
    }

    void modeResizing(bool isOn){
        var Magnitude = HapticDevice.stylusVelocityRaw.magnitude;
        
        /* - Enable module on other script to modify size - */
        laserPointerComp.GetComponent<raycastLaser>().modeResizing = isOn;
        laserPointerComp.GetComponent<raycastLaser>().HDPosition   = HapticDevice.stylusPositionWorld;
        laserPointerComp.GetComponent<raycastLaser>().HDMagnitude  = Magnitude;
    }
}