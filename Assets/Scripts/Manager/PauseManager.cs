using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    //フラグ
    public bool pauseFLG;    //ポーズ中

    [Header("キャンバス")]
    [SerializeField] GameObject[] canvas;

    BaseGameManager baseGM;

    void Start()
    {
        //初期化
        CanvasInit();

        //メインメニューだけアクティブ
        canvas[0].SetActive(true);

        baseGM = GameObject.Find("GameManager").GetComponent<BaseGameManager>();
    }

    void Update()
    {
        
    }

    //ポーズ処理
    public void ChangePause(bool flg)
    {
        //キャンバス全部消す
        CanvasInit();

        pauseFLG = flg;

        //ポーズ中だったら時間停止
        if (flg)
        {
            baseGM.inGameEnable = false;

            Time.timeScale = 0;
            canvas[1].SetActive(true);
        }
        else
        {
            Time.timeScale = 1;
            canvas[0].SetActive(true);

            baseGM.inGameEnable = true;
        }
    }

    //すべてのキャンバスを非表示に
    void CanvasInit()
    {
        for (int i = 0; i < canvas.Length; i++)
        {
            canvas[i].SetActive(false);
        }
    }

    //シーンをリスタート
    public void Restart()
    {
        //timeScaleをもとに戻してから
        ChangePause(false);

        baseGM.SceneReset();
    }

    public void SceneMove(int no)
    {
        //timeScaleをもとに戻してから
        ChangePause(false);

        baseGM.SceneMove(no);
    }
}