using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using TMPro;

public class TypingGameManager : BaseGameManager
{
    string answerWord;
    List<string> fixDataList = new List<string>();

    TextMeshProUGUI questionText;

    GameObject blind;

    public override void Arrangements()
    {
        questionText = GameObject.Find("QuestionText").GetComponent<TextMeshProUGUI>();
        blind = GameObject.Find("Blind");
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

    public override void UpdatePlus()
    {
        if (inGameEnable && blind.activeSelf)
        {
            blind.SetActive(false);
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

        questionText.text = answerWord;
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
