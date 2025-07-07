using UnityEngine;
using TMPro;

public class NPCNameDisplay : MonoBehaviour
{
    [Header("Gán trong Inspector")]
    public GameObject nameBox;               // ← Box chứa IMG + Text
    public TextMeshProUGUI nameText;         // ← Component Text (trong Box)
    [Tooltip("Tên NPC sẽ hiển thị khi lại gần")]
    public string npcName = "👤 Nhân vật";

    private string originalText = "";
    private bool playerInRange = false;
    private bool hasShownFullText = false;

    void Start()
    {
        if (nameBox != null)
            nameBox.SetActive(false);

        if (nameText != null)
        {
            originalText = nameText.text; // Lưu nội dung lời thoại có sẵn
            nameText.text = "";           // Ẩn ban đầu
        }
    }

    void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.E))
        {
            if (nameText != null && !hasShownFullText)
            {
                nameText.text = originalText; // Hiện lời thoại
                hasShownFullText = true;
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
            hasShownFullText = false;

            if (nameBox != null)
                nameBox.SetActive(true);

            if (nameText != null)
                nameText.text = npcName; // Hiện tên NPC riêng
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;

            if (nameBox != null)
                nameBox.SetActive(false);

            if (nameText != null)
                nameText.text = ""; // Ẩn khi đi xa
        }
    }
}
