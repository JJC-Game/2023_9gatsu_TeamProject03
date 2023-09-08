using UnityEngine;

public class TapEffect : MonoBehaviour
{
    public GameObject effectParent; // エフェクトの親オブジェクトを指定
    private GameObject effectInstance;

    private void Update()
    {
        // マウスクリックを検出
        if (Input.GetMouseButtonDown(0))
        {
            var mousePosition = Input.mousePosition;//クリックした場所に取得
            mousePosition.z = 8.8f; // Z座標を設定
            effectInstance = GameObject.Instantiate(EffectManager.Instance.playerFX[0], Camera.main.ScreenToWorldPoint(mousePosition), Quaternion.identity);

            // 生成したエフェクトを親オブジェクトの子オブジェクトにする
            if (effectParent != null)
            {
                effectInstance.transform.SetParent(effectParent.transform);
            }
        }

        if (Input.touchCount > 0)
        {
            var touch = Input.GetTouch(0);//タッチした場所に取得

            // タップが開始された瞬間（TouchPhase.Began）のみオブジェクトを生成
            if (touch.phase == TouchPhase.Began)
            {
                Vector3 touchPosition = Camera.main.ScreenToWorldPoint(new Vector3(touch.position.x, touch.position.y, 8.8f)); // Z座標を設定
                effectInstance = GameObject.Instantiate(EffectManager.Instance.playerFX[0], touchPosition, Quaternion.identity);

                // 生成したエフェクトを親オブジェクトの子オブジェクトにする
                if (effectParent != null)
                {
                    effectInstance.transform.SetParent(effectParent.transform);
                }
            }
        }
    }
}
