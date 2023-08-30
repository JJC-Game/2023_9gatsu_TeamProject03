using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Collections;
public class PuzzleBoard : MonoBehaviour
{
    // キャンバスの半分の大きさを設定する変数
    public float canvasScale = 0.3f;
    // 盤面の行数と列数を設定
    public int rows = 3;
    public int columns = 3;

    // PiecePrefabを設定
    public GameObject piecePrefab;

    // ピースの間隔を設定
    public float pieceSpacing = 10.0f;
    bool touchFlag;
    public int number = 1;
    int type;
    int o, d, t,f,fi,s,seven,e,n;
    public int selectedValue = 3;
    public int selectedValue2 = 3;
    public int selectedValue3 = 3;
    int randomIndex;
    float startX;
    float startY;
    GameObject canvas;
    Director dire;
    private GridLayoutGroup gridLayoutGroup;
    private List<int> generatedNumbers = new List<int>(); // 生成済みの数字を格納するリスト
    private void Awake()
    {
        dire = GameObject.Find("GameManager").GetComponent<Director>();
        //coler();
        GenerateNewRandomValues();
    }
    private void Start()
    {
        // パズルピースを生成する親オブジェクトを指定
        GameObject parentObject = this.gameObject;

        // キャンバスの GameObject を生成してそのプロパティを設定
        canvas = new GameObject("PuzzleCanvas");
        Canvas canvasComponent = canvas.AddComponent<Canvas>();
        canvasComponent.renderMode = RenderMode.ScreenSpaceOverlay;
        canvas.AddComponent<CanvasScaler>();
        canvas.AddComponent<GraphicRaycaster>();

        // キャンバスのスケールを設定
        canvasComponent.transform.localScale = new Vector3(0.5f, 0.3f, 1f);

        // キャンバスの中央を計算
        float canvasCenterX = canvasComponent.pixelRect.width / 2.0f;
        float canvasCenterY = canvasComponent.pixelRect.height / 2.0f;

        // パズルピースを生成する親オブジェクトを設定
        parentObject = canvas;

        // パズルピースをキャンバス上に配置
        startX = -columns * pieceSpacing / 2.0f + pieceSpacing / 2.0f;
        startY = rows * pieceSpacing / 2.0f - pieceSpacing / 2.0f;

        gridLayoutGroup = this.gameObject.GetComponent<GridLayoutGroup>();
        // パズルピースを生成
        crieit();
     

    }

    private void Update()
    {
        if (dire.on == true)
        {
            // coler();
            GenerateNewRandomValues();
            crieit();
            number++;
            dire.on = false;
        }
        if (gridLayoutGroup.enabled == true)
        {
            StartCoroutine(DelayedEnableGridLayout());
        }
    }
    public void crieit()
    {
        gridLayoutGroup.enabled = true;
        int i = 0;
        for (int row = 0; row < rows; row++)
        {
           
            for (int col = 0; col < columns; col++)
            {           
                // PiecePrefabを生成
                GameObject piece = Instantiate(piecePrefab, this.transform);

                // PiecePrefabの位置を設定
                float posX = startX + col * pieceSpacing;
                float posY = startY - row * pieceSpacing;
                piece.transform.localPosition = new Vector3(posX, posY, 0f);
                // colerSelect();
                type = generatedNumbers[i];
               // Debug.Log(type + "" + generatedNumbers[i]);
                i++;
                // 生成したピースのImageDragAndDropコンポーネントを取得して設定
                ImageDragAndDrop imageDragAndDrop = piece.GetComponent<ImageDragAndDrop>();
                imageDragAndDrop.Set(type);
                imageDragAndDrop.ID1 = row;
                imageDragAndDrop.ID2 = col;
                // Debug.Log(type);"Piece at (" + row + ", " + col + ") - Position: " + piece.transform.localPosition + " - ID1: " + imageDragAndDrop.ID1 + " - ID2: " + imageDragAndDrop.ID2 +
                GameObject.Find("GameManager").GetComponent<Director>().obj[row, col] = piece;
                GameObject.Find("GameManager").GetComponent<Director>().Field[row, col] = type;
            }
        }

    }
    public void coler()
    {
        do
        {
            o = Random.Range(0, 9);
            d = Random.Range(0, 9);
            t = Random.Range(0, 9);
            f = Random.Range(0, 9);
            fi = Random.Range(0, 9);
            s = Random.Range(0, 9);
            seven = Random.Range(0, 9);
            e = Random.Range(0, 9);
            n = Random.Range(0, 9);
        } while (o == d || o == t || d == t);
        //Debug.Log(o + "to" + d + "to" + t);
    }
    public void colerSelect()
    {
        bool coler = true;

        do
        {
            int randomIndex = Random.Range(0, 3);
           // Debug.Log(randomIndex);
            if (randomIndex == 0 && selectedValue > 0)
            {
                type = o;
                selectedValue--;
                coler = false;
            }
            else if (randomIndex == 1 && selectedValue2 > 0)
            {
                type = d;
                selectedValue2--;
                coler = false;
            }
            else if (randomIndex == 2 && selectedValue3 > 0)
            {
                type = t;
                selectedValue3--;
                coler = false;
            }
        } while (coler == true);
        if (selectedValue == 0 && selectedValue2 == 0 && selectedValue3 == 0)
        {
            selectedValue = 3;
            selectedValue2 = 3;
            selectedValue3 = 3;
        }
    }
    private void GenerateUniqueRandomNumbers(int count, int minValue, int maxValue)
    {
        generatedNumbers.Clear(); // 生成済みの数字リストをクリア

        for (int i = 0; i < count; i++)
        {
            int randomValue;
            do
            {
                randomValue = Random.Range(minValue, maxValue + 1);
            } while (generatedNumbers.Contains(randomValue)); // 重複している場合は再生成

            generatedNumbers.Add(randomValue); // リストに追加
        }
    }
    // 呼び出すタイミングでこの関数を使用する
    public void GenerateNewRandomValues()
    {
        GenerateUniqueRandomNumbers(9, 0, 8); // 9つの異なる乱数を生成（0から8まで）
        o = generatedNumbers[0];
        d = generatedNumbers[1];
        t = generatedNumbers[2];
        f = generatedNumbers[3];
        fi = generatedNumbers[4];
        s = generatedNumbers[5];
        seven = generatedNumbers[6];
        e = generatedNumbers[7];
        n = generatedNumbers[8];
        //Debug.Log(o + "+" + d + "+" + t + "+" + f + "+" + fi + "+" + s + "+" + seven + "+" + e + "+" + n);
    }

    // 数秒待機してから GridLayoutGroup を有効にするコルーチン
    private IEnumerator DelayedEnableGridLayout()
    {
        yield return new WaitForSeconds(0.3f); // 2秒待機

        // GridLayoutGroup を有効にする
        gridLayoutGroup.enabled = false;
    }
}
