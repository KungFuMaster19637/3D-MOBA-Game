using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamSwitchManager : MonoBehaviour
{

    public CameraFollow camFollowScript;
    public CameraRoam camRoamScript;

    bool camViewChanged = false;

    void Start()
    {
        camRoamScript.enabled = false;
    }

    void Update()
    {
        if (camViewChanged == false)
        {
            if (Input.GetKeyDown(KeyCode.Y))
            {
                camViewChanged = true;

                camRoamScript.enabled = true;
                camFollowScript.enabled = false;
            }
        }

        else if (camViewChanged == true)
        {
            if (Input.GetKeyDown(KeyCode.Y))
            {
                camViewChanged = false;

                camRoamScript.enabled = false;
                camFollowScript.enabled = true;
            }

        }
    }
}
