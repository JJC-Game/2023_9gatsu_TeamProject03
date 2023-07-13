using UnityEngine;
using UnityEngine.EventSystems;

public class SwipeAndFlickDetection : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    public RectTransform target; // 移動対象のUI要素
    public Collider movementBoundsCollider; // 移動範囲を制限するコライダー

    private Vector2 fingerDownPosition;
    private Vector2 fingerUpPosition;
    private float fingerDownTime;
    private float fingerUpTime;

    public float swipeThreshold = 50f; // スワイプを検出するための最小移動距離
    public float flickThreshold = 1000f; // フリックを検出するための最小速度
    public float maxSpeed = 100f; // 移動の最大速度
    public float acceleration = 500f; // 加速度
    public float deceleration = 800f; // 減速度

    private Vector2 initialPosition;
    private Vector2 flickVelocity;
    private bool isFlicking = false;
    private bool isTapping = false;

    private bool isOutOfBounds = false;

    void Start()
    {
        initialPosition = target.anchoredPosition;
    }

    void Update()
    {
        if (isFlicking)
        {
            float currentSpeed = flickVelocity.magnitude;
            float targetSpeed = Mathf.Clamp(currentSpeed - deceleration * Time.deltaTime, 0f, maxSpeed);
            flickVelocity = flickVelocity.normalized * targetSpeed;

            target.anchoredPosition += flickVelocity * Time.deltaTime;

            if (flickVelocity.magnitude <= 0.01f)
            {
                StopMovement();
            }

            if (isOutOfBounds && !movementBoundsCollider.bounds.Contains(target.position))
            {
                StopMovement();
            }
        }
        else if (isTapping)
        {
            // タップ時の処理を追加
            // 例えば、タップしたら停止するなどの処理を記述する
            StopMovement();
        }
        else
        {
            // 通常の移動処理を行うなどの処理を記述する
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        fingerDownPosition = eventData.position;
        fingerDownTime = Time.time;
        isTapping = true;
        isOutOfBounds = false;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        fingerUpPosition = eventData.position;
        fingerUpTime = Time.time;
        isTapping = false;

        float swipeDistance = Vector2.Distance(fingerDownPosition, fingerUpPosition);
        float swipeDuration = fingerUpTime - fingerDownTime;
        Vector2 swipeDirection = fingerUpPosition - fingerDownPosition;
        float swipeSpeed = swipeDistance / swipeDuration;

        if (swipeDistance >= swipeThreshold && swipeSpeed >= flickThreshold)
        {
            // フリックを検出
            Debug.Log("Flick Detected");
            flickVelocity = swipeDirection.normalized * swipeSpeed;
            isFlicking = true;
            isOutOfBounds = false;
        }
        else
        {
            // スワイプを検出
            Debug.Log("Swipe Detected");
            isFlicking = false;
            isOutOfBounds = false;
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        // ドラッグ中の処理を追加
        // タッチ位置に応じてUI要素を移動させる処理を記述する
        target.anchoredPosition += eventData.delta;
    }

    private void StopMovement()
    {
        target.anchoredPosition = initialPosition;
        flickVelocity = Vector2.zero;
        isFlicking = false;
        isOutOfBounds = false;
    }
}
