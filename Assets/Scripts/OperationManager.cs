using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using TMPro;

public class OperationManager : Singleton<OperationManager>
{
    List<OperationFixData> fixDataList;

    bool startFLG;
    int progress = 0;

    TextMeshProUGUI opetrationText;

    GameObject operationCanvas;

    BaseGameManager baseGM;

    void Start()
    {
        LoadFixData();
        OperationTextChange();
    }
    
    void Update()
    {
        
    }

    void LoadFixData()
    {
        TextAsset csvFile;
        csvFile = Resources.Load("FixData/OpetationFixData") as TextAsset;
        StringReader reader = new StringReader(csvFile.text);

        while (reader.Peek() > -1)
        {
            string line = reader.ReadLine();
            string[] elementArray = line.Split(',');
            Debug.Log(line);

            OperationFixData newOperationFixData = new OperationFixData();
            newOperationFixData._id = int.Parse(elementArray[0]);
            newOperationFixData._operationText[0] = elementArray[1];
            newOperationFixData._operationText[1] = elementArray[2];
            newOperationFixData._operationText[2] = elementArray[3];

            fixDataList.Add(newOperationFixData);
            //            csvDatas.Add(line.Split(',')); // リストに入れる
        }
    }

    public void OperationTextChange()
    {
        if (progress >= 0 && progress <= 2)
        {
            opetrationText.text = fixDataList[baseGM.stageNo]._operationText[progress];
            progress++;
        }
        else
        {
            if (!startFLG)
            {
                baseGM.StartDemoPlay();
                startFLG = true;
                progress = 0;
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
        OperationTextChange();
    }
}
