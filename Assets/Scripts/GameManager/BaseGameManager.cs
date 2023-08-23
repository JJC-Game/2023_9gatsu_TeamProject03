using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class BaseGameManager : MonoBehaviour
{
    public int timeLimit;
    float timeCurrent;
    Image timer;
    GameObject clockHand;

    public bool inGameEnable = false;
    // COMMENT_KUWABARA 変数名が何の情報も示してないので、ゲームがどうなったフラグなのかを示してほしいです.
    // ソースコードを見る限り、isGameActive、とか、isGameEnableといった名前がいいんじゃないでしょうか.

    public int scorePuls;  //正解時に増えるスコア
    int scoreCurrent;         //現在のスコア
    public int scoreGoal;  //目標のスコア

    Image coinBag;
    float gaugeMax;

    GameObject clearCanvas;
    GameObject overCanvas;

    void Awake()
    {
        scoreCurrent = 0;

        timer = GameObject.Find("Clock").GetComponent<Image>();
        timer.fillAmount = 1;

        clockHand = GameObject.Find("CircleFrame");

        coinBag = GameObject.Find("Square").GetComponent<Image>();
        coinBag.fillAmount = 0;

        gaugeMax = scoreGoal * 1.25f;

        Arrangements();
    }

    void Start()
    {
        clearCanvas = GameObject.Find("ClearCanvas");
        overCanvas = GameObject.Find("OverCanvas");

        clearCanvas.SetActive(false);
        overCanvas.SetActive(false);

        timeCurrent = timeLimit;
    }

    void Update()
    {
        if (inGameEnable)
        {
            timeCurrent -= Time.deltaTime;
            timer.fillAmount = timeCurrent / timeLimit;

            clockHand.transform.rotation = Quaternion.Euler(0, 0, 360 * (timeCurrent / timeLimit));

            if (timeCurrent <= 0)
            {
                timeCurrent = 0;
            }

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
        inGameEnable = true;
        Debug.Log("Start");
    }

    virtual public void TimeUp()
    {
        if (scoreCurrent >= scoreGoal)
        {
            GameClear();
        }
        else
        {
            GameOver();
        }
    }

    public void GameClear()
    {
        clearCanvas.SetActive(true);
        inGameEnable = false;
    }

    public void GameOver()
    {
        overCanvas.SetActive(true);
        inGameEnable = false;
    }

    public void AddScore()
    {
        scoreCurrent += scorePuls;

        coinBag.fillAmount = scoreCurrent / gaugeMax;
    }

    public void LessTime()
    {
        timeCurrent -= 10;
        timer.fillAmount = timeCurrent / timeLimit;

        clockHand.transform.rotation = Quaternion.Euler(0, 0, 360 * (timeCurrent / timeLimit));
    }
    
    //シーン遷移
    public void SceneMove(int sceneNo)
    {
        FadeManager.Instance.LoadSceneIndex(sceneNo, 0.5f);
    }
    
    //シーンリセット
    public void SceneReset()
    {
        FadeManager.Instance.LoadScene(SceneManager.GetActiveScene().name, 0.5f);
    }
}