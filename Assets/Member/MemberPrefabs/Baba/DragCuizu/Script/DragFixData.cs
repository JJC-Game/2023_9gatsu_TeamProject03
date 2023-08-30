using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using TMPro;
using UnityEngine.UI;
public class DragFixData : MonoBehaviour
{
    public List<Question> fixDataList;
    private int currentQuestionIndex = 0;
    public TextMeshProUGUI questionText;
    public Transform answerContainer;
    private TextMeshProUGUI[] answerTexts;
    int text;
    public string anserw;
    public List<string> Options = new List<string>();
    public List<string> NextOptions = new List<string>();
    public List<string> allOptions = new List<string>();
    public GameObject gridLayoutGroupObject;
    private GridLayoutGroup gridLayoutGroup;
    public GameObject anotherObject;
    private AnotherScript another;
    private float enableGridLayoutTime = 0.1f;
    private bool isDelaying = false;
    public TextMeshProUGUI[] now;
    public int nowNumber = 1;
    AnswerManager anserManager;
    int anserText;
    // Start is called before the first frame update
    void Awake()
    {
        gridLayoutGroup = gridLayoutGroupObject.GetComponent<GridLayoutGroup>();
        another = anotherObject.GetComponent<AnotherScript>();
        anserManager = gridLayoutGroup.GetComponent<AnswerManager>();
        // Create an instance of Question class
        Question question = new Question();

        // Access hiraganaList array
        string[] hiraganaList = question.hiraganaList;
        now[0].SetText("" + nowNumber);
        InitializeAnswerTexts();
        LoadFixData();
        Answer(currentQuestionIndex);
        Ansew();
        CrateAnswer();
        DisplayQuestion(currentQuestionIndex);
        // DisplayQuestion(currentQuestionIndex);
    }

    // Update is called once per frame
    void Update()
    {
        if (another.ok == true)
        {
            NextQuestion();
            another.ok = false;
        }
        if (gridLayoutGroup.enabled == true && !isDelaying)
        {
            StartCoroutine(DisableGridLayoutAfterDelay());
        }
        anserText = nowNumber - 1;
        now[1].SetText("" + anserText);
    }
    // ボタンを押した時に呼ばれる関数
    public void NextQuestion()
    {

        if (currentQuestionIndex < fixDataList.Count)
        {
            //anserManager.reduce();
            currentQuestionIndex++;
            Answer(currentQuestionIndex);
            CrateAnswer();
            DisplayQuestion(currentQuestionIndex);
            gridLayoutGroup.enabled = true;
            nowNumber++;
            now[0].SetText(""+nowNumber) ;
        }
        else
        {
            Debug.Log("クイズ終了");
            // クイズ終了の処理をここに追加
        }
    }
    private IEnumerator DisableGridLayoutAfterDelay()
    {
        isDelaying = true;
        yield return new WaitForSeconds(enableGridLayoutTime);
        gridLayoutGroup.enabled = false;
        isDelaying = false;
    }
    void InitializeAnswerTexts()
    {
        answerTexts = answerContainer.GetComponentsInChildren<TextMeshProUGUI>();
    }
    void Answer(int index)
    {
        text = answerTexts.Length - fixDataList[index].answer.Length;
        Debug.Log("ランダムで出るテキストの数" + text + "正解の数" + fixDataList[index].answer.Length);
    }
    void CrateAnswer()
    {
        allOptions.Clear();
        // Add correct answer characters
        foreach (char c in fixDataList[currentQuestionIndex].answer)
        {
            allOptions.Add(c.ToString());
        }

        // Add extra characters (shuffled hiragana)
        List<string> shuffledHiraganaList = new List<string>(fixDataList[currentQuestionIndex].hiraganaList); // Access hiraganaList from the current Question instance
        ShuffleList(shuffledHiraganaList);

        for (int i = 0; i < text; i++)
        {
            allOptions.Add(shuffledHiraganaList[i]);
        }

        // Shuffle all options
        ShuffleList(allOptions);

        // Display the options
        for (int i = 0; i < answerTexts.Length; i++)
        {
            answerTexts[i].text = allOptions[i];

            // Attach AnswerOptionComponent script to the answer characters
            AnswerOptionComponent answerOption = answerTexts[i].gameObject.AddComponent<AnswerOptionComponent>();

            answerOption.isCorrect = fixDataList[currentQuestionIndex].answer.Contains(allOptions[i]);
            answerOption.answerID = i;
        }
    }
    public void Ansew()
    {
        Options.Clear();
        NextOptions.Clear();
        // Add correct answer characters
        foreach (char c in fixDataList[currentQuestionIndex].answer)
        {
            // Debug.Log(fixDataList[currentQuestionIndex-1].answer);
            Options.Add(c.ToString());
        }
        foreach (char c in fixDataList[currentQuestionIndex+1].answer)
        {
            // Debug.Log(fixDataList[currentQuestionIndex-1].answer);
            NextOptions.Add(c.ToString());
        }

    }
    void LoadFixData()
    {
        fixDataList = new List<Question>(); // リストを初期化
        TextAsset csvFile;
        //csvFile = Resources.Load("DragQ/FixDataDrag") as TextAsset;
        csvFile = Resources.Load("DragQ/FixData2") as TextAsset;
        StringReader reader = new StringReader(csvFile.text);

        while (reader.Peek() > -1)
        {
            string line = reader.ReadLine();
            string[] elementArray = line.Split(',');
            // Debug.Log(line);

            Question newItemFixData = new Question();

            newItemFixData.number = int.Parse(elementArray[0]);
            newItemFixData.questionText = elementArray[1].Replace("\\n", "\n");
            newItemFixData.answer = elementArray[2];
            fixDataList.Add(newItemFixData);
        }
        ShuffleList(fixDataList);
    }
    void DisplayQuestion(int index)
    {
        if (index < fixDataList.Count)
        {
            questionText.text = fixDataList[index].questionText;
        }
        else
        {
            Debug.LogError("問題がありません");
        }
    }
    private List<T> ShuffleList<T>(List<T> list)
    {
        int n = list.Count;
        while (n > 1)
        {
            n--;
            int k = Random.Range(0, n + 1);
            T value = list[k];
            list[k] = list[n];
            list[n] = value;
        }
        return list;
    }
}
