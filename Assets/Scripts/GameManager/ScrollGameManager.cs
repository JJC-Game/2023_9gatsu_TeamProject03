using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScrollGameManager : BaseGameManager
{
    public int iconMin = 6;

    int currentNo;

    GameObject[] targetList;
    GameObject[] iconList;

    AimController aim;
    Image aimTimer;

    float aimCount;
    public float aimGoal;

    public override void Arrangements()
    {
        IconSet();

        aim = GameObject.Find("Aim").GetComponent<AimController>();
        aimTimer = GameObject.Find("AimTimer").GetComponent<Image>();

        targetList = GameObject.FindGameObjectsWithTag("Target");

        TargetReset(false);
        RandomChange();
    }

    void IconSet()
    {
        GameObject iconParent = GameObject.Find("IconParent");

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

        if (currentNo == randomTarget)
        {
            randomTarget = Random.Range(0, targetList.Length);
        }

        targetList[randomTarget].SetActive(true);

        Image targetImage = targetList[randomTarget].GetComponent<Image>();
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
            aimTimer.fillAmount = aimCount / aimGoal;

            if (aimCount >= aimGoal)
            {
                if (inGameEnable)
                {
                    AddScore();

                    aimCount = 0;
                    aimTimer.fillAmount = aimCount / aimGoal;

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
        else
        {
            aimCount = 0;
            aimTimer.fillAmount = aimCount / aimGoal;
        }
    }
}
