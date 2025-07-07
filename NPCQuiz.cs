using UnityEngine;
using TMPro;

public class NPCQuiz : MonoBehaviour
{
    public GameObject talkUI;
    public TextMeshProUGUI talkText;
    public QuizManager quizManager; 

    private bool playerNear = false;

    void Update()
    {
        if (playerNear && Input.GetKeyDown(KeyCode.E))
        {
            quizManager.ResetQuiz();   
            talkUI.SetActive(false);  
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerNear = true;
            talkUI.SetActive(true);
            talkText.text = "Bạn có muốn chơi một trò chơi nhỏ với tôi không?";
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerNear = false;
            talkUI.SetActive(false);
        }
    }
}
