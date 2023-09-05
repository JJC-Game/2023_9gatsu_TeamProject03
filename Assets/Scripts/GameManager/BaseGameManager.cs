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
    int timeLimit = 90;
    float timeCurrent;
    Image timer;
    GameObject clockHand;
    TextMeshProUGUI timeText;

    public bool inGameEnable = false;
    // COMMENT_KUWABARA 変数名が何の情報も示してないので、ゲームがどうなったフラグなのかを示してほしいです.
    // ソースコードを見る限り、isGameActive、とか、isGameEnableといった名前がいいんじゃないでしょうか.

    [Header("スコア関連")]
    public int coinPulsTimes;  //一問当たりのコイン獲得枚数
    int questionCurrent;         //正解数
    TextMeshProUGUI questionText;

    [Header("デモ演出")]
    [SerializeField] PlayableDirector pd_gameStart;  //ゲームスタートのデモ演出
    [SerializeField] PlayableDirector pd_gameClear;  //ゲームクリアのデモ演出

    void Awake()
    {
        questionCurrent = 0;

        timer = GameObject.Find("Clock").GetComponent<Image>();
        timer.fillAmount = 1;

        clockHand = GameObject.Find("CircleFrame");

        timeText = GameObject.Find("TimelimitNumber").GetComponent<TextMeshProUGUI>();
        timeCurrent = timeLimit;
        timeText.text = timeCurrent.ToString("00");

        questionText = GameObject.Find("QuestionNumber").GetComponent<TextMeshProUGUI>();
        questionText.text = 1.ToString("00");

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

        int coinSum = questionCurrent * coinPulsTimes;

        if (PlayerPrefs.GetInt("StageScoreMax_" + stageNo) < coinSum)
        {
            PlayerPrefs.SetInt("StageScoreMax_" + stageNo, coinSum);
        }

        coinSum += PlayerPrefs.GetInt("StageScore_" + stageNo, 0);
        PlayerPrefs.SetInt("StageScore_" + stageNo, coinSum);
        PlayerPrefs.Save();
    }

    public void AddScore()
    {
        questionCurrent++;
        questionText.text = questionCurrent + 1.ToString("00");

        SoundManager.Instance.PlaySE_Sys(2);
    }

    public void LessTime()
    {
        timeCurrent -= 10;
        timer.fillAmount = timeCurrent / timeLimit;

        clockHand.transform.rotation = Quaternion.Euler(0, 0, 360 * (timeCurrent / timeLimit));

        SoundManager.Instance.PlaySE_Sys(3);
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