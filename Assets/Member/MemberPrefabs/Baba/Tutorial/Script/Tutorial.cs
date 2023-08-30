using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    Timer timer;
    [SerializeField]
    public string Game = "Drag";
    DragFixData dragFixData;
    RangeCheck rangeCheck;
    public GameObject StartTutorialText;
    // Start is called before the first frame update
    void Start()
    {
        GameObject targetObject = GameObject.Find("D");
        timer = targetObject.GetComponent<Timer>();
        switch (Game)
        {
            case "Drag":
                TutorialTime();
                break;
            case "Pinthi":
                TutorialTime();
                break;
            default:

                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        TutorialBifurcation();
    }
    void TutorialBifurcation()
    {
        switch (Game)
        {
            case "Drag":
                DragTutorial();
                break;
            case "Pinthi":
                PinchiTutorial();
                break;
            default:

                break;
        }
    }
    private void TutorialTime()
    {
        timer.countdown = 3600;
    }
    private void DragTutorial()
    {
        if (!dragFixData)
        {
            GameObject targetObject = GameObject.Find("D");
            dragFixData = targetObject.GetComponent<DragFixData>();
        }
        if (dragFixData.nowNumber >= 4)
        {
            timer.startTime = false;       
              //  int id = 1;
              //  timer.timeline.EventPlay(id);            
        }
        if (Input.GetMouseButton(0)&& StartTutorialText)
        {
            StartTutorialText.SetActive(false);
            timer.startTime = true;
        }
    }
    private void PinchiTutorial()
    {
        if (!rangeCheck)
        {
            GameObject targetObject = GameObject.Find("AnswerArea");
            rangeCheck = targetObject.GetComponent<RangeCheck>();
        }
        if (rangeCheck.correctCount >= 4)
        {
            timer.startTime = false;
            //int id = 1;
           // timer.timeline.EventPlay(id);
        }
        if (Input.GetMouseButton(0) && StartTutorialText)
        {
            StartTutorialText.SetActive(false);
            timer.startTime = true;
        }
    }
}
