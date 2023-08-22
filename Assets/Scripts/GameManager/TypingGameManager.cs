using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class TypingGameManager : BaseGameManager
{
    string answerWard;
    public List<string> fixDataList;

    public override void Arrangements()
    {
        LoadFixData();
    }

    void LoadFixData()
    {
        TextAsset csvFile;
        csvFile = Resources.Load("FixData/ItemFixData") as TextAsset;
        StringReader reader = new StringReader(csvFile.text);

        while (reader.Peek() > -1)
        {
            string line = reader.ReadLine();
            string[] elementArray = line.Split(',');
            Debug.Log(line);

            fixDataList.Add(elementArray[0]);
            //            csvDatas.Add(line.Split(',')); // リストに入れる
        }


    }

    public void Decision(string input)
    {
        if (answerWard == input)
        {
            Correct();
        }
        else
        {
            Incorrect();
        }
    }

    void WardChange()
    {

    }

    public void Correct()
    {
        if (inGameEnable)
        {
            AddScore();

            WardChange();
        }
    }

    public void Incorrect()
    {
        LessTime();
    }
}
