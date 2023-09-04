using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Fric : MonoBehaviour, IDragHandler
{
    public float acceleration = 10.0f;
    public float maxSpeed = 5.0f;
    public float friction = 0.95f;
    public float minVelocity = 0.1f;
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
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        GameObject targetObject = GameObject.Find("GameManager");
        baseGameManajer = targetObject.GetComponent<BaseGameManager>();
    }

    private void Update()
    {
        if (baseGameManajer.inGameEnable == true)
        {
            if (Input.GetMouseButtonDown(0))
            {
                startTouchPos = Input.mousePosition;
                startTime = Time.time;
                velocity = Vector3.zero;
                rb.velocity = velocity;
                isFlicked = false;
                offset = transform.position - Camera.main.ScreenToWorldPoint(new Vector3(startTouchPos.x, startTouchPos.y, transform.position.z));
            }
            else if (Input.GetMouseButtonUp(0) && !isFlicked)
            {
                endTouchPos = Input.mousePosition;
                endTime = Time.time;

                Vector3 touchDelta = endTouchPos - startTouchPos;
                float touchDuration = endTime - startTime;
                Vector3 touchDirection = touchDelta.normalized;

                float flickSpeed = touchDelta.magnitude / touchDuration;

                velocity += touchDirection * flickSpeed * acceleration * Time.deltaTime;
                velocity = Vector3.ClampMagnitude(velocity, maxSpeed);

                isFlicked = true;
            }
            ClampPosition();
        }
        
    }

    private void FixedUpdate()
    {
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
        if (baseGameManajer.inGameEnable == true)
        {
            // ドラッグの差分を計算して現在の位置に加えます
            transform.position += new Vector3(eventData.delta.x, eventData.delta.y, 0);
        }

    }


}
