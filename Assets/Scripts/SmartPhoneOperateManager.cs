using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using TMPro;

public class SmartPhoneOperateManager : MonoBehaviour
{
    public List<SmartPhoneOperateFixData> fixDataList;

    public int stageNo;
    int progress = 0;

    TextMeshProUGUI operationText;

    GameObject buttonClose;
    GameObject nextButton;
    GameObject endButton;

    void Start()
    {
        operationText = GameObject.Find("OperationText").GetComponent<TextMeshProUGUI>();

        buttonClose = GameObject.Find("ButtonClose");
        nextButton = GameObject.Find("NextButton");
        endButton = GameObject.Find("EndButton");

        endButton.SetActive(false);

        LoadFixData();
        operationText.text = fixDataList[stageNo]._operationText[progress];
    }

    void Update()
    {

    }

    void LoadFixData()
    {
        TextAsset csvFile;
        csvFile = Resources.Load("FixData/SmartPhoneOperateText") as TextAsset;
        StringReader reader = new StringReader(csvFile.text);

        while (reader.Peek() > -1)
        {
            string line = reader.ReadLine();
            string[] elementArray = line.Split(',');
            Debug.Log(line);

            SmartPhoneOperateFixData newOperationFixData = new SmartPhoneOperateFixData();
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

    public void OperationTextNext()
    {
        if (progress >= 0 && progress < 2)
        {
            progress++;
            operationText.text = fixDataList[stageNo]._operationText[progress];

            if (progress > 1)
            {
                nextButton.SetActive(false);
                endButton.SetActive(true);
            }
        }

        if (buttonClose.activeSelf)
        {
            buttonClose.SetActive(false);
        }

        SoundManager.Instance.PlaySE_Game(0);
    }

    public void OperationTextBack()
    {
        if (progress >= 0 && progress <= 2)
        {
            progress--;
            operationText.text = fixDataList[stageNo]._operationText[progress];

            if (progress <= 0)
            {
                buttonClose.SetActive(true);
            }
            if (!nextButton.activeSelf)
            {
                nextButton.SetActive(true);
                endButton.SetActive(false);
            }
        }

        SoundManager.Instance.PlaySE_Game(0);
    }

    public void SceneMove()
    {
        FadeManager.Instance.LoadSceneIndex(1, 0.5f);

        SoundManager.Instance.PlaySE_Sys(3);
    }
}
