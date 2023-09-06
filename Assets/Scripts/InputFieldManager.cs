using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InputFieldManager : MonoBehaviour
{
    private InputField inputField;
    public string resultText;   // 入力されたテキストを格納

    private bool isCancel = false;  // cancelボタンが押されたか

    TypingGameManager typingGameManager;


    void Start()
    {
        inputField = this.gameObject.GetComponent<InputField>();
        typingGameManager = GameObject.Find("GameManager").GetComponent<TypingGameManager>();
        InitInputField();
    }

    private void Update()
    {
        if (typingGameManager.inGameEnable && !inputField.isFocused)
        {
            TouchScreenKeyboard.Open("文字を入力してください", TouchScreenKeyboardType.Default);
        }
    }

    public void ResetKeybord()
    {
        TouchScreenKeyboard.Open("文字を入力してください", TouchScreenKeyboardType.Default);
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
            Debug.Log("Canseled");
        }
    }
}