using UnityEngine;
using UnityEngine.UI;

public class NoLightCrossZone : MonoBehaviour
{
    public GameObject safeCrossPanel;
    public Button confirmButton;



    private static bool hasShownWarning = false;

    private void Start()
    {
        if (safeCrossPanel != null)
            safeCrossPanel.SetActive(false);

        if (confirmButton != null)
            confirmButton.onClick.AddListener(HidePanel);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !hasShownWarning)
        {
            hasShownWarning = true; 

            if (safeCrossPanel != null)
                safeCrossPanel.SetActive(true);

            Time.timeScale = 0f;
        }
    }

    private void HidePanel()
    {
        if (safeCrossPanel != null)
            safeCrossPanel.SetActive(false);

        Time.timeScale = 1f;
    }
}
