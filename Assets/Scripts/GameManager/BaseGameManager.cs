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

    public bool inGameEnable = false;
    // COMMENT_KUWABARA 変数名が何の情報も示してないので、ゲームがどうなったフラグなのかを示してほしいです.
    // ソースコードを見る限り、isGameActive、とか、isGameEnableといった名前がいいんじゃないでしょうか.

    [Header("スコア関連")]
    public int scorePuls;  //正解時に増えるスコア
    int scoreCurrent;         //現在のスコア
    public int scoreGoal;  //目標のスコア
    public TextMeshProUGUI scoreCurrentText;
    public TextMeshProUGUI scoreHighText;

    Image coinBag;
    float gaugeMax;

    GameObject clearCanvas;
    GameObject overCanvas;

    [Header("デモ演出")]
    [SerializeField] PlayableDirector pd_gameStart;  //ゲームスタートのデモ演出
    [SerializeField] PlayableDirector pd_gameClear;  //ゲームクリアのデモ演出

    void Awake()
    {
        scoreCurrent = 0;

        timer = GameObject.Find("Clock").GetComponent<Image>();
        timer.fillAmount = 1;

        clockHand = GameObject.Find("CircleFrame");

        //coinBag = GameObject.Find("Square").GetComponent<Image>();
        //coinBag.fillAmount = 0;

        //gaugeMax = scoreGoal * 1.25f;

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

        if (PlayerPrefs.GetInt("StageScore_" + stageNo) < scoreCurrent)
        {
            PlayerPrefs.SetInt("StageScore_" + stageNo, scoreCurrent);
            PlayerPrefs.Save();
        }
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