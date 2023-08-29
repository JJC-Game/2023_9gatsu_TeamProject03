using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragObject : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler, IEndDragHandler
{
    private bool buttonDown = false;
    public GameObject hitNowObject; // ヒットしたオブジェクトを格納する変数
    public GameObject hitObject;
    public int numRays = 12; // Rayの本数
    public float maxRayLength = 100f; // Rayの最大長さ

    public Vector3 initialPosition; // ドラッグ開始時の位置を記録
    private Vector3 hitIntialPosition;

    public float time = 0.35f;
    public bool timerFlg = false;
    private void Start()
    {

    }

    public void Update()
    {
        if (buttonDown)
        {
            for (int i = 0; i < numRays; i++)
            {
                float angle = i * (360f / numRays);
                Vector3 rayDirection = Quaternion.Euler(0f, 0f, angle) * Vector3.right;
               // Debug.DrawRay(transform.position, rayDirection * maxRayLength, Color.red); // Rayを可視化
                RaycastHit hit;
                if (Physics.Raycast(transform.position, rayDirection, out hit, maxRayLength))
                {
                    // 自分自身のオブジェクトでないことを確認してから判定
                    if (hit.collider.gameObject != gameObject && hit.collider.gameObject.name == "1")
                    {
                        hitNowObject = hit.collider.gameObject; // ヒットしたオブジェクトを変数に格納
                        Debug.Log("ヒットしたオブジェクトを保存しました");
                    }
                }
            }
        }
        if (timerFlg == true)
        {
            Timer();
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        initialPosition = transform.position; // 初期位置を記録
        transform.SetAsLastSibling();
        buttonDown = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        buttonDown = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = eventData.position;
        if (hitNowObject != null)
        {
            if (hitNowObject == hitObject)
            {
                timerFlg = true;
            }
            else
            {
                hitObject = hitNowObject;
                hitIntialPosition = hitNowObject.transform.position;
                hitNowObject.transform.position = initialPosition; // ヒットしたオブジェクトの位置を initialPosition に変更
                initialPosition = hitIntialPosition; // ヒットしたオブジェクトの位置を initialPosition に代入

            }
            if (time <= 0)
            {
                if (hitObject)
                {
                    hitObject = null;
                    time = 0.35f;
                    timerFlg = false;
                }
            }
            hitNowObject = null; // 変数をリセット
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        transform.position = initialPosition; // ドラッグ終了時に初期位置に戻す
        hitNowObject = null;
        hitObject = null;
    }
    public void Timer()
    {
        time -= Time.deltaTime;
    }
}