using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Director : MonoBehaviour
{
    // 保存するファイルパスを指定（例：Assets/IDs.txt）
    //private string filePath = "Assets/X";
    private ImageDragAndDrop draggingPiece; // ドラッグ中のオブジェクトを保持する変数
    public bool on = false;
    // 盤面の位置情報を管理する二次元配列
    private Vector3[,] boardPositions;
    // 盤面のサイズを決定（例えば8x8の場合）
    int boardSizeX = 3;
    int boardSizeY = 3;
    GameObject[,] o = new GameObject[3, 3];
    public GameObject[,] obj
    {
        get { return o; }
        set { o = value; }
    }
    int[,] f = new int[3, 3];
    public int[,] Field
    {
        get { return f; }
        set { f = value; }
    }
    //private float positionThreshold = 30000f;
    private ImageDragAndDrop[,] pieces; // ピースの配列を追加
    public int count = 0;
    ButtonClickHandler click;
    // Start is called before the first frame update
    void Start()
    {
        click = GameObject.Find("AnswerButton").GetComponent<ButtonClickHandler>();
    }

    // Update is called once per frame
    void Update()
    {
        SaveInitialPositions();
        if (click.check == true)
        {

            DeletDrop();
            click.check = false;
        }
    }
    public bool CheckPos(Vector2 p1, Vector2 p2)
    {
        float x = p1.x - p2.x;
        float y = p1.y - p2.y;
        float r = Mathf.Sqrt(x * x + y * y);

        // 距離の最大値を計算
        float maxDistance = Mathf.Sqrt(Screen.width * Screen.width + Screen.height * Screen.height);

        // 距離の最大値に対する割合を計算
        float ratio = r / maxDistance;
        Debug.Log(ratio);
        // ある程度近い場合に入れ替えると判断
        if (ratio <= 0.2f)
        {
            return true;
        }
        return false;
    }
    public void ChangePos(ImageDragAndDrop d1, ImageDragAndDrop d2)
    {
        GameObject obj1 = d1.gameObject;
        GameObject obj2 = d2.gameObject;

        GameObject temObj;
        Vector3 tempPos;
        int temp;
        temObj = obj[d1.ID1, d1.ID2];
        obj[d1.ID1, d1.ID2] = obj[d2.ID1, d2.ID2];
        obj[d2.ID1, d2.ID2] = temObj;

        temp = Field[d1.ID1, d1.ID2];
        Field[d1.ID1, d1.ID2] = Field[d2.ID1, d2.ID2];
        Field[d2.ID1, d2.ID2] = temp;

        // 位置情報を入れ替える
        tempPos = obj1.GetComponent<RectTransform>().position;
        obj1.GetComponent<RectTransform>().position = obj2.GetComponent<RectTransform>().position;
        obj2.GetComponent<RectTransform>().position = tempPos;

        tempPos = d1.P1;
        d1.P1 = d2.P1;
        d2.P1 = tempPos;

        tempPos = d1.P2;
        d1.P2 = d2.P2;
        d2.P2 = tempPos;

        temp = d1.ID1;
        d1.ID1 = d2.ID1;
        d2.ID1 = temp;

        temp = d1.ID2;
        d1.ID2 = d2.ID2;
        d2.ID2 = temp;
    }
    private void SaveInitialPositions()
    {
        ImageDragAndDrop[] dragAndDrops = FindObjectsOfType<ImageDragAndDrop>();

        // 盤面の位置情報を初期化
        boardPositions = new Vector3[boardSizeX, boardSizeY];
        pieces = new ImageDragAndDrop[boardSizeX, boardSizeY]; // ImageDragAndDrop クラスの二次元配列を初期化

        foreach (ImageDragAndDrop piece in dragAndDrops)
        {
            int x = piece.ID1;
            int y = piece.ID2;

            boardPositions[x, y] = piece.GetComponent<RectTransform>().position;
            pieces[x, y] = piece; // ImageDragAndDrop クラスのオブジェクトを代入
            //Debug.Log("Piece at (" + x + ", " + y + ") - Position: " + piece.transform.localPosition + " - ID1: " + piece.ID1 + " - ID2: " + piece.ID2 + " - Type: " + type);
        }
    }

    public void SetDraggingPiece(ImageDragAndDrop piece)
    {
        draggingPiece = piece;
    }

    public void ClearDraggingPiece()
    {
        draggingPiece = null;
    }
    public void ComparePositions()
    {
        if (draggingPiece == null) return;
        Vector3[] distances = new Vector3[boardSizeX * boardSizeY];
        int draggingPieceIndex = -1;

        for (int x = 0; x < boardSizeX; x++)
        {
            for (int y = 0; y < boardSizeY; y++)
            {
                distances[x + y * boardSizeX] = boardPositions[x, y] - draggingPiece.transform.position;
            }
        }

        float minDistance = float.MaxValue;
        int minIndex = -1;

        for (int i = 0; i < distances.Length; i++)
        {
            float distance = distances[i].sqrMagnitude;
            if (distance < minDistance)
            {
                minDistance = distance;
                minIndex = i;
            }
        }

        if (minIndex >= 0)
        {
            draggingPieceIndex = minIndex;
            int newX = minIndex % boardSizeX;
            int newY = minIndex / boardSizeX;

            // ドラッグ中のピースと入れ替える対象のピースを取得
            ImageDragAndDrop targetPiece = pieces[newX, newY];

            // 入れ替える対象のピースが存在する場合に入れ替え処理を行う
            if (targetPiece != null)
            {
                int dragX = draggingPiece.ID1;
                int dragY = draggingPiece.ID2;
                int targetX = targetPiece.ID1;
                int targetY = targetPiece.ID2;

                // ピースの入れ替え
                pieces[dragX, dragY] = targetPiece;
                pieces[targetX, targetY] = draggingPiece;

                // Field配列の値も入れ替え
                int tempField = Field[dragX, dragY];
                Field[dragX, dragY] = Field[targetX, targetY];
                Field[targetX, targetY] = tempField;

                GameObject de = obj[dragX, dragY];
                obj[dragX, dragY] = obj[targetX, targetY];
                obj[targetX, targetY] = de;
                // 位置の更新
                draggingPiece.transform.position = boardPositions[newX, newY];
                targetPiece.transform.position = boardPositions[dragX, dragY];
                // ピースのIDを交換前と交換後でデバッグログで表示
               // Debug.Log("交換前：オブジェクト名: " + draggingPiece.gameObject.name + ", ID1: " + draggingPiece.ID1 + ", ID2: " + draggingPiece.ID2);
              //  Debug.Log("交換前：入れ替え対象のオブジェクト名: " + targetPiece.gameObject.name + ", ID1: " + targetPiece.ID1 + ", ID2: " + targetPiece.ID2);

                // ピースのIDを更新
                draggingPiece.Set(newX, newY);
                targetPiece.Set(dragX, dragY);
                // ピースのIDを交換後でデバッグログで表示
              //  Debug.Log("交換後：オブジェクト名: " + draggingPiece.gameObject.name + ", ID1: " + draggingPiece.ID1 + ", ID2: " + draggingPiece.ID2);
              //  Debug.Log("交換後：入れ替え対象のオブジェクト名: " + targetPiece.gameObject.name + ", ID1: " + targetPiece.ID1 + ", ID2: " + targetPiece.ID2);

                // ピースの位置を更新した後に、複製したピースの位置情報を初期化
                for (int x = 0; x < boardSizeX; x++)
                {
                    for (int y = 0; y < boardSizeY; y++)
                    {
                        if (pieces[x, y] == null)
                        {
                            boardPositions[x, y] = pieces[x, y].GetComponent<RectTransform>().position;
                        }
                    }
                }
                // 盤面の位置とID、ドラッグ後の位置とIDをテキストファイルに保存
               // SavePositionsAndIDsToFile saver = new SavePositionsAndIDsToFile();
                //saver.SavePositionsAndIDs(filePath, pieces);
            }
        }
    }
    public void DeletDrop()
    {
        int c = 0;
        int[,] temp = new int[3, 3];
        int[,] temp2 = new int[3, 3];
        count = 0;
        // Check for horizontal matches and update 'temp' array
        for (int i = 0; i < 3; i++)
        {
            c = 1;
            for (int j = 1; j < 3; j++)
            {
                int currentCellValue = Field[i, j];
                int prevCellValue = Field[i, j - 1];
                string objNameBefore = obj[i, j - 1].gameObject.name; // オブジェクト名を取得
                string objNameCurrent = obj[i, j].gameObject.name; // オブジェクト名を取得
                //Debug.Log(currentCellValue + 1 + "" + prevCellValue);
                // Debug.Log("横" + currentCellValue + "前" + prevCellValue + " (オブジェクト名前: " + objNameBefore + ", オブジェクト名: " + objNameCurrent + ") " + Field[i, j]); // デバッグログに番号とオブジェクト名を表示
                if (currentCellValue == prevCellValue + 1)
                {
                  //Debug.Log(Field[i, j] + "" + Field[i - 1, j]);
                    c++;
                    if (c >= 3)
                    {
                        Debug.Log("combo");
                        temp[i, j] = c;
                    }
                }
                else
                {
                    c = 1;
                }
            }
        }
        /*
        // Check for vertical matches and update 'temp2' array
        for (int j = 0; j < 3; j++)
        {
            c = 1;
            for (int i = 1; i < 3; i++)
            {
                int currentCellValue = Field[i, j];
                int prevCellValue = Field[i - 1, j];
                string objNameBefore = obj[i - 1, j].gameObject.name; // オブジェクト名を取得
                string objNameCurrent = obj[i, j].gameObject.name; // オブジェクト名を取得
             //Debug.Log("縦"+currentCellValue + "前" + prevCellValue + " (オブジェクト名前: " + objNameBefore + ", オブジェクト名: " + objNameCurrent + ") " + Field[i, j]); // デバッグログに番号とオブジェクト名を表示
                if (Field[i, j] == Field[i - 1, j])
                {
              Debug.Log(Field[i, j] + "" + Field[i - 1, j]);
                    c++;
                    if (c >= 3)
                    {
                        Debug.Log("combo");
                        temp2[i, j] = c;
                    }
                }
                else
                {
                    c = 1;
                }
            }
        }
        */
        // Delete horizontal matches
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                if (temp[i, j] >= 3)
                {
                    count++;
                    /*
                    //Debug.Log("Deleting Drop at (" + i + ", " + j + ") with number " + Field[i, j]);
                    for (int k = j; temp[i, j] > 0; k--, temp[i, j]--)
                    {
                        Field[i, k] = 6;
                        //Debug.Log("obj[" + i + ", " + k + "]の名前: " + obj[i, k].gameObject.name);
                        // obj[i, k].GetComponent<DropCnt>().Set(6);
                        int type = Random.Range(6, 6);

                        // 生成したピースのImageDragAndDropコンポーネントを取得して設定
                        ImageDragAndDrop imageDragAndDrop = obj[i, k].GetComponent<ImageDragAndDrop>();
                        imageDragAndDrop.Set(type);
                    }*/
                }
            }
        }

        // Delete vertical matches
        for (int j = 0; j < 3; j++)
        {
            for (int i = 0; i < 3; i++)
            {
                //Debug.Log(temp2);
                if (temp2[i, j] >= 3)
                {
                    count++;
                    /*
                   // Debug.Log("Deleting Drop at (" + i + ", " + j + ") with number " + Field[i, j]);
                    for (int k = i; temp2[i, j] > 0; k--, temp2[i, k]--)
                    {
                        Field[k, j] = 6;
                        //Debug.Log("obj[" + k + ", " + j + "]の名前: " + obj[k, j].gameObject.name);

                        // int six = 6;
                        //obj[k, j].GetComponent<DropCnt>().Set(6);
                        int type = Random.Range(6, 6);

                        // 生成したピースのImageDragAndDropコンポーネントを取得して設定
                        ImageDragAndDrop imageDragAndDrop = obj[k, j].GetComponent<ImageDragAndDrop>();  k
                        imageDragAndDrop.Set(type);
                    }
                */
                }
            }
        }
        if (count >= 3)
        {
            // 盤面のピースと盤面情報を全て消す
            for (int x = 0; x < boardSizeX; x++)
            {
                for (int y = 0; y < boardSizeY; y++)
                {
                    // ピースのGameObjectを削除
                    if (obj[x, y] != null)
                    {
                        Destroy(obj[x, y]);
                        obj[x, y] = null;
                    }
                    // 盤面情報を初期値（6）にリセット
                    Field[x, y] = 6;
                }
            }
            on = true;
        }
    }


}
