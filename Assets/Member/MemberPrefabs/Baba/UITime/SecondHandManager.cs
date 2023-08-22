using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SecondHandManager : MonoBehaviour
{
    public Vector3 secondHandAngle;
    public Vector3 initialRotation; // 初期位置
    float s=0;
    Timer timer;
    // Start is called before the first frame update
    void Start()
    {
        // 初期位置を設定
        GetComponent<Transform>().localEulerAngles = initialRotation;
        GameObject targetObject = GameObject.Find("D");
        timer = targetObject.GetComponent<Timer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (timer.startTime == true)
        {
            s += Time.deltaTime;
            if (s >= 60.0f)
            {
                s = 0;
            }
            secondHandAngle = new Vector3(0, 0, -360 / 60.0f * s);
            GetComponent<Transform>().localEulerAngles = secondHandAngle;
        }

    }
}