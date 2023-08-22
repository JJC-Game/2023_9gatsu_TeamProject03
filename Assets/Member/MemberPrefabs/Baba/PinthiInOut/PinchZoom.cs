using UnityEngine;
using UnityEngine.UI;
using System.IO;
using TMPro;
public class PinchZoom : MonoBehaviour
{
    // タッチ操作の最小距離を定義
    private float minPinchDistance = 10f;

    // タッチ操作の開始時の2点間の距離を保持する変数
    private float startPinchDistance = 0f;

    // タッチ操作の開始時のオブジェクトのスケールを保持する変数
    private Vector3 startScale = Vector3.one;

    // ピンチイン・ピンチアウトを適用する対象のオブジェクト
    public GameObject targetObject;

    // 指定するランダムな大きさの範囲
    public float minRandomScale = 0.01f;
    public float maxRandomScale = 100f;

    // 枠線のみを表示するオブジェクトのプレハブ
    public GameObject borderPrefab;

    // オブジェクトの初期位置とスケール
    private Vector3 initialPosition;
    private Vector3 initialScale;
    private GameObject currentObject; // 現在のオブジェクトを格納する変数
    private string randomImageFileName;

    public Image image;
    private int generatedImageCount = 0;
    public TextMeshProUGUI CounntText;
    void Start()
    {
        // オブジェクトの初期位置とスケールを記録
        initialPosition = targetObject.transform.position;
        initialScale = targetObject.transform.localScale;
        // ランダムな大きさにオブジェクトをリセット
        ResetObjectToRandomScale();
        SelectRandomImage();
        generatedImageCount = 1;
        CounntText.text = "1";
    }

    void Update()
    {
        // オブジェクトのスケールを制限するための関数を呼び出す
        LimitObjectScale(currentObject);
        // タッチされた指の数を取得
        int touchCount = Input.touchCount;

        // タッチされた指の数が2本であることを確認
        if (touchCount == 2)
        {
            // タッチ情報を取得
            Touch touchZero = Input.GetTouch(0);
            Touch touchOne = Input.GetTouch(1);

            // タッチ操作の種類を確認
            if (touchZero.phase == TouchPhase.Began || touchOne.phase == TouchPhase.Began)
            {
                // タッチ操作が始まった瞬間の2点間の距離とオブジェクトのスケールを保持
                startPinchDistance = Vector2.Distance(touchZero.position, touchOne.position);
                startScale = targetObject.transform.localScale;
            }
            else if (touchZero.phase == TouchPhase.Moved || touchOne.phase == TouchPhase.Moved)
            {
                // タッチ操作中の2点間の距離を計算
                float currentPinchDistance = Vector2.Distance(touchZero.position, touchOne.position);

                // タッチ操作開始時からの変化率を計算
                float scaleFactor = currentPinchDistance / startPinchDistance;

                // 変化率を元のスケールに適用してオブジェクトを拡大縮小
                targetObject.transform.localScale = startScale * scaleFactor;
            }
        }
     
    }

    void ResetObjectToRandomScale()
    {
        // 現在のオブジェクトのスケールを取得
        Vector3 currentScale = currentObject != null ? currentObject.transform.localScale : targetObject.transform.localScale;

        // 現在のオブジェクトのスケールと異なるランダムなスケールを生成
        float randomScale = currentScale.x;
        while (Mathf.Approximately(randomScale, currentScale.x))
        {
            randomScale = Random.Range(minRandomScale, maxRandomScale);
        }

        // 新しいオブジェクトを複製
        currentObject = Instantiate(targetObject, initialPosition, Quaternion.identity);

        // 新しいオブジェクトのスケールを設定
        currentObject.transform.localScale = initialScale * randomScale;

        // 新しいオブジェクトをこのスクリプトがアタッチされているオブジェクトの子オブジェクトにする
        currentObject.transform.parent = transform;

        // 新しいオブジェクトに枠線のみを表示するスクリプトを追加
        currentObject.AddComponent<BorderOnlyDisplay>();

        // 古いオブジェクトは操作対象として残す
        targetObject.SetActive(true);

        // 新しいオブジェクトに制限をかける
        LimitObjectScale(currentObject); // 引数を追加したここで関数を呼び出す
    }

    void SelectRandomImage()
    {
        // Imagesフォルダ内のすべての画像ファイルを取得
        string[] imageFiles = Directory.GetFiles("Assets/Resources/Images", "*.jpg");

        // ランダムにファイルを選択
        randomImageFileName = imageFiles[Random.Range(0, imageFiles.Length)];
        Debug.Log(randomImageFileName);
    }

    // 現在のオブジェクトのイメージを変更する関数
    void SetImageForCurrentObject()
    {
        // ランダムに選ばれたイメージを読み込む
        Sprite newSprite = LoadImageFromFile(randomImageFileName);

        // 現在のオブジェクトにImageコンポーネントがある場合のみイメージを変更
        Image image = targetObject.GetComponent<Image>();
        if (image != null)
        {
            image.sprite = newSprite;
        }
    }
    Sprite LoadImageFromFile(string fileName)
    {
        // ファイルをバイト配列として読み込む
        byte[] bytes = File.ReadAllBytes(fileName);

        // バイト配列からテクスチャを作成
        Texture2D texture = new Texture2D(1, 1);
        texture.LoadImage(bytes);

        // テクスチャのY軸を反転させる
       // FlipTextureY(texture);

        // テクスチャからスプライトを作成して返す
        return Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.one * 0.5f);
    }
    void FlipTextureY(Texture2D texture)
    {
        Color[] pixels = texture.GetPixels();
        int width = texture.width;
        int height = texture.height;

        for (int y = 0; y < height / 2; y++)
        {
            for (int x = 0; x < width; x++)
            {
                int topIndex = y * width + x;
                int bottomIndex = (height - y - 1) * width + x;
                Color temp = pixels[topIndex];
                pixels[topIndex] = pixels[bottomIndex];
                pixels[bottomIndex] = temp;
            }
        }

        texture.SetPixels(pixels);
        texture.Apply();
    }
    void LimitObjectScale(GameObject obj)
    {
        // キャンバスコンポーネントを取得
        Canvas canvas = GetComponentInParent<Canvas>();
        if (canvas == null)
        {
            Debug.LogError("Canvas component not found.");
            return;
        }

        // キャンバスのサイズを取得
        Vector2 canvasSize = canvas.GetComponent<RectTransform>().sizeDelta;

        // オブジェクトがキャンバスからはみ出さないように制限
        float objectWidth = obj.transform.localScale.x * obj.GetComponent<RectTransform>().rect.width;
        float objectHeight = obj.transform.localScale.y * obj.GetComponent<RectTransform>().rect.height;

        if (objectWidth > canvasSize.x || objectHeight > canvasSize.y)
        {
            // オブジェクトのスケールを調整してキャンバス内に収まるようにする
            float widthRatio = canvasSize.x / objectWidth;
            float heightRatio = canvasSize.y / objectHeight;
            float minRatio = Mathf.Min(widthRatio, heightRatio);

            // スケールを調整
            obj.transform.localScale *= minRatio;
        }
    }
    public void ButtonChick()
    {
        // 現在のオブジェクトがnullでなく、ほぼ同じサイズになった場合
        if (Vector3.Distance(targetObject.transform.localScale, currentObject.transform.localScale) < 0.8f)
        {
            // 現在のオブジェクトのスケールを取得
            Vector3 currentScale = currentObject != null ? currentObject.transform.localScale : targetObject.transform.localScale;

            int maxAttempts = 100; // 最大試行回数を設定
            float randomScale = Random.Range(minRandomScale, maxRandomScale);
            float diff = Mathf.Abs(randomScale - currentScale.x);
            int attempts = 0;
            float maxDifference = 10f; // 最大のスケールの差
                                       // 生成された画像のカウントを1プラス
            generatedImageCount++;

            // カウントをログに出力
            Debug.Log("生成された画像の数: " + generatedImageCount.ToString());
            while ((Mathf.Approximately(diff, 0f) || diff < maxDifference) && attempts < maxAttempts)
            {
                randomScale = Random.Range(minRandomScale, maxRandomScale);
                diff = Mathf.Abs(randomScale - currentScale.x);
                attempts++;
            }

            if (attempts == maxAttempts)
            {
                // 最大試行回数を超えても条件を満たすスケールが見つからなかった場合は、現在のスケールから±10の範囲内でランダムに選ぶ
                randomScale = Random.Range(currentScale.x - maxDifference, currentScale.x + maxDifference);
            }


            SelectRandomImage();
            Destroy(currentObject);
            // 現在のオブジェクトのイメージを変更する
            SetImageForCurrentObject();
            // 新しいオブジェクトを複製
            currentObject = Instantiate(targetObject, initialPosition, Quaternion.identity);
            currentObject.transform.parent = transform;
            // 新しいオブジェクトのスケールをリセット
            currentObject.transform.localScale = initialScale;
            currentObject.transform.localScale = initialScale * randomScale;
            if (currentObject.transform.localScale.x <= 0 && currentObject.transform.localScale.y <= 0 && currentObject.transform.localScale.z <= 0)
            {
                currentObject.transform.localScale = currentObject.transform.localScale * -1;
            }
            currentObject.AddComponent<BorderOnlyDisplay>();            // 新しいオブジェクトを最前列に表示
           // int generatedImageCountText = generatedImageCount++;
            CounntText.text =""+ generatedImageCount;
        }
    }
}

