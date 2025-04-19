using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TextTriggers : MonoBehaviour
{
    [SerializeField] private GameObject alleyOne;
    [SerializeField] private GameObject alleyTwo;
    [SerializeField] private GameObject alleyThree;
    private void OnTriggerEnter(Collider other)
    {
        if (this.gameObject.name == "Trigger_1" || this.gameObject.name == "Trigger_1 (1)")
        {
            if (other.tag == "Player")
            {
                alleyOne.SetActive(true);
            }
        }
        
        if (this.gameObject.name == "Trigger_2" || this.gameObject.name == "Trigger_2 (1)")
        {
            if (other.tag == "Player")
            {
                alleyTwo.SetActive(true);
            }
        }
        
        if (this.gameObject.name == "Trigger_3" || this.gameObject.name == "Trigger_3 (1)")
        {
            if (other.tag == "Player")
            {
                alleyThree.SetActive(true);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            alleyOne.SetActive(false);
            alleyTwo.SetActive(false);
            alleyThree.SetActive(false); 
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
