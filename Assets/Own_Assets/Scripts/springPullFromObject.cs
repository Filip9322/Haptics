using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class springPullFromObject : MonoBehaviour
{
    public  HapticPlugin HapticDevice    = null;
    public  Vector3 pullDir, laserPos;
    public  bool pullingOn   = false;
    private bool activeEffect;
    private int  FXID = -1;

    // Start is called before the first frame update
    void Start()
    {
        pullingOn = false;
        if (HapticDevice == null)
            HapticDevice = (HapticPlugin)FindObjectOfType(typeof(HapticPlugin));
        
        if( HapticDevice /* STILL */ == null )
            Debug.LogError("This script requires that Haptic Device be assigned.");
    }

    public void TurnEffectOn()
    {
        if (HapticDevice == null) return;       //If there is no device, bail out early.

        // If a haptic effect has not been assigned through Open Haptics, assign one now.
        if (FXID == -1)
        {
            FXID = HapticPlugin.effects_assignEffect(HapticDevice.configName);

            if (FXID == -1) // Still broken?
            {
                Debug.LogError("Unable to assign Haptic effect.");
                return;
            }
        }

        // Send the effect settings to OpenHaptics.
        double[] pos = {laserPos.x, laserPos.y, laserPos.z}; // Position (not used for vibration)
        double[] dir = {pullDir.x, pullDir.y, pullDir.z}; // Direction of vibration

        HapticPlugin.effects_settings(
            HapticDevice.configName,
            FXID,
            0.5, // Gain
            0.3, // Magnitude
            0,  // Frequency
            pos,  // Position (not used for vibration)
            dir); //Direction.
        
        HapticPlugin.effects_type( HapticDevice.configName, FXID,0 ); // Vibration effect == 4

        HapticPlugin.effects_startEffect(HapticDevice.configName, FXID );
    }

    public void TurnEffectOff()
    {
        if (HapticDevice == null) return;       //If there is no device, bail out early.
        if (FXID == -1) return;                 //If there is no effect, bail out early.

        //sphrAnimationOFF();
        HapticPlugin.effects_stopEffect(HapticDevice.configName, FXID );
    }

    void OnDestroy()
    {
        TurnEffectOff();
    }

    void OnApplicationQuit()
    {
        TurnEffectOff();
    }

    void OnDisable()
    {
        TurnEffectOff();
    }


    void Update()
    {
        // If there's no haptic device, bail out early.
        if (HapticDevice == null)
            return;

        //bool buttonState = (HapticDevice.Buttons [0] != 0);

        //If the Button is on and the vibration isn't, or vice-versa
        if (pullingOn != activeEffect)
        {
            activeEffect = pullingOn;
            //sphrAnimationON();
            //If we've just turned it ON
            if (pullingOn)
                { 
                TurnEffectOn();
            } else
                {TurnEffectOff();}
        }

    }
}
