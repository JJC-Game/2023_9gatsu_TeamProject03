using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class PinchScore : BaseGameManager
{
    PinchZoom pinchZoom;
    Timer timer;
    public GameObject pinchZoomObject;
    public GameObject timerObject;
    public int newScore;
    public int nowScore;
    public TextMeshProUGUI[] GameScoreText;
    public TextMeshProUGUI[] GameHiScoreText;
    public TextMeshProUGUI[] GameObjectiveScoreText;
    public override void UpdatePlus()
    {
        if (pinchZoom.generatedImageCount > newScore)
        {
            AddScore();
            newScore++;
        }
        
    }
    public override void Arrangements()
    {
        pinchZoom = pinchZoomObject.GetComponent<PinchZoom>();
        timer = timerObject.GetComponent<Timer>();
        newScore = pinchZoom.generatedImageCount;
        newScore++;
        scorePuls = 1;
        scoreGoal = 2;
        timeLimit = 15;
        int a1 = PlayerPrefs.GetInt("HiPinchScore1");
        Debug.Log(a1);
    }
    public override void TimeUp()
    {
        nowScore = newScore - 1;
        base.TimeUp();
        int a1 = PlayerPrefs.GetInt("HiPinchScore1");
        Debug.Log(a1+"<="+nowScore);
        if (a1<= nowScore)
        {
            Debug.Log( "aiueo" );
            PlayerPrefs.SetInt("HiPinchScore1", nowScore);
            PlayerPrefs.Save();
          a1 = PlayerPrefs.GetInt("HiPinchScore1");
            Debug.Log(a1);
        }
        GameScoreText[0].text = "" + nowScore;
        GameScoreText[1].text = "" + nowScore;
        GameHiScoreText[0].text = "" + a1;
        GameHiScoreText[1].text = "" + a1;
        GameObjectiveScoreText[0].text = "" + scoreGoal;
        GameObjectiveScoreText[1].text = "" + scoreGoal;
    }
}
