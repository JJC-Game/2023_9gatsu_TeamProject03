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
    [SerializeField] GameObject canvas;

    BaseGameManager baseGM;

    void Start()
    {
        //初期化
        canvas.SetActive(false);

        baseGM = GameObject.Find("GameManager").GetComponent<BaseGameManager>();
    }

    void Update()
    {
        
    }

    //ポーズ処理
    public void ChangePause(bool flg)
    {
        //キャンバス全部消す
        canvas.SetActive(false);

        pauseFLG = flg;

        //ポーズ中だったら時間停止
        if (flg)
        {
            baseGM.inGameEnable = false;

            Time.timeScale = 0;
            canvas.SetActive(true);
        }
        else
        {
            Time.timeScale = 1;

            baseGM.inGameEnable = true;
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