using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectDelete : MonoBehaviour
{
    public float growthDuration = 1.5f; // 成長にかける時間
    public float fadeDuration = 1.5f;   // 色を薄くする時間

    private Material material;
    private Color initialColor;
    private float startTime;
    public float targetScale = 1.2f; // 目標のサイズ

    private float initialScale;
    // Start is called before the first frame update
    void Start()
    {
        material = GetComponent<Renderer>().material; // オブジェクトのマテリアルを取得
        initialColor = material.color; // 初期色を保存
        startTime = Time.time; // 開始時間を記録

        // 1.5秒後にDestroyを呼び出すコルーチンを開始
        StartCoroutine(DestroyAfterDelay(growthDuration));
    }

    // Update is called once per frame
    void Update()
    {
        // 現在の経過時間を計算
        float elapsedTime = Time.time - startTime;

        // 目標色に達するまで色を変更
        if (elapsedTime < fadeDuration)
        {
            float t = elapsedTime / fadeDuration; // 色のフェードの進捗度合い
            Color newColor = Color.Lerp(initialColor, Color.clear, t);
            material.color = newColor;
            float newScale = Mathf.Lerp(initialScale, targetScale, t);
            transform.localScale = new Vector3(newScale, newScale, newScale);
        }
    }

    // 指定した時間を待ってからGameObjectを削除するコルーチン
    private IEnumerator DestroyAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(this.gameObject);
    }
}
