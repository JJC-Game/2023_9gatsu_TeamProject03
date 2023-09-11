using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Fric : MonoBehaviour, IDragHandler
{
    private float acceleration = 5.0f;
    private float maxSpeed = 5000.0f;
    private float friction = 0.92f;
    private float minVelocity = 0.1f;
    private Vector3 startTouchPos;
    private Vector3 endTouchPos;
    private Rigidbody rb;
    private float startTime;
    private float endTime;
    private Vector3 velocity;
    public bool isFlicked = false;
    public Collider movementBoundsCollider;
    private Vector3 offset; // タップ位置とキャラクター中心のオフセット
    public Transform a;
    BaseGameManager baseGameManajer;
    public  float timer=3;//フリックとスワイプの判別時間
    public Vector3 nowPosition;//オブジェクトの場所を取得
    public Vector3 bforePosition;//オブジェクトの場所を取得
    public bool stop;//trueになったらスワイプ後その場にとどまる]
    private float nowTimeNumber = 0.2f;
    float stopTimer;
    [SerializeField]
    float stopNumber=0.2f;
    private float dragSpeed = 0.1f; // ドラッグ速度を調整するための係数

    AimController aim;
   public float aimCount;
    PauseManager pause;
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        GameObject targetObject = GameObject.Find("GameManager");
        baseGameManajer = targetObject.GetComponent<BaseGameManager>();
        aim = GameObject.Find("Aim").GetComponent<AimController>();
        pause = GameObject.Find("GameManager").GetComponent<PauseManager>();
    }

    private void Update()
    {
        if (pause.pauseFLG == true)
        {
            ((RectTransform)transform).anchoredPosition = new Vector2(0, ((RectTransform)transform).anchoredPosition.y); // x軸とz軸を0に設定
        }
        ClampPosition();
        if (baseGameManajer.inGameEnable == false)
        {
           // transform.position = nowPosition;
        }
        if (baseGameManajer.inGameEnable == true)
        {
            if (Input.GetMouseButtonDown(0))
            {
                aimCount = 0;
                startTouchPos = Input.mousePosition;
                startTime = Time.time;
                velocity = Vector3.zero;
                rb.velocity = velocity;
                isFlicked = false;
                offset = transform.position - Camera.main.ScreenToWorldPoint(new Vector3(startTouchPos.x, startTouchPos.y, transform.position.z));
                timer = 0;                                //数値のリセット
                nowTimeNumber = 0.1f;
                bforePosition = Vector3.zero;
                nowPosition = Vector3.zero;
            }
            if (Input.GetMouseButton(0))
            {
                timer += Time.deltaTime;
                if (timer>= nowTimeNumber)//1秒ごとに現在の場所を保存する
                {
                    bforePosition = nowPosition;//前の場所のオブジェクトの場所を取得
                    nowPosition = transform.position;//オブジェクトの場所を取得
                    nowTimeNumber+=0.01f;
                        if (Mathf.Abs(nowPosition.y - bforePosition.y) <= 5)//前の場所のオブジェクトの場所と現在の場所が同じなら時間を図る
                        {
                        Debug.Log(nowPosition + "と" + bforePosition);
                             stopTimer += Time.deltaTime;
                                 if (stopTimer>= stopNumber)//指定の時間を超えたらスワイプ後停止する
                                  {
                                      stop = true;
                                  }
                        }
                         else
                         {
                                stop = false;
                         }

                }       
            }
            else if (Input.GetMouseButtonUp(0) && !isFlicked)
            {
                aimCount = 0;
                if (stop==false)
                {
                    if (Mathf.Abs(nowPosition.y - bforePosition.y) >= 70)
                    {
                        acceleration = 5;
                        Debug.Log(3);
                    }
                    else if (Mathf.Abs(nowPosition.y - bforePosition.y) >= 50)
                    {
                        acceleration = 5;
                        Debug.Log(2);
                    }
                    else if (Mathf.Abs(nowPosition.y - bforePosition.y) >= 0)
                    {
                        acceleration = 5;
                        //  Debug.Log(Mathf.Abs(nowPosition.y - bforePosition.y));
                        Debug.Log(1);
                    }
                    endTouchPos = Input.mousePosition;
                    endTime = Time.time;

                    Vector3 touchDelta = endTouchPos - startTouchPos;
                    float touchDuration = endTime - startTime;
                    Vector3 touchDirection = touchDelta.normalized;

                    float flickSpeed = touchDelta.magnitude / touchDuration;

                    velocity += touchDirection * flickSpeed * acceleration * Time.deltaTime;
                    velocity = Vector3.ClampMagnitude(velocity, maxSpeed);
                    
                    isFlicked = true;
                    timer = 0;
                }
                else
                {
                    stop = false;
                    timer = 0;
                }
            }
            ClampPosition();
        }
        else
        {
            rb.velocity = Vector3.zero;
        }
        
    }

    private void FixedUpdate()
    {
        ClampPosition();
        if (baseGameManajer.inGameEnable == true)
        {
            if (isFlicked)
            {
                if (velocity.magnitude < minVelocity)
                {
                    velocity = Vector3.zero;
                    isFlicked = false;
                }
                else
                {
                    velocity *= friction;
                    rb.velocity = velocity;

                    if (velocity.magnitude < minVelocity)
                    {
                        isFlicked = false;
                    }
                }
            }
        }
        else
        {
            rb.velocity = Vector3.zero;
        }

    }

    void ClampPosition()
    {
        Vector3 clampedPosition = transform.position;
        clampedPosition.x = Mathf.Clamp(clampedPosition.x, movementBoundsCollider.bounds.min.x, movementBoundsCollider.bounds.max.x);
        clampedPosition.y = Mathf.Clamp(clampedPosition.y, movementBoundsCollider.bounds.min.y, movementBoundsCollider.bounds.max.y);

        if (clampedPosition.x != transform.position.x || clampedPosition.y != transform.position.y)
        {
            transform.position = clampedPosition;
        }

        if (transform.position != clampedPosition)
        {
            velocity *= friction;
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
      //  if (aimCount <= 1.9f)
       // {
            if (baseGameManajer.inGameEnable == true)
            {
                // ドラッグの差分を計算して現在の位置に加えます
                Vector3 deltaPosition = new Vector3(0, eventData.delta.y, 0);
                transform.position += deltaPosition * dragSpeed;
            }
        else
        {
            transform.position = nowPosition;
        }
      //  }
        
    }
}
