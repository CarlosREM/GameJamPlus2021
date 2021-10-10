using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    public CameraEffects blur;
    public float levelTime;
    public float blurValue;
    public bool setActive;

    void Start()
    {
        blur.SetDepth(blurValue, levelTime);
    }

    void Update()
    {

    }
}
