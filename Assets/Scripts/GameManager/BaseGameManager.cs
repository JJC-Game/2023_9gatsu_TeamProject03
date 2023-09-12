using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.Playables;

public class BaseGameManager : MonoBehaviour
{
    public int stageNo;

    [Header("制限時間関連")]
    int timeLimit = 60;
    float timeCurrent;
    Image timer;
    GameObject clockHand;
    TextMeshProUGUI timeText;

    public bool inGameEnable = false;
    // COMMENT_KUWABARA 変数名が何の情報も示してないので、ゲームがどうなったフラグなのかを示してほしいです.
    // ソースコードを見る限り、isGameActive、とか、isGameEnableといった名前がいいんじゃないでしょうか.

    [Header("スコア関連")]
    int coinPulsTimes;  //一問当たりのコイン獲得枚数
    int questionCurrent;         //正解数
    TextMeshProUGUI questionNoText;

    //ゲーム終了時
    TextMeshProUGUI clearNoText;
    TextMeshProUGUI getCoinText_Current;
    TextMeshProUGUI getCoinText_Max;

    [Header("デモ演出")]
    [SerializeField] PlayableDirector pd_gameStart;  //ゲームスタートのデモ演出
    [SerializeField] PlayableDirector pd_gameClear;  //ゲームクリアのデモ演出
    [SerializeField] PlayableDirector pd_correct;  //ゲームスタートのデモ演出
    [SerializeField] PlayableDirector pd_incorrect;  //ゲームクリアのデモ演出

    bool sceneMove;

    void Awake()
    {
        sceneMove = false;

        questionCurrent = 1;

        timer = GameObject.Find("Clock").GetComponent<Image>();
        timer.fillAmount = 1;

        clockHand = GameObject.Find("CircleFrame");

        timeText = GameObject.Find("TimelimitNumber").GetComponent<TextMeshProUGUI>();
        timeCurrent = timeLimit;
        timeText.text = timeCurrent.ToString("00");

        questionNoText = GameObject.Find("QuestionNumber").GetComponent<TextMeshProUGUI>();
        questionNoText.text = 1.ToString("00");

        clearNoText = GameObject.Find("ClearNumber").GetComponent<TextMeshProUGUI>();
        getCoinText_Current = GameObject.Find("GetCoinNumber_Current").GetComponent<TextMeshProUGUI>();
        getCoinText_Max = GameObject.Find("GetCoinNumber_Max").GetComponent<TextMeshProUGUI>();

        GameObject.Find("ClearCanvas").SetActive(false);

        if (stageNo == 1)
        {
            coinPulsTimes = 200;
        }
        else if(stageNo == 2)
        {
            coinPulsTimes = 1000;
        }
        else if(stageNo == 3)
        {
            coinPulsTimes = 800;
        }
        else if (stageNo == 4)
        {
            coinPulsTimes = 500;
        }
        else
        {
            coinPulsTimes = 1000;
        }

        Arrangements();
    }

    void Update()
    {
        if (inGameEnable)
        {
            timeCurrent -= Time.deltaTime;
            timer.fillAmount = timeCurrent / timeLimit;

            clockHand.transform.rotation = Quaternion.Euler(0, 0, 360 * (timeCurrent / timeLimit));

            timeText.text = timeCurrent.ToString("00");

            if (timeCurrent <= 0)
            {
                timeCurrent = 0;
                timer.fillAmount = timeCurrent / timeLimit;
                clockHand.transform.rotation = Quaternion.Euler(0, 0, 360 * (timeCurrent / timeLimit));
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

    public void StartDemoPlay()
    {
        //スタートデモを再生
        pd_gameStart.Play();
    }

    public void GameStart()
    {
        inGameEnable = true;
        Debug.Log("Start");

        SoundManager.Instance.PlayBGM(0);
    }

    public void TimeUp()
    {
        GameClear();
    }

    public void GameClear()
    {
        inGameEnable = false;

        pd_gameClear.Play();

        questionCurrent--;

        int coinSum = questionCurrent * coinPulsTimes;

        if (PlayerPrefs.GetInt("StageScoreMax_" + stageNo, 0) < coinSum)
        {
            PlayerPrefs.SetInt("StageScoreMax_" + stageNo, coinSum);
        }

        clearNoText.text = questionCurrent.ToString("00");
        getCoinText_Current.text = coinSum.ToString("0000");

        coinSum += PlayerPrefs.GetInt("StageScore_" + stageNo, 0);
        PlayerPrefs.SetInt("StageScore_" + stageNo, coinSum);
        PlayerPrefs.Save();

        getCoinText_Max.text = PlayerPrefs.GetInt("StageScoreMax_" + stageNo, 0).ToString("0000");
    }

    public void AddScore()
    {
        questionCurrent++;
        questionNoText.text = questionCurrent.ToString("00");

        pd_correct.Play();
        SoundManager.Instance.PlaySE_Sys(2);
    }

    public void LessTime()
    {
        timeCurrent -= 5;

        if (timeCurrent <= 0)
        {
            timeCurrent = 0;
        }

        timer.fillAmount = timeCurrent / timeLimit;

        clockHand.transform.rotation = Quaternion.Euler(0, 0, 360 * (timeCurrent / timeLimit));

        pd_incorrect.Play();
        SoundManager.Instance.PlaySE_Sys(3);
    }
    
    //シーン遷移
    public void SceneMove(int sceneNo)
    {
        if (!sceneMove)
        {
            FadeManager.Instance.LoadSceneIndex(sceneNo, 0.5f);

            sceneMove = true;

            SoundManager.Instance.PlaySE_Sys(1);
        }
    }
    
    //シーンリセット
    public void SceneReset()
    {
        FadeManager.Instance.LoadScene(SceneManager.GetActiveScene().name, 0.5f);
    }
}