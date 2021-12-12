using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class vibratingEffect : MonoBehaviour
{
    public HapticPlugin HapticDevice    = null;
    private bool vibrationOn;
    private int FXID = -1;

    // Start is called before the first frame update
    void Start()
    {
        vibrationOn = false;
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
        double[] pos = {0.0, 0.0, 0.0}; // Position (not used for vibration)
        double[] dir = {1.0, 0.0, 0.0}; // Direction of vibration

        HapticPlugin.effects_settings(
            HapticDevice.configName,
            FXID,
            0.63, // Gain
            0.63, // Magnitude
            40,  // Frequency
            pos,  // Position (not used for vibration)
            dir); //Direction.
        
        HapticPlugin.effects_type( HapticDevice.configName, FXID,4 ); // Vibration effect == 4

        HapticPlugin.effects_startEffect(HapticDevice.configName, FXID );
    }

    public void TurnEffectOff()
    {
        if (HapticDevice == null) return;       //If there is no device, bail out early.
        if (FXID == -1) return;                 //If there is no effect, bail out early.

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

    // Update is called once per frame
    void Update()
    {
        // If there's no haptic device, bail out early.
        if (HapticDevice == null)
            return;

        //If the Button is on and the vibration isn't, or vice-versa
        if (HapticDevice.Buttons [0] != 0)
        {
            TurnEffectOn();
        } else
        {
            TurnEffectOff();
        }
    }
}
