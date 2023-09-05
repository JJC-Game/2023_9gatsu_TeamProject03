using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreDisplay : MonoBehaviour
{
    TextMeshProUGUI text;

    public int stageNo = 1;

    void Start()
    {
        text = gameObject.GetComponent<TextMeshProUGUI>();

        int sumScore = 0;

        sumScore += PlayerPrefs.GetInt("StageScore_" + stageNo, 0);
        sumScore += PlayerPrefs.GetInt("StageScore_" + (stageNo+1), 0);

        text.text = sumScore.ToString("00000");
    }
}
