using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Threading.Tasks;
using TMPro;
public class ImageDragAndDrop : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
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
    DragGameManager dGame;
    Image i;
    Color currentColor; // 現在の色を取得
    float redComponent;
    float greenComponent;
    float blueComponent;
    float alphaComponent;
    bool dragObj=false;
  public  bool dragOK=false;
    ButtonClickHandler buttonCheck;
    PauseManager pause;
    private void Start()
    {
        GetComponent<RectTransform>().rotation = Quaternion.identity;
        P1 = GetComponent<RectTransform>().position; // 自身の位置情報を親オブジェクトとして指定
        P2 = RectTransformUtility.WorldToScreenPoint(Camera.main, P1);
        d = GameObject.Find("GameManager").GetComponent<Director>();
        dGame= GameObject.Find("GameManager").GetComponent<DragGameManager>();
        pause= GameObject.Find("GameManager").GetComponent<PauseManager>();
        i = GetComponent<Image>();
        buttonCheck = GameObject.Find("AnswerButton").GetComponent<ButtonClickHandler>();
        Color currentColor = i.color; // 現在の色を取得

        redComponent = currentColor.r;
        greenComponent = currentColor.g;
        blueComponent = currentColor.b;
        alphaComponent = currentColor.a;
    }


    void Update()
    {
        if (pause.pauseFLG == true)
        {

            isDragging = false;
            Destroy(dragObject);
            dragObject = null;
            d.ClearDraggingPiece();
            this.i.color = new Color(redComponent, greenComponent, blueComponent, alphaComponent);
            dragObj = false;
            dragOK = false;
            buttonCheck.dragNowCheck = false;
        }

        // Debug.Log(Type);
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        if (dGame.inGameEnable == true)
        {
          if (Input.touchCount == 1)
           {
                // ドラッグオブジェクトを複製して作成
                dragObject = Instantiate(gameObject, transform.parent);
                dragObject.GetComponent<Image>().raycastTarget = false;
                dragObject.GetComponent<Image>().color = new Color(i.color.r, i.color.g, i.color.b, 0.97f);
                dragObject.transform.SetAsLastSibling();
                Transform drag = dragObject.transform;
                initialPosition = drag.position;
                dragObject.SetActive(true);
                this.i.color = new Color(i.color.r, i.color.g, i.color.b, 0.5f);
                // 元のピースをドラッグ可能にする
                gameObject.GetComponent<Image>().raycastTarget = true;
                Transform dra = dragObject.transform;
                d.SetDraggingPiece(this);
                isDragging = true;
                dragStartPosition = eventData.position; // スクリーン座標をワールド座標に変換して代入
                dragObj = true;
                dragOK = true;
               buttonCheck. dragNowCheck = true;
            }
            //Debug.Log("OnPointerDown: isDragging = " + isDragging);
        }
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        if (dGame.inGameEnable == true)
        {
            if (dragOK == true)
            {
                isDragging = false;
                Destroy(dragObject);
                dragObject = null;
                d.ClearDraggingPiece();
                this.i.color = new Color(redComponent, greenComponent, blueComponent, alphaComponent);
                dragObj = false;
                dragOK = false;
                buttonCheck.dragNowCheck = false;
            }
           
        }

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
    private void SetImageAlpha(float alpha)
    {
        Image image = GetComponent<Image>();
        Color color = image.color;
        color.a = alpha;
        image.color = color;
    }
    public void OnDrag(PointerEventData eventData)
    {
        // if (timer.startTime == true)
        //{
        if (dGame.inGameEnable == true)
        {
            if (dragOK == true&& buttonCheck.check==false)
            {
                transform.position = eventData.position;
                dragObject.transform.position = eventData.position;
                //ClampPosition();
                d.ComparePositions();
            }

        }
        // }
    }
}
