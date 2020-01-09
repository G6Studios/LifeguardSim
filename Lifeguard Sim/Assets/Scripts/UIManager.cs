using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    private TextMeshProUGUI test;
    private TextMeshProUGUI timerText;
    private SwimmerManager s_manager;
    public GameObject diagnosisOptions;

    // Start is called before the first frame update
    private void Start()
    {
        s_manager = GameObject.Find("Game Core").GetComponent<SwimmerManager>();
        test = GameObject.Find("Normal Swimmers").GetComponent<TextMeshProUGUI>();
        timerText = GameObject.Find("Timer Text").GetComponent<TextMeshProUGUI>();
        diagnosisOptions.SetActive(false);
    }

    // Update is called once per frame
    private void Update()
    {
        SwimmersText();
    }

    public void TimerText(string text)
    {
        timerText.text = text;
    }

    private void SwimmersText()
    {
        test.text = "Normal swimmers: " + s_manager.normalSwimmers;
    }
}