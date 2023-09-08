using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreDisplay : MonoBehaviour
{
    //使用するイラスト
    public Sprite sprite;
    int textureId;

    TextMeshProUGUI text;

    public int stageNo = 1;

    void Start()
    {
        text = gameObject.GetComponent<TextMeshProUGUI>();

        int sumScore = 0;

        sumScore += PlayerPrefs.GetInt("StageScore_" + stageNo, 0);
        sumScore += PlayerPrefs.GetInt("StageScore_" + (stageNo+1), 0);

        text.text = sumScore.ToString("00000");

        if (sumScore >= 1000000)
        {
            textureId = 4;
        }
        else if(sumScore >= 100000)
        {
            textureId = 3;
        }
        else if(sumScore >= 10000)
        {
            textureId = 2;
        }
        else if(sumScore >= 1000)
        {
            textureId = 1;
        }
        else
        {
            textureId = 0;
        }

        if (Resources.Load<Sprite>("ProjectAssets/UIPack/CoinMountain_" + textureId))
        {
            sprite = Resources.Load<Sprite>("ProjectAssets/UIPack/CoinMountain_" + textureId);
        }
    }
}
