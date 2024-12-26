using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class starTimer : MonoBehaviour
{
    public float time;
    public bool startTimer;
    [SerializeField] TextMeshProUGUI TimeText;
    public GameObject firstStar;
    public GameObject secondStar;
    public GameObject thirdStar;

    void Update()
    {
        if (startTimer)
            Timer();
    }
    private void Timer()
    {
        time += Time.deltaTime;
        TimeSpan timerCalculated = TimeSpan.FromSeconds(time);
        TimeText.text = timerCalculated.ToString(@"mm\:ss");
    }
}
