using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleManager : MonoBehaviour
{
    bool sceneMove;

    void Start()
    {
        SoundManager.Instance.PlayBGM(0);
        sceneMove = false;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !sceneMove)
        {
            sceneMove = true;

            SoundManager.Instance.PlaySE_Game(0);

            FadeManager.Instance.LoadSceneIndex(1, 0.5f);
        }
    }
}
