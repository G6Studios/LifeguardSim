using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerstats : MonoBehaviour
{
    [SerializeField]
    SwimmerManager info;

    public int normalSwimmers; // Number of normal swimmers
    public int weakSwimmers; // Number of weak swimmers
    public int exhaustedSwimmers; // Number of exhausted swimmers
    public int injuredSwimmers; // Number of swimmers with broken bones

    public int playerCorrectAnswer = 0;
    public int playerWrongAnswer = 0;

    // Start is called before the first frame update
    void Start()
    {
        //info = GameObject.FindGameObjectWithTag("GameCore").GetComponent<SwimmerManager>();
        DontDestroyOnLoad(this.gameObject);
    }


    public void resetStatistics()
    {
        
        normalSwimmers = info.normalSwimmers;
        weakSwimmers = info.weakSwimmers;
        exhaustedSwimmers = info.exhaustedSwimmers;
        injuredSwimmers = info.injuredSwimmers;

        playerCorrectAnswer = 0;
        playerWrongAnswer = 0;
    }
}
