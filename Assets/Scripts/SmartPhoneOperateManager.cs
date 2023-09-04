using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using TMPro;

public class SmartPhoneOperateManager : MonoBehaviour
{
    public List<SmartPhoneOperateFixData> fixDataList;

    int stageNo;
    int progress = 0;

    TextMeshProUGUI operationText;

    void Start()
    {
        operationText = GameObject.Find("OperationText").GetComponent<TextMeshProUGUI>();

        LoadFixData();
        OperationTextChange();
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

    public void OperationTextChange()
    {
        if (progress >= 0 && progress <= 2)
        {
            operationText.text = fixDataList[stageNo]._operationText[progress];
            progress++;
        }
        else
        {
            FadeManager.Instance.LoadSceneIndex(1, 0.5f);
        }
    }
}
