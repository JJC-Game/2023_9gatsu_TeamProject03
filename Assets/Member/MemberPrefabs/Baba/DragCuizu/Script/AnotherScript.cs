using UnityEngine;
using System.Collections.Generic;
using TMPro;
public class AnotherScript : MonoBehaviour
{
    public bool ok = false;
    public bool okTM = false;
   // public TextMeshProUGUI FailureText;
  //  float time=3;
    BaseGameManager baseManager;
    public void Awake()
    {
        GameObject TextObjyekut = GameObject.Find("AText");
        // FailureText = TextObjyekut.GetComponent<TextMeshProUGUI>();
        GameObject targetObject = GameObject.Find("GameManager");
        baseManager = targetObject.GetComponent<BaseGameManager>();
    }
    private void Update()
    {


    }
    public void sheck()
    {
        if (baseManager.inGameEnable == true)
        {
            okTM = true;
            // GetChildTMPro スクリプトがアタッチされたオブジェクトを取得
            GameObject getChildTMProObject = GameObject.Find("T");
            GameObject getAnser = GameObject.Find("GameManager");
            GetChildTMPro[] getChildTMProScripts = getChildTMProObject.GetComponentsInChildren<GetChildTMPro>();
            DragFixData getAnsers = getAnser.GetComponent<DragFixData>();
            int trueCount = 0;
            int okCont = 0;
            // 各子オブジェクトの condition プロパティにアクセス
            foreach (GetChildTMPro getChildTMProScript in getChildTMProScripts)
            {
                bool isConditionTrue = getChildTMProScript.condition;

                if (isConditionTrue)
                {
                    trueCount++;
                }
            }
            // 各子オブジェクトの condition プロパティにアクセス
            foreach (GetChildTMPro getChildTMProScript in getChildTMProScripts)
            {
                bool isConditionTrue = getChildTMProScript.ok;

                if (isConditionTrue)
                {
                    okCont++;
                }
            }
            List<string> anser = new List<string>();
            anser = getAnsers.Options;

            if (anser.Count == okCont)
            {
                Debug.Log("正かい");
                ok = true;
                okTM = true;
                for (int i = 0; i < getChildTMProScripts.Length; i++)
                {
                    getChildTMProScripts[i].DragID = 99;
                    getChildTMProScripts[i].ID = 88;
                    getChildTMProScripts[i].condition = false;
                    getChildTMProScripts[i].ok = false;
                    Debug.Log("リセット");
                    baseManager.AddScore();
                }
            }
            else
            {
                // FailureText.text = "不正解";
                baseManager.LessTime();
                //TimerCheck = true;
            }
            // true の数を表示
            Debug.Log("文字の数 " + trueCount);
            Debug.Log("合ってる数 " + okCont);
        }
     
    }
    public void skip()
    {
        if (baseManager.inGameEnable == true)
        {
            okTM = true;
            // GetChildTMPro スクリプトがアタッチされたオブジェクトを取得
            GameObject getChildTMProObject = GameObject.Find("T");
            GameObject getAnser = GameObject.Find("GameManager");
            GetChildTMPro[] getChildTMProScripts = getChildTMProObject.GetComponentsInChildren<GetChildTMPro>();
            DragFixData getAnsers = getAnser.GetComponent<DragFixData>();
            int trueCount = 0;
            int okCont = 0;
            // 各子オブジェクトの condition プロパティにアクセス
            foreach (GetChildTMPro getChildTMProScript in getChildTMProScripts)
            {
                bool isConditionTrue = getChildTMProScript.condition;

                if (isConditionTrue)
                {
                    trueCount++;
                }
            }
            // 各子オブジェクトの condition プロパティにアクセス
            foreach (GetChildTMPro getChildTMProScript in getChildTMProScripts)
            {
                bool isConditionTrue = getChildTMProScript.ok;

                if (isConditionTrue)
                {
                    okCont++;
                }
            }
            List<string> anser = new List<string>();
            anser = getAnsers.Options;


                Debug.Log("正かい");
                ok = true;
                okTM = true;
                for (int i = 0; i < getChildTMProScripts.Length; i++)
                {
                    getChildTMProScripts[i].DragID = 99;
                    getChildTMProScripts[i].ID = 88;
                    getChildTMProScripts[i].condition = false;
                    getChildTMProScripts[i].ok = false;
                    Debug.Log("リセット");
                   // baseManager.AddScore();
                }

            // true の数を表示
            Debug.Log("文字の数 " + trueCount);
            Debug.Log("合ってる数 " + okCont);
        }

    }
}
