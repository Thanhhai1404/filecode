using UnityEngine;
using TMPro;

public class NPCChat : MonoBehaviour
{
    public GameObject talkUI;
    public TextMeshProUGUI talkText;
    public string[] dialogueLines;

    private bool playerNear = false;

    void Update()
    {
        if (playerNear && Input.GetKeyDown(KeyCode.E))
        {
            string line = dialogueLines[Random.Range(0, dialogueLines.Length)];
            talkText.text = line;
            talkUI.SetActive(true);
            Invoke("HideTalk", 3f);
        }

        if (playerNear)
        {
            Debug.Log("Đang ở gần NPC");

            if (Input.GetKeyDown(KeyCode.E))
            {
                Debug.Log("Đã nhấn phím E");
                string line = dialogueLines[Random.Range(0, dialogueLines.Length)];
                talkText.text = line;
                talkUI.SetActive(true);
                Invoke("HideTalk", 3f);
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerNear = true;
            talkUI.SetActive(true);
            talkText.text = "Nhấn E để nói chuyện";
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

    void HideTalk()
    {
        talkUI.SetActive(false);
    }
}
