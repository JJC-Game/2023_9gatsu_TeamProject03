using UnityEngine;

public class PinchZoomCount : MonoBehaviour
{
    public float minScale = 0.5f; // 最小のスケール
    public float maxScale = 1000.0f; // 最大のスケール

    private float startDistance;
    private Vector3 initialScale;
    public float sensitivity = 0.1f;
    Timer timer;
    private void Start()
    {
        initialScale = transform.localScale;
        GameObject targetObject = GameObject.Find("GameManager");
        timer = targetObject.GetComponent<Timer>();
    }

    private void Update()
    {
        if (timer.startTime == true)
        {
            Pc();
            if (Input.touchCount == 2)
            {
                Touch touch1 = Input.GetTouch(0);
                Touch touch2 = Input.GetTouch(1);

                if (touch2.phase == TouchPhase.Began)
                {
                    startDistance = Vector2.Distance(touch1.position, touch2.position);
                }
                else if (touch1.phase == TouchPhase.Moved && touch2.phase == TouchPhase.Moved)
                {
                    float currentDistance = Vector2.Distance(touch1.position, touch2.position);
                    float scaleFactor = currentDistance / startDistance;

                    // オブジェクトのスケールを更新
                    Vector3 newScale = initialScale * scaleFactor;
                    newScale = Vector3.Max(newScale, Vector3.one * minScale); // 最小スケールを適用
                    newScale = Vector3.Min(newScale, Vector3.one * maxScale); // 最大スケールを適用
                    transform.localScale = newScale;
                }
            }
        }
    }
    void Pc()
    {
        float pinchAmount = 0;

        // マウスのホイールのスクロール量を取得
        float scrollDelta = Input.GetAxis("Mouse ScrollWheel");

        // スクロール量をピンチ操作のスケーリングに変換（反転させる）
        pinchAmount = scrollDelta * sensitivity;

        // オブジェクトのスケールを更新
        Vector3 newScale = transform.localScale + Vector3.one * pinchAmount;
        newScale = Vector3.Max(newScale, Vector3.one * minScale); // 最小スケールを適用
        newScale = Vector3.Min(newScale, Vector3.one * maxScale); // 最大スケールを適用
        transform.localScale = newScale;
    }

}
