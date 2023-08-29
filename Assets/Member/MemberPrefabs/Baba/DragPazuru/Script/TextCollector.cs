using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class TextCollector : MonoBehaviour
{
    public List<string> textList = new List<string>(); // �e�L�X�g�����i�[���郊�X�g
    public List<string> additionalTextList = new List<string>(); // �ʂ̃��X�g�ɒǉ�����e�L�X�g��񃊃X�g
    int number = 9;
    int count;
    private void Start()
    {
        CollectTextFromChildren(transform);
        // �ʂ̃��X�g�Ƀe�L�X�g��ǉ�
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
            Debug.Log("�Z");
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

            // �q�I�u�W�F�N�g���ċA�I�ɒT��
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
