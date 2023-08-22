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

    GameObject[] targetList;
    GameObject[] iconList;

    public Image targetPicture;

    AimController aim;

    float aimCount;
    public float aimGoal;

    public override void Arrangements()
    {
        IconSet();

        aim = GameObject.Find("Aim").GetComponent<AimController>();
        correctCount = 0;

        targetList = GameObject.FindGameObjectsWithTag("Target");

        TargetReset(false);
        RandomChange();
    }

    void IconSet()
    {
        GameObject iconParent = GameObject.Find("BackGround_Scroll");

        int childCount = iconParent.transform.childCount;
        iconList = new GameObject[childCount];

        for (int n = 0; n < childCount; n++)
        {
            Transform childTransform = iconParent.transform.GetChild(n);
            GameObject childObject = childTransform.gameObject;

            iconList[n] = childObject;
        }

        int randomNo = Random.Range(iconMin, childCount);

        for (int i = randomNo; i < childCount; i++)
        {
            iconList[i].SetActive(false);
        }
    }

    void RandomChange()
    {
        int randomTarget = Random.Range(0, targetList.Length);
        int randomTexture = Random.Range(spriteMin, spriteMax + 1);

        sprite_Target = Resources.Load<Sprite>("ProjectAssets/GameIcon/Icon_" + randomTexture);

        targetList[randomTarget].SetActive(true);

        Image targetImage = targetList[randomTarget].GetComponent<Image>();
        targetImage.sprite = sprite_Target;
        targetPicture.sprite = sprite_Target;
    }

    void TargetReset(bool flg)
    {
        for (int i = 0; i < targetList.Length; i++)
        {
            targetList[i].SetActive(flg);
        }
    }

    public override void UpdatePlus()
    {
        if (aim.targetIn)
        {
            aimCount += Time.deltaTime;

            if (aimCount >= aimGoal)
            {
                if (inGameEnable)
                {
                    correctCount++;

                    if (correctCount >= 3)
                    {
                        GameClear();
                    }
                    else
                    {
                        aimCount = 0;

                        TargetReset(true);

                        int randomNo = Random.Range(iconMin, iconList.Length);

                        for (int i = randomNo; i < iconList.Length; i++)
                        {
                            iconList[i].SetActive(false);
                        }

                        targetList = GameObject.FindGameObjectsWithTag("Target");

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
