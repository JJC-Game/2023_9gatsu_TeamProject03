using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PinntiInOut : MonoBehaviour
{
    public float zoomSpeed = 0.5f;

    private float initialDistance;
    private float initialZoom;

    void Update()
    {
        if (Input.touchCount == 2)
        {
            Touch touch1 = Input.GetTouch(0);
            Touch touch2 = Input.GetTouch(1);

            if (touch2.phase == TouchPhase.Began)
            {
                initialDistance = Vector2.Distance(touch1.position, touch2.position);
                initialZoom = Camera.main.orthographicSize;
            }
            else if (touch1.phase == TouchPhase.Moved && touch2.phase == TouchPhase.Moved)
            {
                float currentDistance = Vector2.Distance(touch1.position, touch2.position);
                float deltaDistance = currentDistance - initialDistance;

                float zoomAmount = deltaDistance * zoomSpeed;

                Camera.main.orthographicSize = Mathf.Clamp(initialZoom - zoomAmount, 1f, float.MaxValue);
            }
        }
    }
}
