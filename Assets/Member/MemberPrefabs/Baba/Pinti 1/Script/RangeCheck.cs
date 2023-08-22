using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class RangeCheck : MonoBehaviour
{
    public TextMeshProUGUI problemText; // 問題を表示するTextMeshPro
    public TextMeshProUGUI[] numberText; // 問題数を表示するTextMeshPro
    public TextMeshProUGUI resultText; // 正解判定結果を表示するTextMeshPro
    public GameObject detectionArea; // 特定の範囲を表すオブジェクト
    public List<GameObject> parentObjects = new List<GameObject>(); // 親オブジェクトのリスト
    public Button solveButton; // 解答ボタン

    private int targetIndex;
    private int targetCount;
    public int correctCount = 1;
    public Image imageComponent;
    public List<Sprite> imageToShow = new List<Sprite>();  // 表示する画像
    Timer timer;
    public GameObject ScoreText;
    private void Start()
    {
        ScoreText.SetActive(false);
        GameObject targetObject = GameObject.Find("D");
        timer = targetObject.GetComponent<Timer>();
        solveButton.onClick.AddListener(CheckSolution);
        GenerateRandomProblem();
        numberText[0].text = "1";
    }
    private void Update()
    {
        if (timer.end == true)
        {
            ScoreText.SetActive(true);
        }
    }
    private void GenerateRandomProblem()
    {
        targetIndex = Random.Range(0, parentObjects.Count);
        GameObject targetParent = parentObjects[targetIndex];
        string targetObjectName = targetParent.name;

        int actualObjectCount = targetParent.transform.childCount;
        targetCount = Random.Range(1, actualObjectCount + 1);

        if (targetIndex==0|| targetIndex==1|| targetIndex==2)
        {
            imageComponent.sprite = null;
            if (targetIndex == 0)
            {
                imageComponent.color = Color.red;
            }else if (targetIndex == 1)
            {
                imageComponent.color = Color.blue;
            }else if (targetIndex == 2)
            {
                imageComponent.color = Color.green;
            }
            problemText.text =  "を" + targetCount + "個範囲に入れてください";
            resultText.text = "";
        }
        if (targetIndex == 3 || targetIndex == 4 || targetIndex ==5)
        {
            if (targetIndex ==3)
            {
                imageComponent.color = Color.red;
            }
            else if (targetIndex ==4)
            {
                imageComponent.color = Color.blue;
            }
            else if (targetIndex ==5)
            {
                imageComponent.color = Color.green;
            }
            imageComponent.sprite = imageToShow[0];
            problemText.text =  "を" + targetCount + "個範囲に入れてください";
            resultText.text = "";
        }
        if (targetIndex ==6 || targetIndex ==7|| targetIndex ==8)
        {
            if (targetIndex ==6)
            {
                imageComponent.color = Color.red;
            }
            else if (targetIndex ==7)
            {
                imageComponent.color = Color.blue;
            }
            else if (targetIndex ==8)
            {
                imageComponent.color = Color.green;
            }
            imageComponent.sprite = imageToShow[1];
            problemText.text =  "を" + targetCount + "個範囲に入れてください";
            resultText.text = "";
        }
        if (targetIndex ==9 || targetIndex ==10 || targetIndex ==11)
        {
            if (targetIndex ==9)
            {
                imageComponent.color = Color.red;
            }
            else if (targetIndex ==10)
            {
                imageComponent.color = Color.blue;
            }
            else if (targetIndex ==11)
            {
                imageComponent.color = Color.green;
            }
            imageComponent.sprite = imageToShow[2];
            problemText.text = "を" + targetCount + "個範囲に入れてください";
            resultText.text = "";
        }
    }

    private void CheckSolution()
    {
        int objectsInsideCount = CountObjectsInside(parentObjects[targetIndex]);
        if (objectsInsideCount == targetCount)
        {
            resultText.text = "正解！";
            correctCount++;
            GenerateRandomProblem();
        }
        else
        {
            resultText.text = "不正解。もう一度試してみてください。";
        }
        numberText[0].text = correctCount.ToString();
        numberText[1].text = correctCount.ToString();
    }

    private int CountObjectsInside(GameObject parentObject)
    {
        int count = 0;
        Collider detectionCollider = detectionArea.GetComponent<Collider>();

        if (detectionCollider == null || parentObject == null)
        {
            return count;
        }

        foreach (Transform child in parentObject.transform)
        {
            Collider childCollider = child.GetComponent<Collider>();
            if (childCollider != null && detectionCollider.bounds.Intersects(childCollider.bounds))
            {
                count++;
            }
        }

        return count;
    }
}
