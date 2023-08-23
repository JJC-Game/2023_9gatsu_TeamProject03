﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    PinchScore pinchScore;
    GameObject targetObject;
    //カウントダウン
    public float countdown = 60.0f;
    //タイムリミット
    private float timeLimit = 0.0f;

    public bool startTime=false;
    public bool end=false;
    public GameObject StartText;
    public TimeLine timeline;
    bool startTimeLine=false;
    float time=4;
    // Update is called once per frame
    private void Awake()
    {
         targetObject = GameObject.Find("D");
        timeline = targetObject.GetComponent<TimeLine>();
        if (targetObject.GetComponent<PinchScore>())
        {
            pinchScore = targetObject.GetComponent<PinchScore>();
        }
    }
    void Update()
    {
        if (startTime == true)
        {
            //時間をカウントする
            countdown -= Time.deltaTime;
        }
        else
        {
           // countdown = 10f;
        }
        if (countdown <= timeLimit)
        {
            end = true;
            startTime = false;
            int id = 1;
            timeline.EventPlay(id);
            Debug.Log("時間になりました！");
        }
        StartTime();
        StartTimes();
    }
    public void StartTime()
    {
        if (time <= 0)
        {
            startTime = true;            
            Debug.Log("時間になりました！");
            GameStart();
        }
    }
    public void StratTimeline()
    {
        StartText.SetActive(false);
        int id = 0;
        timeline.EventPlay(id);
        startTimeLine = true;
    }
    public void StartTimes()
    {
        if(startTimeLine == true)
        {
            time -= Time.deltaTime;
        }
    }
    private void GameStart()
    {
        if (targetObject.GetComponent<PinchScore>())
        {
            pinchScore.inGameEnable = true;
        }
    }
}
