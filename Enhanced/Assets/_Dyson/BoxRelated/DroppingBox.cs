using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroppingBox : MonoBehaviour
{
    private BoxCounter _boxCounter;

    private void Awake()
    {
        _boxCounter = BoxCounter.Instance;
    }

    private void OnCollisionEnter(Collision other)
    {
        if (this.gameObject.name == "RedBox(Clone)" && other.gameObject.name == "DropBoxRed")
        {
            Destroy(gameObject);
            _boxCounter.IncrementCounter();
        }
        if (this.gameObject.name == "BlueBox(Clone)" && other.gameObject.name == "DropBoxBlue")
        {
            Destroy(gameObject);
            _boxCounter.IncrementCounter();
        }
        
        if (this.gameObject.name == "GreenBox(Clone)" && other.gameObject.name == "DropBoxGreen")
        {
            Destroy(gameObject);
            _boxCounter.IncrementCounter();
        }
    }
}
