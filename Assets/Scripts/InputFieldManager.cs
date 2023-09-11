using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InputFieldManager : MonoBehaviour
{
    private InputField inputField;
    public string resultText;   // 入力されたテキストを格納

    private RectTransform parentRect;
    private Vector3 defaultParentPos;  // 初期位置
    private bool isOnceInput = true; // 入力時のfooter・bodyの位置移動フラグ

    private bool isCancel = false;  // cancelボタンが押されたか

    TypingGameManager typingGameManager;


    void Start()
    {
        inputField = this.gameObject.GetComponent<InputField>();
        parentRect = this.transform.parent.GetComponent<RectTransform>();
        typingGameManager = GameObject.Find("GameManager").GetComponent<TypingGameManager>();
        defaultParentPos = parentRect.localPosition;
        InitInputField();
    }

    void Update()
    {
        if (!typingGameManager.inGameEnable)
        {
            // フィールドの初期化
            InitInputField();

            if (inputField.readOnly)
            {
                inputField.readOnly = false;
            }
        }
        else
        {
            StartInputText();

            if (!inputField.readOnly)
            {
                inputField.readOnly = true;
            }
        }
    }

    // 入力開始時
    private void StartInputText()
    {
        if (inputField.isFocused && isOnceInput)
        {
            isOnceInput = false;
            // y軸をいい感じの値にする
            parentRect.localPosition += new Vector3(0, 940f, 0);
        }
    }

    // キーボードによって上にずれたUIの位置を戻す
    public void ResetKeybord()
    {
        isOnceInput = true;
        parentRect.localPosition = defaultParentPos;
        isCancel = false;
    }

    // フィールドの初期化
    private void InitInputField()
    {
        inputField.text = "";
        resultText = "";
        ResetKeybord();
    }

    // OnValueCangeで呼び出す関数
    public void ChangeText()
    {
        if (inputField.touchScreenKeyboard.status == TouchScreenKeyboard.Status.Canceled)
        {
            // Cancleを押した時
            isCancel = true;
        }
        else if (inputField.isFocused && !isCancel)
        {
            // 他のところをタップした時
            resultText = inputField.text;
            Debug.Log("inputText: " + resultText);
        }
    }

    // OnEndEditで呼び出す関数
    public void FinishEditText()
    {
        if (inputField.touchScreenKeyboard.status == TouchScreenKeyboard.Status.Done)
        {
            // 入力完了時何かに渡す
            typingGameManager.Decision(resultText);

            // フィールドの初期化
            InitInputField();
            Debug.Log("Done");
        }
        else if (inputField.touchScreenKeyboard.status == TouchScreenKeyboard.Status.Canceled || inputField.touchScreenKeyboard.status == TouchScreenKeyboard.Status.LostFocus)
        {
            // 入力キャンセル時もしくは
            inputField.text = resultText;
            ResetKeybord();
            Debug.Log("Canseled");
        }
        else
        {
            // 他の部分をタップした場合
            inputField.text = resultText;
            ResetKeybord();
            Debug.Log("Canseled");
        }
    }
}