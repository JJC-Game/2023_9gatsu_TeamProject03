using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreDisplay : MonoBehaviour
{
    TextMeshProUGUI text;

    public int stageNo = 0;

    void Start()
    {
        text = gameObject.GetComponent<TextMeshProUGUI>();

        if (stageNo <= 0)
        {
            int sumScore = 0;

            for (int i = 0; i < 8; i++)
            {
                sumScore += PlayerPrefs.GetInt("StageScore_" + stageNo, 0);
            }

            text.text = sumScore.ToString("00000");
        }
        else
        {
            text.text = PlayerPrefs.GetInt("StageScore_" + stageNo, 0).ToString("0000");
        }
    }
}
