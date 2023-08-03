using System.Collections;
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
    }

    //シーン遷移
    public void SceneMove(int sceneNo)
    {
        FadeManager.Instance.LoadSceneIndex(sceneNo, 0.5f);
    }
}
