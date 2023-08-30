﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleManager : MonoBehaviour
{
    void Start()
    {
        SoundManager.Instance.PlayBGM(0);
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            FadeManager.Instance.LoadSceneIndex(1, 0.5f);
        }
    }
}
