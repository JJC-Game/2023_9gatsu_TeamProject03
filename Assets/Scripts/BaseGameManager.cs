using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BaseGameManager : MonoBehaviour
{
    public int timeLimit;
    float timeCurrent;

    public bool gameFLG = false;
    // COMMENT_KUWABARA 変数名が何の情報も示してないので、ゲームがどうなったフラグなのかを示してほしいです.
    // ソースコードを見る限り、isGameActive、とか、isGameEnableといった名前がいいんじゃないでしょうか.

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
        timeText.text = timeCurrent.ToString("00");
    }

    void Update()
    {
        if (gameFLG)
        {
            timeCurrent -= Time.deltaTime;

            if (timeCurrent <= 0)
            {
                timeCurrent = 0;
            }

            timeText.text = timeCurrent.ToString("00");
            if (timeCurrent <= 0)
            {
                TimeUp();
            }

            UpdatePlus();
        }
    }

    virtual public void Arrangements()
    {
        
    }

    virtual public void UpdatePlus()
    {

    }

    public void GameStart()
    {
        gameFLG = true;
    }

    virtual public void TimeUp()
    {
        GameOver();
    }

    public void GameClear()
    {
        clearCanvas.SetActive(true);
        gameFLG = false;
    }

    public void GameOver()
    {
        overCanvas.SetActive(true);
        gameFLG = false;
    }

    public void LessTime()
    {
        timeCurrent -= 10;
    }
}