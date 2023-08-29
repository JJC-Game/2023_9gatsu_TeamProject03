using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Threading.Tasks;
using TMPro;
public class ImageDragAndDrop : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] Sprite[] sprites;
    private bool isDragging = false;
    private Vector3 initialPosition;
    private Vector3 dragStartPosition;
    private Vector3 _currentPos = Vector3.zero;
    public bool _isDrog = false; // 「ドラッグされているか」の真偽値
    private GameObject dragObject; // ドラッグオブジェクトの参照を保持する変数
    Transform drag;
    private Vector3 offset; // ドラッグオブジェクトのオフセット
    public Collider dragBoundsCollider;
    private ImageDragAndDrop draggingPiece; // ドラッグ中のオブジェクトを保持する変数
    public string[] number;
    public TextMeshProUGUI text;

    int Type;
    public int ID1
    {
        get;
        set;
    }
    public int ID2
    {
        get;
        set;
    }
    public Vector2 P1
    {
        get;
        set;
    }
    public Vector2 P2
    {
        get;
        set;
    }
    Director d;
    bool touchFlag;
    Vector2 m;

    private void Start()
    {
        GetComponent<RectTransform>().rotation = Quaternion.identity;
        P1 = GetComponent<RectTransform>().position; // 自身の位置情報を親オブジェクトとして指定
        P2 = RectTransformUtility.WorldToScreenPoint(Camera.main, P1);
        d = GameObject.Find("D").GetComponent<Director>();
      
    }


    void Update()
    {
        HandleInput();
        // Debug.Log(Type);
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        // ドラッグオブジェクトを複製して作成
        dragObject = Instantiate(gameObject, transform.parent);
        dragObject.GetComponent<Image>().raycastTarget = false;
        dragObject.GetComponent<Image>().color = new Color(0f, 0f, 0f, 0f);
        dragObject.transform.SetAsLastSibling();
        Transform drag = dragObject.transform;
        initialPosition = drag.position;
        dragObject.SetActive(false);
        // 元のピースをドラッグ可能にする
        gameObject.GetComponent<Image>().raycastTarget = true;
        d.SetDraggingPiece(this);
        isDragging = true;
        dragStartPosition = eventData.position; // スクリーン座標をワールド座標に変換して代入
        //Debug.Log("OnPointerDown: isDragging = " + isDragging);
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        isDragging = false;
        Destroy(dragObject);
        dragObject = null;
        d.ClearDraggingPiece();


    }
    public void Set(int n)
    {
        //GetComponent<Image>().sprite = sprites[n];
        text.text= number[n];
        gameObject.name = "type" + n;
    }
    public void Set(int x, int y)
    {
        ID1 = x;
        ID2 = y;
        P1 = new Vector2(GetComponent<RectTransform>().position.x, GetComponent<RectTransform>().position.y);
        P2 = RectTransformUtility.WorldToScreenPoint(Camera.main, P1);
    }
    void HandleInput()
    {

        if (isDragging)
        {
            Vector3 currentPosition = Input.mousePosition;
            Vector3 delta = currentPosition - dragStartPosition;
            Vector3 newPosition = initialPosition + delta;
            transform.position = newPosition;
            // Debug.Log(dragc + "ths:iairth");
            ClampPosition();
            d.ComparePositions();
        }
    }
    void ClampPosition()
    {
        if (dragBoundsCollider != null)
        {
            Vector3 clampedDragPosition = drag.position;
            clampedDragPosition.x = Mathf.Clamp(clampedDragPosition.x, dragBoundsCollider.bounds.min.x, dragBoundsCollider.bounds.max.x);
            clampedDragPosition.y = Mathf.Clamp(clampedDragPosition.y, dragBoundsCollider.bounds.min.y, dragBoundsCollider.bounds.max.y);

            // ドラッグ範囲の外に出た場合に停止する
            if (clampedDragPosition.x != drag.position.x || clampedDragPosition.y != drag.position.y)
            {
                drag.position = clampedDragPosition;
            }
        }
    }
}
