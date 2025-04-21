using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BoxCounter : MonoBehaviour
{
    public static BoxCounter Instance { get; set; }

    private int count = 0;

    private TextMeshProUGUI counterText;

    public Timer _timer;
    private void Awake()
    {
        GameObject obj = GameObject.Find("Counter");

        if (obj != null)
        {
            counterText = obj.GetComponent<TextMeshProUGUI>();
        }
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void IncrementCounter()
    {
        count++;
        _timer.countdown += 15;
        counterText.text = "BoxCount: " + count;
    }
}
