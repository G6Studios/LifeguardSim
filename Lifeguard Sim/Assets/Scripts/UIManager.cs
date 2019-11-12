using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{

    TextMeshProUGUI test;
    SwimmerManager s_manager;
    public GameObject diagnosisOptions;

    // Start is called before the first frame update
    void Start()
    {
        s_manager = GameObject.Find("Game Core").GetComponent<SwimmerManager>();
        test = GameObject.Find("Normal Swimmers").GetComponent<TextMeshProUGUI>();
        diagnosisOptions.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        SwimmersText();
    }


    void SwimmersText()
    {

        test.text = "Normal swimmers: " + s_manager.normalSwimmers;


    }
}
