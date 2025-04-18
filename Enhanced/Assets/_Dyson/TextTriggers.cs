using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TextTriggers : MonoBehaviour
{
    [SerializeField] private GameObject alleyOne; 
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            alleyOne.SetActive(true);
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
