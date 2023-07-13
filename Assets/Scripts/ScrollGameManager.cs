using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollGameManager : BaseGameManager
{
    AimController aim;

    float aimCount;
    public float aimGoal;

    public override void Arrangements()
    {
        aim = GameObject.Find("Aim").GetComponent<AimController>();
    }

    public override void UpdatePlus()
    {
        if (aim.targetIn)
        {
            aimCount += Time.deltaTime;

            if (aimCount >= aimGoal)
            {
                GameClear();
            }
        }
        else
        {
            aimCount = 0;
        }
    }
}
