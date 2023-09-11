using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MainMenuManager : MonoBehaviour
{
    GameObject operateSelectCanvas;
    GameObject[] stageSelectCanvas = new GameObject[3];
    GameObject creditCanvas;
    GameObject checkCanvas;

    public ScoreDisplay[] stageScore;
    public GameObject[] coinEffect;

    int stageNo = -1;

    int operateNo = 0;

    void Start()
    {
        operateSelectCanvas = GameObject.Find("StageSelectCanvas");
        creditCanvas = GameObject.Find("CreditCanvas");

        stageSelectCanvas[0] = GameObject.Find("TapCanvas");
        stageSelectCanvas[1] = GameObject.Find("SwipeCanvas");
        stageSelectCanvas[2] = GameObject.Find("DragCanvas");

        checkCanvas = GameObject.Find("CheckCanvas");

        CanvasInit();
        operateSelectCanvas.SetActive(true);

        EffectInit();

        SoundManager.Instance.PlayBGM(0);
    }

    void Update()
    {
        if (stageNo >= 0)
        {
            if (stageScore[stageNo].textureId == 1 && !coinEffect[0].activeSelf)
            {
                coinEffect[0].SetActive(true);
            }
            else if (stageScore[stageNo].textureId == 3 || stageScore[stageNo].textureId == 2 && !coinEffect[1].activeSelf)
            {
                coinEffect[1].SetActive(true);
            }
            else if (stageScore[stageNo].textureId == 4 && !coinEffect[2].activeSelf)
            {
                coinEffect[2].SetActive(true);
            }
        }
    }

    //すべてのキャンバスを非表示に
    void CanvasInit()
    {
        operateSelectCanvas.SetActive(false);
        for (int i = 0; i < stageSelectCanvas.Length; i++)
        {
            stageSelectCanvas[i].SetActive(false);
        }
        creditCanvas.SetActive(false);
        checkCanvas.SetActive(false);
    }

    void EffectInit()
    {
        for (int i = 0; i < coinEffect.Length; i++)
        {
            coinEffect[i].SetActive(false);
        }
        stageNo = -1;
    }

    public void StageSerectCanvasChange(int canvasNo)
    {
        CanvasInit();
        stageSelectCanvas[canvasNo].SetActive(true);

        stageNo = canvasNo;
        Debug.Log(stageNo);

        SoundManager.Instance.PlaySE_Sys(0);
    }

    public void CreditCanvasOpen()
    {
        CanvasInit();
        creditCanvas.SetActive(true);

        SoundManager.Instance.PlaySE_Sys(0);
    }

    public void ScoreResetCheck(int n)
    {
        operateNo = n;

        checkCanvas.SetActive(true);

        SoundManager.Instance.PlaySE_Sys(0);
    }

    public void ScoreReset(bool flg)
    {
        if (flg)
        {
            for (int i = 0; i < 2; i++)
            {
                PlayerPrefs.SetInt("StageScore_" + operateNo, 0);
                operateNo++;
            }
        }
        else
        {
            operateNo = 0;

        }

        EffectInit();
        checkCanvas.SetActive(false);
        SoundManager.Instance.PlaySE_Sys(0);
    }

    public void CanvasBack()
    {
        CanvasInit();
        operateSelectCanvas.SetActive(true);

        EffectInit();

        SoundManager.Instance.PlaySE_Sys(5);
    }

    //シーン遷移
    public void SceneMove(int sceneNo)
    {
        FadeManager.Instance.LoadSceneIndex(sceneNo, 0.5f);

        SoundManager.Instance.PlaySE_Sys(1);
    }

    public void GameStop()
    {
        Application.runInBackground = false;
        Application.Quit();
        return;
    }
}