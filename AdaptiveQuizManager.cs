using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AdaptiveQuizManager : MonoBehaviour
{
    [Header("Danh sách câu hỏi")]
    public List<Question> easyQuestions;
    public List<Question> mediumQuestions;
    public List<Question> hardQuestions;

    private List<Question> selectedQuestions;
    private int currentQuestion;
    private int correctCount;

    [Header("UI")]
    public TextMeshProUGUI questionText;
    public TextMeshProUGUI questionCounterText; // ← Đừng quên kéo vào trong Inspector
    public Button[] answerButtons;
    public GameObject resultPanel;
    public TextMeshProUGUI resultText;

    [Header("Âm thanh")]
    public AudioSource winSound;
    public AudioSource loseSound;

    [Header("Khởi đầu")]
    public GameObject quizPanel; // ← Panel chứa UI Quiz, dùng để bật/tắt

    void Start()
    {
        // Ẩn panel và kết quả khi khởi động game
        if (quizPanel != null) quizPanel.SetActive(false);
        if (resultPanel != null) resultPanel.SetActive(false);
    }

    /// <summary>
    /// Gọi hàm này khi muốn bắt đầu lại quiz (ví dụ khi ấn E)
    /// </summary>
    public void ResetQuiz()
    {
        Time.timeScale = 0f; // Tạm dừng game khi chơi quiz

        currentQuestion = 0;
        correctCount = 0;
        resultPanel.SetActive(false);

        selectedQuestions = new List<Question>();
        selectedQuestions.AddRange(GetRandomQuestions(easyQuestions, 3));
        selectedQuestions.AddRange(GetRandomQuestions(mediumQuestions, 4));
        selectedQuestions.AddRange(GetRandomQuestions(hardQuestions, 3));

        ShowQuestion();
    }

    List<Question> GetRandomQuestions(List<Question> source, int count)
    {
        List<Question> list = new List<Question>(source);
        List<Question> result = new List<Question>();

        for (int i = 0; i < count && list.Count > 0; i++)
        {
            int rand = Random.Range(0, list.Count);
            result.Add(list[rand]);
            list.RemoveAt(rand);
        }

        return result;
    }

    void ShowQuestion()
    {
        if (currentQuestion >= selectedQuestions.Count) return;

        Question q = selectedQuestions[currentQuestion];
        questionText.text = q.questionText;

        if (questionCounterText != null)
            questionCounterText.text = $"Câu {currentQuestion + 1} / {selectedQuestions.Count}";

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
        {
            correctCount++;
        }

        currentQuestion++;

        if (currentQuestion < selectedQuestions.Count)
        {
            ShowQuestion();
        }
        else
        {
            ShowResult();
        }
    }

    void ShowResult()
    {
        resultPanel.SetActive(true);

        if (correctCount >= 8)
        {
            resultText.text = $"Chúc mừng! Bạn đã đúng {correctCount}/10 câu!";
            if (winSound != null) winSound.Play();
        }
        else
        {
            resultText.text = $"Bạn chỉ đúng {correctCount}/10. Học lại nhé!";
            if (loseSound != null) loseSound.Play();
        }

        Time.timeScale = 0f; // Giữ game dừng để xem kết quả
    }

    public void CloseQuiz()
    {
        if (quizPanel != null) quizPanel.SetActive(false);
        Time.timeScale = 1f; // Tiếp tục game sau khi đóng quiz
    }

    [System.Serializable]
    public class Question
    {
        public string questionText;
        public string[] answers = new string[4];
        public int correctIndex;
    }
}
