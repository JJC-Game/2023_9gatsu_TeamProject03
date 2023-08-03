using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MainMenuManager : MonoBehaviour
{
    [Header("キャンバス")]
    GameObject operateSelectCanvas;
    GameObject[] stageSelectCanvas;

    [Header("ポーズメニューのカーソル初期位置")]
    [SerializeField] GameObject focusPausemenu;

    //フォーカスが外れないようにする処理用
    GameObject currentFocus;   //現在
    GameObject previousFocus;  //前フレーム

    void Start()
    {
        operateSelectCanvas = GameObject.Find("");

        for (int i = 0; i <= 5; i++)
        {
            stageSelectCanvas[i] = GameObject.Find("" + i);
        }

        CanvasInit();
        operateSelectCanvas.SetActive(true);
    }

    void Update()
    {
        FocusCheck();
    }

    //すべてのキャンバスを非表示に
    void CanvasInit()
    {
        operateSelectCanvas.SetActive(false);
        for (int i = 0; i < stageSelectCanvas.Length; i++)
        {
            stageSelectCanvas[i].SetActive(false);
        }
    }

    //フォーカスが外れていないかチェック
    void FocusCheck()
    {
        //現在のフォーカスを格納
        currentFocus = EventSystem.current.currentSelectedGameObject;

        //もし前回までのフォーカスと同じなら即終了
        if (currentFocus == previousFocus) return;

        //もしフォーカスが外れていたら前フレームのフォーカスに戻す
        if (currentFocus == null)
        {
            EventSystem.current.SetSelectedGameObject(previousFocus);
            return;
        }

        //残された条件から、フォーカスが存在するのは確定
        //前フレームのフォーカスを更新
        previousFocus = EventSystem.current.currentSelectedGameObject;
    }

    public void StageSerectCanvasChange(int canvasNo)
    {
        CanvasInit();
        stageSelectCanvas[canvasNo].SetActive(true);
    }

    //シーン遷移
    public void SceneMove(int sceneNo)
    {
        FadeManager.Instance.LoadSceneIndex(sceneNo, 0.5f);
    }
}
