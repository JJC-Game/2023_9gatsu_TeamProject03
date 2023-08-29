using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class TextCollector : MonoBehaviour
{
    public List<string> textList = new List<string>(); // テキスト情報を格納するリスト
    public List<string> additionalTextList = new List<string>(); // 別のリストに追加するテキスト情報リスト
    int number = 9;
    int count;
    private void Start()
    {
        CollectTextFromChildren(transform);
        // 別のリストにテキストを追加
        additionalTextList.Add("1");
        additionalTextList.Add("2");
        additionalTextList.Add("3");
        additionalTextList.Add("4");
        additionalTextList.Add("5");
        additionalTextList.Add("6");
        additionalTextList.Add("7");
        additionalTextList.Add("8");
        additionalTextList.Add("9");
    }
    private void Update()
    {
        if (number == count)
        {
            Debug.Log("〇");
        }
    }
    private void CollectTextFromChildren(Transform parentTransform)
    {
        foreach (Transform childTransform in parentTransform)
        {
            TMP_Text tmpText = childTransform.GetComponent<TMP_Text>();
            if (tmpText != null)
            {
                textList.Add(tmpText.text);
            }

            // 子オブジェクトも再帰的に探索
            CollectTextFromChildren(childTransform);
        }
    }
    public void CheckDuplicateTexts()
    {
        textList.Clear();
        CollectTextFromChildren(transform);
        foreach (string text in textList)
        {
            if (additionalTextList.Contains(text))
            {

                count++;

            }
        }
    }
}
