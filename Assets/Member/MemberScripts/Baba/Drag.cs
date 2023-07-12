using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drag : MonoBehaviour
{
    private bool isDragging = false;
    private Vector3 initialPosition;
    private Vector3 offset;
    public Transform target; // 移動対象のオブジェクト
    private float speed = 100f;//移動のスピード
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            initialPosition = target.position;
        }
        else if (Input.GetMouseButton(0))
        {
            float dx = Input.GetAxis("Mouse X") * speed;
            float dy = Input.GetAxis("Mouse Y") * speed;
            target.Translate(dx, dy, 0f);
          
        }
    }
    void OnMouseDown()
    {
        if (Input.touchCount <= 1) // タッチ入力が1本以下の場合のみ処理を実行
        {
            isDragging = true;
            initialPosition = transform.position;

            if (Input.touchCount == 1)
            {
                offset = transform.position - Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
            }
            else
            {
                offset = transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition);
            }
        }
    }

    void OnMouseDrag()
    {
        if (isDragging)
        {
            Vector3 newPosition;

            if (Input.touchCount == 1)
            {
                newPosition = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position) + offset;
            }
            else
            {
                newPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition) + offset;
            }

            transform.position = new Vector3(newPosition.x, newPosition.y, transform.position.z);
        }
    }

    void OnMouseUp()
    {
        isDragging = false;
    }
}
