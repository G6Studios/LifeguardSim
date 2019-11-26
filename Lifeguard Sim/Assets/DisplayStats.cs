using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DisplayStats : MonoBehaviour
{
    public GameObject normalSwimmers;
    public GameObject weakSwimmers;
    public GameObject exhaustedSwimmers;
    public GameObject injuredSwimmers;

    public GameObject correctfullyAnswered;
    public GameObject wrongfullyAnswered;

    private playerstats statistics;
    // Start is called before the first frame update
    void Start()
    {
        if(GameObject.FindGameObjectWithTag("PlayerStats") != null)
        {
        statistics = GameObject.FindGameObjectWithTag("PlayerStats").GetComponent<playerstats>();
        normalSwimmers.GetComponent<TextMeshProUGUI>().text = statistics.normalSwimmers.ToString();
        weakSwimmers.GetComponent<TextMeshProUGUI>().text = statistics.weakSwimmers.ToString();
        exhaustedSwimmers.GetComponent<TextMeshProUGUI>().text = statistics.exhaustedSwimmers.ToString();
        injuredSwimmers.GetComponent<TextMeshProUGUI>().text = statistics.injuredSwimmers.ToString();
        correctfullyAnswered.GetComponent<TextMeshProUGUI>().text = statistics.playerCorrectAnswer.ToString();
        wrongfullyAnswered.GetComponent<TextMeshProUGUI>().text = statistics.playerWrongAnswer.ToString();
        }
        else
        {
            normalSwimmers.GetComponent<TextMeshProUGUI>().text = "0";
            weakSwimmers.GetComponent<TextMeshProUGUI>().text = "0";
            exhaustedSwimmers.GetComponent<TextMeshProUGUI>().text = "0";
            injuredSwimmers.GetComponent<TextMeshProUGUI>().text = "0";
            correctfullyAnswered.GetComponent<TextMeshProUGUI>().text = "0";
            wrongfullyAnswered.GetComponent<TextMeshProUGUI>().text = "0";
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
