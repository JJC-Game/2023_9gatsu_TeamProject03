using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LeaveOutGameManager : BaseGameManager
{
    //使用するイラスト
    Sprite sprite_a;
    Sprite sprite_b;

    //画面上にあるアイコン
    Image[] icon;

    GameObject[] button;

    //イラストの数（イラストの通し番号の最も大きい数）
    public int spriteMax;

    int correctCount;
    public int correctGoal;

    public TextMeshProUGUI questionNoText;

    public override void Arrangements()
    {
        GameObject iconParent = GameObject.Find("IconParent");
        GameObject buttonParent = GameObject.Find("ButtonArea");
        if (!iconParent || iconParent.transform.childCount <= 0)
        {
            return;
        }

        int childCount = iconParent.transform.childCount;
        icon = new Image[childCount];
        button = new GameObject[childCount];

        for (int n = 0; n < childCount; n++)
        {
            Transform childTransform = iconParent.transform.GetChild(n);
            GameObject childObject= childTransform.gameObject;
            Transform childTransform_Button = buttonParent.transform.GetChild(n);
            GameObject childObject_Button = childTransform_Button.gameObject;

            icon[n] = childObject.GetComponent<Image>();
            button[n] = childObject_Button;
        }

        RandomChange();

        correctCount = 0;
        questionNoText.text = (correctCount + 1).ToString("00");
    }

    void RandomChange()
    {
        ButtonReset();

        int randomIcon = Random.Range(0, icon.Length);
        int randomTexture = Random.Range(0, spriteMax);

        if (Resources.Load<Sprite>("ProjectAssets/GameIcon/Icon_" + randomTexture))
        {
            sprite_a = Resources.Load<Sprite>("ProjectAssets/GameIcon/Icon_" + randomTexture);

            if (randomTexture % 2 == 0)
            {
                randomTexture++;
            }
            else
            {
                randomTexture--;
            }
            sprite_b = Resources.Load<Sprite>("ProjectAssets/GameIcon/Icon_" + randomTexture);
        }

        for (int i = 0; i < icon.Length; i++)
        {
            if (i == randomIcon)
            {
                icon[i].sprite = sprite_b;
                button[i].SetActive(true);
                continue;
            }

            icon[i].sprite = sprite_a;
        }
    }

    void ButtonReset()
    {
        for (int n = 0; n <button.Length; n++)
        {
            button[n].SetActive(false);
        }
    }

    public void Correct()
    {
        if (gameFLG)
        {
            correctCount++;

            if (correctCount >= 99)
            {
                GameClear();
            }
            else
            {
                questionNoText.text = (correctCount + 1).ToString("00");
                RandomChange();
            }
        }
    }

    public void Incorrect()
    {
        LessTime();
    }

    public override void TimeUp()
    {
        if (correctCount >= correctGoal)
        {
            GameClear();
        }
        else
        {
            GameOver();
        }
    }
}