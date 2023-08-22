using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class TypingGameManager : BaseGameManager
{
    string answerWord;
    List<string> fixDataList = new List<string>();

    public override void Arrangements()
    {
        LoadFixData();
        WordChange();
    }

    void LoadFixData()
    {
        TextAsset csvFile;
        csvFile = Resources.Load("FixData/TypingWordList") as TextAsset;
        StringReader reader = new StringReader(csvFile.text);

        while (reader.Peek() > -1)
        {
            string line = reader.ReadLine();
            fixDataList.Add(line);
        }
    }

    public void Decision(string input)
    {
        if (answerWord == input)
        {
            Correct();
        }
        else
        {
            Incorrect();
        }
    }

    void WordChange()
    {
        int wordNo = Random.Range(0, fixDataList.Count);
        answerWord = fixDataList[wordNo];
    }

    public void Correct()
    {
        if (inGameEnable)
        {
            AddScore();

            WordChange();
        }
    }

    public void Incorrect()
    {
        if (inGameEnable)
        {
            LessTime();
        }
    }
}
