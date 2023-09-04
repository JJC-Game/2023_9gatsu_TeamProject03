﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MainMenuManager : MonoBehaviour
{
    [Header("キャンバス")]
    GameObject operateSelectCanvas;
    GameObject[] stageSelectCanvas;

    void Start()
    {
        operateSelectCanvas = GameObject.Find("StageSerect");

        stageSelectCanvas = GameObject.FindGameObjectsWithTag("Stage");

        CanvasInit();
        operateSelectCanvas.SetActive(true);

        SoundManager.Instance.PlayBGM(0);
    }

    void Update()
    {
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

    public void StageSerectCanvasChange(int canvasNo)
    {
        CanvasInit();
        stageSelectCanvas[canvasNo].SetActive(true);

        SoundManager.Instance.PlaySE_Sys(0);
    }

    public void CanvasBack()
    {
        CanvasInit();
        operateSelectCanvas.SetActive(true);

        SoundManager.Instance.PlaySE_Sys(0);
    }

    //シーン遷移
    public void SceneMove(int sceneNo)
    {
        FadeManager.Instance.LoadSceneIndex(sceneNo, 0.5f);

        SoundManager.Instance.PlaySE_Sys(1);
    }

    public void GameStop()
    {
        UnityEditor.EditorApplication.isPlaying = false;
    }
}