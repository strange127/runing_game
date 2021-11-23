using System.Collections;
using System.Collections.Generic;
using UnityEngine;

sealed public class Initialiser : MonoBehaviour
{
    [SerializeField]
    public ScreenOrientation orientation;

    private void Awake()
    {
        //Rotation
        Screen.orientation = orientation;
        switch (orientation)
        {
            case ScreenOrientation.Portrait:
                Screen.autorotateToLandscapeLeft = false;
                Screen.autorotateToLandscapeRight = false;
                Screen.autorotateToPortrait = true;
                Screen.autorotateToPortraitUpsideDown = true;
                break;
            case ScreenOrientation.Landscape:
                Screen.autorotateToLandscapeLeft = true;
                Screen.autorotateToLandscapeRight = true;
                Screen.autorotateToPortrait = false;
                Screen.autorotateToPortraitUpsideDown = false;
                break;

        }

        Screen.orientation = ScreenOrientation.AutoRotation;
        Screen.sleepTimeout = SleepTimeout.NeverSleep;

        Application.targetFrameRate = 60;
    }
}