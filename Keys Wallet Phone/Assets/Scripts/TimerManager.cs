using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TimerManager : MonoBehaviour
{
    public TextMeshProUGUI timerText;
    public float timerDuration;
    private float _CurrentTime;

    public List<string> timerSteps = new List<string>();
    private int _CurrentStepIndex;
    private bool _IsResetting;

    private void Start()
    {
        _CurrentStepIndex = 0;
        _CurrentTime = timerDuration;
    }

    private void Update()
    {
        if (!_IsResetting)
        {
            _CurrentTime -= Time.deltaTime;

            // Clamp time to 0
            if (_CurrentTime <= 0f)
            {
                _CurrentTime = 0f;
                _IsResetting = true;
                StartCoroutine(ResetTimer());
            }
            UpdateTimerUI();
        }
    }

    private void UpdateTimerUI()
    {
        timerText.text = TimeSpan.FromSeconds(_CurrentTime).ToString(@"mm\:ss\.ff");
    }

    private IEnumerator ResetTimer()
    {
        yield return new WaitForSeconds(0.3f);

        timerText.text = timerSteps[_CurrentStepIndex];
        _CurrentStepIndex++;

        if (_CurrentStepIndex >= timerSteps.Count)
        {
            // Trigger end game
            Debug.Log("END GAME");
        }

        yield return new WaitForSeconds(1f);
        _CurrentTime = timerDuration;
        _IsResetting = false;
    }
}
