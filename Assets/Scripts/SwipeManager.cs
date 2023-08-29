using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwipeManager : MonoBehaviour
{
    GameObject[] backGroundList;

    float fingerPosX0;
    float fingerPosX1;
    float posDiff = 100f;

    int firstNo = 0;
    int lastNo = 0;
    int maxNo;

    FlickGameManager fgm;

    bool pushFLG = false;

    void Start()
    {
        fgm = gameObject.GetComponent<FlickGameManager>();

        int childCount = GameObject.Find("BackGroundParent").transform.childCount;
        maxNo = childCount - 1;
        lastNo = maxNo;

        backGroundList = new GameObject[childCount];

        for (int i = 0; i < childCount; i++)
        {
            backGroundList[i] = GameObject.Find("BackGround_" + i).gameObject;
        }
    }

    void Update()
    {
        if (fgm.inGameEnable)
        {
            if (Input.GetMouseButtonDown(0) && !pushFLG)
            {
                fingerPosX0 = Input.mousePosition.x;
                pushFLG = true;
            }
            else if (Input.GetMouseButtonUp(0) && pushFLG)
            {
                fingerPosX1 = Input.mousePosition.x;

                //横移動の判断基準
                if (fingerPosX1 - fingerPosX0 >= posDiff)
                {
                    MoveToLeft(); //別途定義した左方向移動のメソッドを実行
                }
                else if (fingerPosX1 - fingerPosX0 < -posDiff)
                {
                    MoveToRight(); //別途定義した右方向移動のメソッドを実行
                }

                pushFLG = false;
            }
        }
    }

    void MoveToLeft()
    {
        backGroundList[lastNo].transform.SetAsFirstSibling();

        if (firstNo <= 0)
        {
            firstNo = maxNo;
        }
        else
        {
            firstNo--;
        }

        if (lastNo <= 0)
        {
            lastNo = maxNo;
        }
        else
        {
            lastNo--;
        }
    }

    void MoveToRight()
    {
        backGroundList[firstNo].transform.SetAsLastSibling();

        if (firstNo >= maxNo)
        {
            firstNo = 0;
        }
        else
        {
            firstNo++;
        }

        if (lastNo >= maxNo)
        {
            lastNo = 0;
        }
        else
        {
            lastNo++;
        }
    }
}