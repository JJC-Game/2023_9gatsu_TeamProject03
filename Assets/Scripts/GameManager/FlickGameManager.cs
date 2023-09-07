using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FlickGameManager : BaseGameManager
{
    GameObject[] targetList;

    AimController aim;

    int currentTarget;

    public override void Arrangements()
    {
        aim = GameObject.Find("Aim").GetComponent<AimController>();

        targetList = GameObject.FindGameObjectsWithTag("Target");

        TargetReset(false);
        RandomChange();
    }

    void RandomChange()
    {
        int randomTarget = Random.Range(0, targetList.Length);

        if (randomTarget == currentTarget)
        {
            randomTarget = Random.Range(0, targetList.Length);
        }

        currentTarget = randomTarget;
        targetList[randomTarget].SetActive(true);
    }

    void TargetReset(bool flg)
    {
        for (int i = 0; i < targetList.Length; i++)
        {
            targetList[i].SetActive(flg);
        }
    }

    public void Answer()
    {
        if (aim.targetIn)
        {
            if (inGameEnable)
            {
                AddScore();

                TargetReset(false);
                RandomChange();
            }
        }
        else
        {
            if (inGameEnable)
            {
                LessTime();
            }
        }
    }
}