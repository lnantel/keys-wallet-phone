using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TimerManager : MonoBehaviour
{
    private float timerDuration;
    private float currentTime;
    private List<string> _TimerSteps = new List<string>();

    private void Update()
    {
        currentTime += Time.deltaTime;
    }

    private void UpdateTimerUI()
    {

    }
}
