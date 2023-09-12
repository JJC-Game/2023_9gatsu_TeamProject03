using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TypingGameManager : BaseGameManager
{
    string answerWord;
    string currentWord = "";
    List<string> fixDataList = new List<string>();

    TextMeshProUGUI questionText;

    GameObject blind;

    InputField inputField;

    public override void Arrangements()
    {
        questionText = GameObject.Find("QuestionText").GetComponent<TextMeshProUGUI>();
        blind = GameObject.Find("Blind");
        inputField = GameObject.Find("InputField (Legacy)").GetComponent<InputField>();
        inputField.enabled = false;
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
        if (inGameEnable)
        {
            if (blind.activeSelf)
            {
                blind.SetActive(false);
            }

            if (!inputField.enabled)
            {
                inputField.enabled = true;
            }
        }
        else
        {
            if (!blind.activeSelf)
            {
                blind.SetActive(true);
            }

            if (inputField.enabled)
            {
                inputField.enabled = false;
            }
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

    public void WordChange()
    {
        int wordNo = Random.Range(0, fixDataList.Count);
        answerWord = fixDataList[wordNo];

        if (currentWord == answerWord)
        {
            wordNo = Random.Range(0, fixDataList.Count);
            answerWord = fixDataList[wordNo];
        }

        currentWord = answerWord;
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
