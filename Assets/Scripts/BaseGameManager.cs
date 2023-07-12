using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BaseGameManager : MonoBehaviour
{
    public int timeLimit;
    float timeCurrent;

    bool gameFLG = false;

    GameObject clearCanvas;
    GameObject overCanvas;

    public TextMeshProUGUI timeText;

    void Awake()
    {
        Arrangements();
    }

    void Start()
    {
        clearCanvas = GameObject.Find("ClearCanvas");
        overCanvas = GameObject.Find("OverCanvas");

        clearCanvas.SetActive(false);
        overCanvas.SetActive(false);

        timeCurrent = timeLimit;
        timeText.text = timeCurrent.ToString("000");
    }

    void Update()
    {
        if (gameFLG)
        {
            timeCurrent -= Time.deltaTime;

            timeText.text = timeCurrent.ToString("000");
            if (timeCurrent >= timeLimit)
            {
                TimeUp();
            }
        }
        
        if (Input.GetKeyDown(KeyCode.Z))
        {
            Arrangements();
        }

        if (Input.GetKeyDown(KeyCode.X))
        {
            gameFLG = true;
        }
    }

    virtual public void Arrangements()
    {
        
    }

    virtual public void TimeUp()
    {
        GameOver();
    }

    public void GameClear()
    {
        clearCanvas.SetActive(true);
    }

    public void GameOver()
    {
        overCanvas.SetActive(true);
    }
}