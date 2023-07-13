using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScrollGameManager : BaseGameManager
{
    //使用するイラスト
    Sprite sprite_Target;
    //イラストの数（イラストの通し番号の最も大きい数）
    public int spriteMin;
    public int spriteMax;

    public int iconMin = 6;

    GameObject[] target;

    GameObject[] icon;

    public Image targetPicture;

    AimController aim;

    float aimCount;
    public float aimGoal;

    int correctCount;
    public int correctGoal;

    public TextMeshProUGUI questionNoText;

    public override void Arrangements()
    {
        IconSet();

        aim = GameObject.Find("Aim").GetComponent<AimController>();
        correctCount = 0;
        questionNoText.text = (correctCount + 1).ToString("00");

        target = GameObject.FindGameObjectsWithTag("Target");

        TargetReset(false);
        RandomChange();
    }

    void IconSet()
    {
        GameObject iconParent = GameObject.Find("BackGround_Scroll");

        int childCount = iconParent.transform.childCount;
        icon = new GameObject[childCount];

        for (int n = 0; n < childCount; n++)
        {
            Transform childTransform = iconParent.transform.GetChild(n);
            GameObject childObject = childTransform.gameObject;

            icon[n] = childObject;
        }

        int randomNo = Random.Range(iconMin, childCount);

        for (int i = randomNo; i < childCount; i++)
        {
            icon[i].SetActive(false);
        }
    }

    void RandomChange()
    {
        int randomTarget = Random.Range(0, target.Length);
        int randomTexture = Random.Range(spriteMin, spriteMax + 1);

        sprite_Target = Resources.Load<Sprite>("ProjectAssets/GameIcon/Icon_" + randomTexture);

        target[randomTarget].SetActive(true);

        Image targetImage = target[randomTarget].GetComponent<Image>();
        targetImage.sprite = sprite_Target;
        targetPicture.sprite = sprite_Target;
    }

    void TargetReset(bool flg)
    {
        for (int i = 0; i < target.Length; i++)
        {
            target[i].SetActive(flg);
        }
    }

    public override void UpdatePlus()
    {
        if (aim.targetIn)
        {
            aimCount += Time.deltaTime;

            if (aimCount >= aimGoal)
            {
                if (gameFLG)
                {
                    correctCount++;

                    if (correctCount >= 3)
                    {
                        GameClear();
                    }
                    else
                    {
                        questionNoText.text = (correctCount + 1).ToString("00");
                        aimCount = 0;

                        TargetReset(true);

                        int randomNo = Random.Range(iconMin, icon.Length);

                        for (int i = randomNo; i < icon.Length; i++)
                        {
                            icon[i].SetActive(false);
                        }

                        target = GameObject.FindGameObjectsWithTag("Target");

                        TargetReset(false);
                        RandomChange();
                    }
                }
            }
        }
        else
        {
            aimCount = 0;
        }
    }
}
