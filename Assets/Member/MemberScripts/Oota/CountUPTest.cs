using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CountUPTest : MonoBehaviour
{
    public TextMeshProUGUI countText;

    public bool StartFLG;

    public int targetCount = 10; // 目標のカウント数
    public float totalTime = 5f; // 決められた時間（秒）
    public AnimationCurve speedCurve; // アニメーションカーブで速度を調整

    public int currentCount = 0; // 現在のカウント数
    public float elapsedTime = 0f; // 経過時間

    public void CountStart()
    {
        StartFLG = true;
    }

    private void Update()
    {
        if (StartFLG)
            if (currentCount < targetCount)
            {
                elapsedTime += Time.deltaTime; // 経過時間を更新

                float timeRatio = Mathf.Clamp01(elapsedTime / totalTime);
                float speedMultiplier = speedCurve.Evaluate(timeRatio);

                int countToAdd = Mathf.FloorToInt(speedMultiplier * Time.deltaTime * targetCount);

                currentCount = Mathf.Clamp(currentCount + countToAdd, 0, targetCount);

                countText.text = currentCount.ToString();
            }
    }
}
