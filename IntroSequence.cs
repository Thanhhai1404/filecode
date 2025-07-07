using UnityEngine;

public class IntroSequence : MonoBehaviour
{
    public GameObject panel1;
    public GameObject panel2;
    public GameObject panel3;

    private void Start()
    {
        Time.timeScale = 0f;

        panel1.SetActive(true);
        panel2.SetActive(false);
        panel3.SetActive(false);
    }

    public void ShowPanel2()
    {
        panel1.SetActive(false);
        panel2.SetActive(true);
    }

    public void ShowPanel3()
    {
        panel2.SetActive(false);
        panel3.SetActive(true);
    }

    public void FinishIntroAndStartGame()
    {
        panel3.SetActive(false);
        Time.timeScale = 1f; 
    }
}
