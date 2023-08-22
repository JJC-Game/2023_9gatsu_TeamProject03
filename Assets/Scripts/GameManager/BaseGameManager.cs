using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class BaseGameManager : MonoBehaviour
{
    public int timeLimit;
    float timeCurrent;

    public bool inGameEnable = false;
    // COMMENT_KUWABARA 変数名が何の情報も示してないので、ゲームがどうなったフラグなのかを示してほしいです.
    // ソースコードを見る限り、isGameActive、とか、isGameEnableといった名前がいいんじゃないでしょうか.

    public int scorePuls;  //正解時に増えるスコア
    int scoreCurrent;         //現在のスコア
    public int scoreGoal;  //目標のスコア

    GameObject clearCanvas;
    GameObject overCanvas;

    void Awake()
    {
        scoreCurrent = 0;

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
    }

    public void LessTime()
    {
        timeCurrent -= 10;
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