using UnityEngine;
using UnityEngine.UI;

public class SmartphoneOperation : MonoBehaviour
{
   // public Text text; // 確認のテキスト
    public Transform target; // 移動対象のオブジェクト
    private float speed = 23f;//移動のスピード
    public bool isFlicking = false; // フリック中かどうかのフラグ
    public Vector3 flickVelocity; // フリックの速度ベクトル

    public Collider movementBoundsCollider; // 移動範囲を制限するコライダー
    public float acceleration = 200f; // 加速度
    public float deceleration = 1000f; // 減速度
    public float maxSpeed = 300f; // 移動の最大速度

    private Vector3 initialPosition; // 初期位置

    public float time;//タップした時間を図る
    public float flicTime;
    private float fulic = 0.35f;
    private float tap = 0.11f;
    public float timer;

    private bool fulicer;//フリック中

   // public Collider ClearBoundsCollider; // クリア範囲を配置するコライダー

    private float timeInBounds = 3f; // 範囲内に入っている必要のある時間
    private float currentTimeInBounds = 0f; // 現在の範囲内滞在時間
   // public Transform Q;
    [SerializeField] private float decelerationTime = 1f; // 減速がかかるまでの時間

   public  bool trisi;
    void Start()
    {
        initialPosition = target.position;

    }

    void ClampPosition()
    {
        Vector3 clampedPosition = target.position;
        clampedPosition.x = Mathf.Clamp(clampedPosition.x, movementBoundsCollider.bounds.min.x, movementBoundsCollider.bounds.max.x);
        clampedPosition.y = Mathf.Clamp(clampedPosition.y, movementBoundsCollider.bounds.min.y, movementBoundsCollider.bounds.max.y);

        // 移動範囲の外に出た場合に停止する
        if (clampedPosition.x != target.position.x || clampedPosition.y != target.position.y)
        {
            flickVelocity = Vector3.zero;
           // isFlicking = false;
            target.position = clampedPosition;
        }
    }

    void Update()
    {
       // OperationChecker();
        HandleInput();
      //  if (clear == true)
      //  {
            //Clear();
       // }

    }

    void HandleInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            isFlicking = false;
            ClampPosition();
            time = 0;
            timer = 0;          
            initialPosition = target.position;
            fulicer = false;
        }
        else if (Input.GetMouseButton(0))
        {
            //clear = false;
            float dx = Input.GetAxis("Mouse X") * speed;
            float dy = Input.GetAxis("Mouse Y") * speed;
            target.Translate(dx, dy, 0f);
            time += Time.deltaTime;
            //text.text = "スワイプ";
        }
        if (time <= tap && fulicer == false)
        {
            //Tap();
           // Debug.Log("タップ");
        }
        if (time <= fulic)
        {
              if (Input.GetMouseButtonUp(0))
              {
                flickVelocity = (target.position - initialPosition).normalized * speed * 5f;
                isFlicking = true;
                initialPosition = target.position;
                flicTime = time;
              
                //text.text = "フリック";
                 }
            if (isFlicking)
            {
                isFlicking = true;
                if (flicTime >= 0.21f)
                {
                    decelerationTime = 1f;
                    maxSpeed = 10000;
                    acceleration = 4000;
                    deceleration = 5000;
                    isFlicking = true;
                    Debug.Log("減速する時間を決めたよ3");
                }
                else if (flicTime >= 0.1f)
                {
                    decelerationTime = 0.8f;
                    maxSpeed = 3000;
                    acceleration = 1500;
                    deceleration = 1800;
                    isFlicking = true;
                    Debug.Log("減速する時間を決めたよ2");
                }
                else if (flicTime >= 0.000001f)
                {
                    decelerationTime = 0.5f;
                    maxSpeed = 400;
                    acceleration = 1200;
                    deceleration = 1500;
                    isFlicking = true;
                    Debug.Log("減速する時間を決めたよ");

                }

                timer += Time.deltaTime;
                float currentSpeed = flickVelocity.magnitude;
                float targetSpeed = Mathf.Clamp(currentSpeed + acceleration * Time.deltaTime, 0f, maxSpeed);
                flickVelocity = flickVelocity.normalized * targetSpeed;
                float decelerationMagnitude = deceleration * Time.deltaTime;
                target.position += flickVelocity * Time.deltaTime;
               // Debug.Log(decelerationMagnitude);

                if (timer >= decelerationTime) // 指定の時間(decelerationTime)を超えたら減速を開始
                {
                    if (flickVelocity.magnitude > 0.01f)
                    {
                        flickVelocity = Vector3.MoveTowards(flickVelocity, Vector3.zero, decelerationMagnitude);
                        
                    }
                    else
                    {
                        flickVelocity = Vector3.zero;
                        // isFlicking = false;
                       
                        time = 0;
                    }
                    trisi = true;
                    Debug.Log("減速したよ");
                    
                }

                if (flickVelocity.magnitude <= 0.00001f&& trisi==true)
                {
                    isFlicking = false;
                    trisi = false;
                }
            }
        }
        else
        {
            ClampPosition();
        }
        ClampPosition();
        if ( !isFlicking)
        {
           // clear = true;
        }
        else
        {
            //clear = false;
        }
    }
   // void Clear()
  //  {
    //    if (IsObjectInBounds())
     //   {
       //     currentTimeInBounds += Time.deltaTime;

        ///    if (currentTimeInBounds >= timeInBounds)
        //    {
        //        Debug.Log("クリア");
                //text.text = "クリア";
         //   }
     //   }
      //  else
      //  {
        //    currentTimeInBounds = 0f;
       // }
   // }
   // bool IsObjectInBounds()
   // {
       // return ClearBoundsCollider.bounds.Contains(Q.position);
   // }
}
