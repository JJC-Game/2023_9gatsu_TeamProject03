using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;

public class GetChildTMPro : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    DragFixData drag;
    public bool condition;
    public List<string> anser = new List<string>();
    public int ID;
    public int DragID = 99;
    private Vector3 prePosition;
    public bool ok = false;
    private bool posi = true;
    private GameObject era;
    public GameObject anotherObject;
    private AnotherScript another;
    private GameObject anserGameObject;
    //ボックスの中にオブジェクトを吸い込んでいい時はFalse
    public static bool boxFlag;
    public bool modoru = false;
    Timer timer;
    AnswerManager anserManager;
    private void Awake()
    {
        GameObject targetObject = GameObject.Find("GameManager");
        GameObject targetObjectA = GameObject.Find("A");
        another = anotherObject.GetComponent<AnotherScript>();
        drag = targetObject.GetComponent<DragFixData>();
        timer = targetObject.GetComponent<Timer>();
        anserManager = targetObjectA.GetComponent<AnswerManager>();

    }
    void Update()
    {
        check();
        if (ID == DragID)
        {
            ok = true;
        }
        if (another.okTM == true)
        {
            dragPosi();
            era = null;
            anserManager.reduce();
        }

    }
    public void check()
    {
        drag.Ansew();
        anser = drag.Options;
        condition = false;
        TextMeshProUGUI[] textMeshProComponents = GetComponentsInChildren<TextMeshProUGUI>();

        foreach (TextMeshProUGUI textMeshPro in textMeshProComponents)
        {
            for (int i = 1; i < anser.Count + 1; i++)
            {
                //Debug.Log(anser[i]);
                if (textMeshPro.text == anser[i - 1])
                {
                    condition = true;
                    ID = i;
                    if (condition)
                    {
                        Debug.Log("Condition is true for object: " + textMeshPro.gameObject.name);
                    }
                }
            }
        }
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        if (timer.startTime == true)
        {
            another.okTM = false;
            if (posi == true)
            {
                prePosition = transform.position;
                posi = false;
            }
            if (era)
            {
                era.SetActive(true);
            }
        }

    }
    public void OnDrag(PointerEventData eventData)
    {
        if (timer.startTime == true)
        {
            transform.position = eventData.position;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (timer.startTime == true)
        {
            bool flg = true;

            var raycastResults = new List<RaycastResult>();
            EventSystem.current.RaycastAll(eventData, raycastResults);

            foreach (var hit in raycastResults)
            {
                if (hit.gameObject.name == "1")
                {
                    Debug.Log("元に戻す");
                    modoru = true;
                    transform.position = hit.gameObject.transform.position;
                    era = hit.gameObject;
                    DragID = 1;
                    hit.gameObject.SetActive(false);
                    flg = false;
                }
                if (hit.gameObject.name == "2")
                {
                    modoru = true;
                    DragID = 2;
                    transform.position = hit.gameObject.transform.position;
                    era = hit.gameObject;
                    hit.gameObject.SetActive(false);
                    flg = false;
                }
                if (hit.gameObject.name == "3")
                {
                    modoru = true;
                    DragID = 3;
                    transform.position = hit.gameObject.transform.position;
                    era = hit.gameObject;
                    hit.gameObject.SetActive(false);
                    flg = false;
                }
                if (hit.gameObject.name == "4")
                {
                    modoru = true;
                    DragID = 4;
                    transform.position = hit.gameObject.transform.position;
                    era = hit.gameObject;
                    hit.gameObject.SetActive(false);
                    flg = false;
                }
                if (hit.gameObject.name == "5")
                {
                    modoru = true;
                    DragID = 5;
                    transform.position = hit.gameObject.transform.position;
                    era = hit.gameObject;
                    hit.gameObject.SetActive(false);
                    flg = false;
                }
            }
            if (flg)
            {
                transform.position = prePosition;
                DragID = 99;
            }
        }
    }
    public void dragPosi()
    {
        if (modoru == true && era != null)
        {
            era.SetActive(true);
            transform.position = prePosition;

            modoru = false;
        }
    }
}
