using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI timerText;
    public float countdown;
    void Update()
    {
        countdown -= Time.deltaTime;
        int minutes = Mathf.FloorToInt(countdown / 60);
        int seconds = Mathf.FloorToInt(countdown % 60); 
        timerText.text = "Timer: " + string.Format("{0:00}:{1:00}", minutes, seconds);

        if (countdown <= 0)
        {
            countdown = 0;
        }
    }
}
