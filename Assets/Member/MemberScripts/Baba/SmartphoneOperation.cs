using UnityEngine;
using UnityEngine.UI;

public class SmartphoneOperation : MonoBehaviour
{
   // public Text text; // 確認のテキスト
    public Transform target; // 移動対象のオブジェクト
    private float speed = 100f;//移動のスピード
    private bool isFlicking = false; // フリック中かどうかのフラグ
    private Vector3 flickVelocity; // フリックの速度ベクトル

    public Collider movementBoundsCollider; // 移動範囲を制限するコライダー
    private float acceleration = 150f; // 加速度
    private float deceleration = 160f; // 減速度
    private float maxSpeed = 100f; // 移動の最大速度

    private Vector3 initialPosition; // 初期位置

    private float time;//タップした時間を図る
    private float fulic = 0.8f;
    private float tap = 0.11f;

    public GameObject seikai;//正解のゲームオブジェクト
    public GameObject hazule;//ハズレのゲームオブジェクト

    private bool fulicer;//フリック中

    public Collider ClearBoundsCollider; // クリア範囲を配置するコライダー

    private float timeInBounds = 3f; // 範囲内に入っている必要のある時間
    private float currentTimeInBounds = 0f; // 現在の範囲内滞在時間
    public Transform Q;
    void Start()
    {
        initialPosition = target.position;
        seikai.SetActive(false);
        hazule.SetActive(false);
    }

    void ClampPosition()
    {
        Vector3 clampedPosition = target.position;
        clampedPosition.x = Mathf.Clamp(clampedPosition.x, movementBoundsCollider.bounds.min.x, movementBoundsCollider.bounds.max.x);
        clampedPosition.y = Mathf.Clamp(clampedPosition.y, movementBoundsCollider.bounds.min.y, movementBoundsCollider.bounds.max.y);

        // 移動範囲の外に出た場合に停止する
        if (clampedPosition.x != target.position.x || clampedPosition.y != target.position.y)
        {
            flickVelocity = Vector3.zero;
            isFlicking = false;
            target.position = clampedPosition;
        }
    }

    void Update()
    {
       // OperationChecker();
        HandleInput();
        Clear();
    }
    void OperationChecker()
    {//どの操作をするかチェック
        if ((Input.GetMouseButtonUp(0)))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                Debug.Log("オブジェクトヒット");
                Debug.Log("Hit Collider: " + hit.collider.name);
                GameObject objectHit = hit.collider.gameObject;
                if (objectHit.tag == "Drag")
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
                else if (objectHit.tag == "PinchOutIn")
                {

                }
                else
                {
                  
                }
            }
        }
    }
    void Tap()
    {
        if (Input.touchCount == 1)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Ended)
            {
                Ray ray = Camera.main.ScreenPointToRay(touch.position);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit))
                {
                    GameObject objectHit = hit.collider.gameObject;
                    if (objectHit.tag == "Correct")
                    {
                        seikai.SetActive(true);
                        Debug.Log("正解");
                    }
                    else if (objectHit.tag == "Hazle")
                    {
                        hazule.SetActive(true);
                        Debug.Log("ハズレ");
                    }
                    //text.text = "タップしたよ";
                }
            }

        }
        if (Input.GetMouseButtonUp(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            time = 0;
            if (Physics.Raycast(ray, out hit))
            {
                Debug.Log("オブジェクトヒット");
                Debug.Log("Hit Collider: " + hit.collider.name);
                GameObject objectHit = hit.collider.gameObject;
                if (objectHit.tag == "Correct")
                {
                    seikai.SetActive(true);
                    Debug.Log("正解");
                }
                else if (objectHit.tag == "Hazle")
                {
                    hazule.SetActive(true);
                    Debug.Log("ハズレ");
                }

                //text.text = "タップしたよ";
            }
        }
    }

    void HandleInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            initialPosition = target.position;
            fulicer = false;
        }
        else if (Input.GetMouseButton(0))
        {
            float dx = Input.GetAxis("Mouse X") * speed;
            float dy = Input.GetAxis("Mouse Y") * speed;
            target.Translate(dx, dy, 0f);
            time += Time.deltaTime;
            //text.text = "スワイプ";
        }
        if (time <= tap && fulicer == false)
        {
            Tap();
            Debug.Log("タップ");
        }
        else if (time <= fulic)
        {
            Debug.Log("フリック");
            fulicer = true;
            if (Input.GetMouseButtonUp(0))
            {
                flickVelocity = (target.position - initialPosition).normalized * speed * 5f;
                isFlicking = true;
                initialPosition = target.position;
                //text.text = "フリック";

            }

            if (isFlicking)
            {
                float currentSpeed = flickVelocity.magnitude;
                float targetSpeed = Mathf.Clamp(currentSpeed + acceleration * Time.deltaTime, 0f, maxSpeed);
                flickVelocity = flickVelocity.normalized * targetSpeed;

                target.position += flickVelocity * Time.deltaTime;

                float decelerationMagnitude = deceleration * Time.deltaTime;
                flickVelocity = Vector3.MoveTowards(flickVelocity, Vector3.zero, decelerationMagnitude);

                if (flickVelocity.magnitude <= 0.01f)
                {
                    isFlicking = false;
                    time = 0;
                }
            }
        }
        ClampPosition();
        if (Input.GetMouseButtonUp(0))
        {
            time = 0;
        }

    }
    void Clear()
    {
        if (IsObjectInBounds())
        {
            currentTimeInBounds += Time.deltaTime;

            if (currentTimeInBounds >= timeInBounds)
            {
                Debug.Log("クリア");
                //text.text = "クリア";
            }
        }
        else
        {
            currentTimeInBounds = 0f;
        }
    }
    bool IsObjectInBounds()
    {
        return ClearBoundsCollider.bounds.Contains(Q.position);
    }
}
