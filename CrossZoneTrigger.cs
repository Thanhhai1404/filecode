using UnityEngine;
using UnityEngine.UI;

public class CrossZoneTrigger : MonoBehaviour
{

    public TrafficLight trafficLight;   // Đèn giao thông đang hoạt động
    public GameObject warningPanel;     // Bảng cảnh báo vượt đèn đỏ
    public Button continueButton;       // Nút "Tiếp tục"
    public GameObject screenFlash;     // UI nền đỏ nhấp nháy
    public AudioSource alertAudio;     // Nhạc cảnh báo

    private bool isInDangerZone = false;
    private float flashTimer = 0f;

    private void Start()
    {
        // Gán sự kiện khi bấm nút
        if (continueButton != null)
        {
            continueButton.onClick.AddListener(ResumeGame);
        }

        if (warningPanel != null)
        {
            warningPanel.SetActive(false);
        }
    }

    private void Update()
    {
        if (isInDangerZone && screenFlash != null)
        {
            flashTimer += Time.unscaledDeltaTime * 5f; // tốc độ nháy
            float alpha = Mathf.Abs(Mathf.Sin(flashTimer)) * 0.4f; // nhấp nháy mờ mờ
            var color = screenFlash.GetComponent<UnityEngine.UI.Image>().color;
            color.a = alpha;
            screenFlash.GetComponent<UnityEngine.UI.Image>().color = color;
        }
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (trafficLight != null && trafficLight.CurrentLightIsRed())
            {
                Debug.Log("🚨 Vượt đèn đỏ! Hiện cảnh báo.");

                if (warningPanel != null)
                    warningPanel.SetActive(true);

                if (screenFlash != null)
                    screenFlash.SetActive(true);

                if (alertAudio != null && !alertAudio.isPlaying)
                    alertAudio.Play();

                isInDangerZone = true;
                Time.timeScale = 0f;
            }
        }
    }


    public void ResumeGame()
    {
        Debug.Log("⏯ Tiếp tục chơi");

        if (warningPanel != null)
            warningPanel.SetActive(false);

        Time.timeScale = 1f;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("✅ Người chơi đã rời khỏi vùng cấm");

            if (screenFlash != null)
                screenFlash.SetActive(false);

            if (alertAudio != null && alertAudio.isPlaying)
                alertAudio.Stop();

            isInDangerZone = false;
        }
    }
}

