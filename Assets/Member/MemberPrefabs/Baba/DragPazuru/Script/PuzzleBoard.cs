using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

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
   // int i = 0;
    int type;
    int o, d, t;
    public int selectedValue = 3;
    public int selectedValue2 = 3;
    public int selectedValue3 = 3;
    int randomIndex;
    float startX;
    float startY;
    GameObject canvas;
    Director dire;
    private void Awake()
    {
        dire = GameObject.Find("D").GetComponent<Director>();
        coler();
    }
    private void Start()
    {
        // キャンバスを生成
        canvas = new GameObject("PuzzleCanvas");
        Canvas canvasComponent = canvas.AddComponent<Canvas>();
        canvasComponent.renderMode = RenderMode.ScreenSpaceOverlay;
        canvas.AddComponent<CanvasScaler>();
        canvas.AddComponent<GraphicRaycaster>();

        // キャンバスの大きさを元のキャンバスの半分に設定
        canvasComponent.transform.localScale = new Vector3(0.5f, 0.3f, 1f);

        // 生成したキャンバスを親オブジェクトに設定
        canvas.transform.SetParent(transform);

        // キャンバスの中央を計算
        float canvasCenterX = canvasComponent.pixelRect.width / 2.0f;
        float canvasCenterY = canvasComponent.pixelRect.height / 2.0f;

        // キャンバスの子オブジェクトとして、PiecePrefabを並べる
        startX = -columns * pieceSpacing / 2.0f + pieceSpacing / 2.0f;
        startY = rows * pieceSpacing / 2.0f - pieceSpacing / 2.0f;

        crieit();



    }
    private void Update()
    {
        if (dire.on == true)
        {
            coler();
            crieit();
            dire.on = false;
        }
    }
    public void crieit()
    {
        for (int row = 0; row < rows; row++)
        {
            for (int col = 0; col < columns; col++)
            {
                // PiecePrefabを生成
                GameObject piece = Instantiate(piecePrefab, canvas.transform);

                // PiecePrefabの位置を設定
                float posX = startX + col * pieceSpacing;
                float posY = startY - row * pieceSpacing;
                piece.transform.localPosition = new Vector3(posX, posY, 0f);
                colerSelect();

                // 生成したピースのImageDragAndDropコンポーネントを取得して設定
                ImageDragAndDrop imageDragAndDrop = piece.GetComponent<ImageDragAndDrop>();
                imageDragAndDrop.Set(type);
                imageDragAndDrop.ID1 = row;
                imageDragAndDrop.ID2 = col;
                // Debug.Log(type);
                GameObject.Find("D").GetComponent<Director>().obj[row, col] = piece;
                GameObject.Find("D").GetComponent<Director>().Field[row, col] = type;
                //Debug.Log("Piece at (" + row + ", " + col + ") - Position: " + piece.transform.localPosition + " - ID1: " + imageDragAndDrop.ID1 + " - ID2: " + imageDragAndDrop.ID2 + " - Type: " + type);
            }
        }
    }
    public void coler()
    {
        do
        {
            o = Random.Range(0, 6);
            d = Random.Range(0, 6);
            t = Random.Range(0, 6);
        } while (o == d || o == t || d == t);
        //Debug.Log(o + "to" + d + "to" + t);
    }
    public void colerSelect()
    {
        bool coler = true;

        do
        {
            int randomIndex = Random.Range(0, 3);
            Debug.Log(randomIndex);
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
}
