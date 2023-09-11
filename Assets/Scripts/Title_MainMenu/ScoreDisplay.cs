using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoreDisplay : MonoBehaviour
{
    //使用するイラスト
    public Image coinImage;
    public int textureId;

    TextMeshProUGUI text;

    public int stageNo = 1;

    int sumScore = 0;

    void Start()
    {
        text = gameObject.GetComponent<TextMeshProUGUI>();

        sumScore += PlayerPrefs.GetInt("StageScore_" + stageNo, 0);
        sumScore += PlayerPrefs.GetInt("StageScore_" + (stageNo+1), 0);

        text.text = sumScore.ToString("0");

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
        Debug.Log(textureId);

        if (Resources.Load<Sprite>("ProjectAssets/UIPack/CoinMountain_" + textureId))
        {
            coinImage.sprite = Resources.Load<Sprite>("ProjectAssets/UIPack/CoinMountain_" + textureId);
        }
    }

    private void Update()
    {
        int no = PlayerPrefs.GetInt("StageScore_" + stageNo, 0);
        no += PlayerPrefs.GetInt("StageScore_" + (stageNo + 1), 0);

        if (sumScore != no)
        {
            sumScore = no;

            text.text = sumScore.ToString("0");

            Color color = coinImage.color;
            color.a = 0;

            coinImage.color = color;

            textureId = 0;
        }
    }
}
