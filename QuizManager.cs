using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuizManager : MonoBehaviour
{
    [System.Serializable]
    public class Question
    {
        public string questionText;
        public string[] answers = new string[4];
        public int correctIndex;
    }

    [Header("UI")]
    public TextMeshProUGUI questionText;
    public TextMeshProUGUI questionCounterText;
    public Button[] answerButtons;

    [Header("Danh sách câu hỏi chia độ khó")]
    public List<Question> easyQuestions;
    public List<Question> mediumQuestions;
    public List<Question> hardQuestions;

    [Header("Kết quả")]
    public GameObject resultPanel;
    public TextMeshProUGUI resultText;
    public AudioSource winSound;
    public AudioSource loseSound;
    public Button closeButton;

    private List<Question> selectedQuestions;
    private int currentQuestion = 0;
    private int correctCount = 0;

    void Start()
    {
        gameObject.SetActive(false); // QuizPanel bị ẩn ban đầu
    }

    public void ResetQuiz()
    {
        currentQuestion = 0;
        correctCount = 0;
        resultPanel.SetActive(false);
        this.gameObject.SetActive(true);
        Time.timeScale = 0f;

        selectedQuestions = new List<Question>();
        selectedQuestions.AddRange(GetRandomQuestions(easyQuestions, 3));
        selectedQuestions.AddRange(GetRandomQuestions(mediumQuestions, 4));
        selectedQuestions.AddRange(GetRandomQuestions(hardQuestions, 3));

        ShowQuestion();
    }

    List<Question> GetRandomQuestions(List<Question> source, int count)
    {
        List<Question> temp = new List<Question>(source);
        List<Question> result = new List<Question>();

        for (int i = 0; i < count && temp.Count > 0; i++)
        {
            int rand = Random.Range(0, temp.Count);
            result.Add(temp[rand]);
            temp.RemoveAt(rand);
        }

        return result;
    }

    void ShowQuestion()
    {
        Question q = selectedQuestions[currentQuestion];
        questionText.text = q.questionText;
        questionCounterText.text = $"Câu {currentQuestion + 1}/10";

        for (int i = 0; i < answerButtons.Length; i++)
        {
            int index = i;
            answerButtons[i].GetComponentInChildren<TextMeshProUGUI>().text = q.answers[i];
            answerButtons[i].onClick.RemoveAllListeners();
            answerButtons[i].onClick.AddListener(() => OnAnswer(index));
        }
    }

    void OnAnswer(int index)
    {
        if (index == selectedQuestions[currentQuestion].correctIndex)
            correctCount++;

        currentQuestion++;

        if (currentQuestion < selectedQuestions.Count)
            ShowQuestion();
        else
            ShowResult();
    }

    void ShowResult()
    {
        resultPanel.SetActive(true);
        closeButton.gameObject.SetActive(true);
        if (correctCount >= 8)
        {
            resultText.text = $"Bạn đúng {correctCount}/10! Tuyệt vời!";
            winSound?.Play();
        }
        else
        {
            resultText.text = $"Bạn chỉ đúng {correctCount}/10. Học thêm nhé!";
            loseSound?.Play();
        }

        Time.timeScale = 0f;
    }

    public void CloseQuiz()
    {
        resultPanel.SetActive(false);
        this.gameObject.SetActive(false);
        Time.timeScale = 1f;
    }
}
