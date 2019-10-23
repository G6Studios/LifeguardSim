using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject swimmer;
    public GameObject errupter;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Instantiate(swimmer);

        if(Input.GetKeyDown(KeyCode.Space))
        {
            errupter.SetActive(true);
            
        }

        if(errupter.gameObject.active)
        {
            errupter.transform.Translate(Vector3.up * 2);
        }
    }
}
