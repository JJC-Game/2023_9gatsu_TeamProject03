﻿using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using TMPro;

public class OperationManager : Singleton<OperationManager>
{
    public List<OperationFixData> fixDataList;

    bool startFLG;
    int progress = 0;

    TextMeshProUGUI operationText;

    GameObject operationCanvas;
    GameObject choicesCanvas;

    BaseGameManager baseGM;

    void Start()
    {
        baseGM = GameObject.Find("GameManager").GetComponent<BaseGameManager>();
        operationText = GameObject.Find("OperationText").GetComponent<TextMeshProUGUI>();
        operationCanvas = GameObject.Find("OperationCanvas").transform.gameObject;
        choicesCanvas = GameObject.Find("ChoicesCanvas").transform.gameObject;

        LoadFixData(); 
        
        operationText.text = fixDataList[baseGM.stageNo - 1]._operationText[progress];

        operationCanvas.SetActive(false);
        choicesCanvas.SetActive(true);
    }
    
    void Update()
    {
        
    }

    void LoadFixData()
    {
        TextAsset csvFile;
        csvFile = Resources.Load("FixData/OperationText") as TextAsset;
        StringReader reader = new StringReader(csvFile.text);

        while (reader.Peek() > -1)
        {
            string line = reader.ReadLine();
            string[] elementArray = line.Split(',');
            Debug.Log(line);

            OperationFixData newOperationFixData = new OperationFixData();
            newOperationFixData._id = int.Parse(elementArray[0]);

            if (elementArray[1].Contains(@"\n"))
            {
                elementArray[1] = elementArray[1].Replace(@"\n", System.Environment.NewLine);
            }
            if (elementArray[2].Contains(@"\n"))
            {
                elementArray[2] = elementArray[2].Replace(@"\n", System.Environment.NewLine);
            }
            if (elementArray[3].Contains(@"\n"))
            {
                elementArray[3] = elementArray[3].Replace(@"\n", System.Environment.NewLine);
            }

            newOperationFixData._operationText[0] = elementArray[1];
            newOperationFixData._operationText[1] = elementArray[2];
            newOperationFixData._operationText[2] = elementArray[3];

            fixDataList.Add(newOperationFixData);
        }
    }

    public void OperationTextBack()
    {
        if (progress >= 1 && progress <= 3)
        {
            progress--;
            operationText.text = fixDataList[baseGM.stageNo - 1]._operationText[progress];

            SoundManager.Instance.PlaySE_Sys(0);
        }
        else
        {
            if (!startFLG)
            {
                baseGM.SceneMove(1);
            }
            else
            {
                operationCanvas.SetActive(false);
                progress = 0;
            }
        }
    }

    public void OperationTextChange()
    {
        if (progress >= 0 && progress < 2)
        {
            progress++;
            operationText.text = fixDataList[baseGM.stageNo - 1]._operationText[progress];

            SoundManager.Instance.PlaySE_Sys(0);
        }
        else
        {
            if (!startFLG)
            {
                operationCanvas.SetActive(false);
                GameStart();
            }
            else
            {
                operationCanvas.SetActive(false);
                progress = 0;
            }
        }
    }

    public void TextDisplay()
    {
        operationCanvas.SetActive(true);
        operationText.text = fixDataList[baseGM.stageNo - 1]._operationText[progress];
    }

    void GameStart()
    {
        startFLG = true;
        progress = 0;
        baseGM.StartDemoPlay();
    }

    public void Choice_Yes()
    {
        choicesCanvas.SetActive(false);
        operationCanvas.SetActive(true);

        SoundManager.Instance.PlaySE_Sys(0);
    }

    public void Choice_No()
    {
        choicesCanvas.SetActive(false);
        GameStart();
    }
}
