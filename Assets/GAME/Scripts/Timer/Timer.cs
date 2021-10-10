using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    public CameraEffects blur;
    public float levelTime;
    public float blurValue;
    private float coolDown;
    public bool setActive;
    public float coolDownT;
    public float blurIn;

    void Start()
    {
        levelTime = Time.time + levelTime;
        coolDown = Time.time + coolDownT;
    }

    void Update()
    {
        if (setActive && Time.time < levelTime && Time.time > coolDown)
        {
            Debug.Log("Borroseado");
            coolDown = Time.time + coolDownT;
            //blurValue = blurValue + blurIn;
            blur.SetDepth(blurValue, 8f);

        }else if (Time.time > levelTime)
        {
            Debug.Log("Se acaba el tiempo");
        }
    }
}
