using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnswerManager : MonoBehaviour
{
    DragFixData D;
    int number;
    int childCount;
    List<GameObject> childObjects = new List<GameObject>(); // GameObjectのリストを作成
    // Start is called before the first frame update
    void Start()
    {
        GameObject targetObject = GameObject.Find("GameManager");
        D = targetObject.GetComponent<DragFixData>();
        childCount = transform.childCount;
        for (int i = 0; i < childCount; i++)
        {
            GameObject childObject = transform.GetChild(i).gameObject; // 子オブジェクトを取得してリストに追加
            childObjects.Add(childObject);
        }
        number = D.Options.Count;
        Debug.Log(number);
        reduce();
    }

    // Update is called once per frame
    void Update()
    {
        number = D.Options.Count;
    }
    public void reduce()
    {
        Debug.Log("aiueoooooo");
        int originalChildCount = childObjects.Count;
        for (int i = 0; i < originalChildCount; i++)
        {
            if (i < number)
            {
                childObjects[i].SetActive(true);
                Debug.Log(";iufvd");
            }
            else
            {
                childObjects[i].SetActive(false);
            }
        }
    }
}
