using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;
using TMPro;

public class TimerManager : MonoBehaviour
{
    public static Action timerStep;

    public TextMeshProUGUI timerText;
    public Volume volume;
    private ColorAdjustments _ColorAdj;
    private ChromaticAberration _ChromaticAb;
    public float timerDuration;
    private float _CurrentTime;

    private string _BaseText = "";
    private string _LastText = "";

    public List<string> timerSteps = new List<string>();
    private int _CurrentStepIndex;
    private bool _IsResetting;

    private void Start()
    {
        _CurrentStepIndex = 0;
        _CurrentTime = timerDuration;
        volume.profile.TryGet(out _ColorAdj);
        volume.profile.TryGet(out _ChromaticAb);
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
        timerText.text = _BaseText + TimeSpan.FromSeconds(_CurrentTime).ToString(@"mm\:ss\.ff");
    }

    private IEnumerator ResetTimer()
    {
        yield return new WaitForSeconds(0.3f);

        _BaseText += timerSteps[_CurrentStepIndex] + "\n";

        timerText.text = _BaseText;
        _CurrentStepIndex++;
        
        _ColorAdj.saturation.value -= 10;
        _ChromaticAb.intensity.value += 0.1f;

        if (_CurrentStepIndex >= timerSteps.Count)
        {
            // Trigger end game
            Debug.Log("END GAME");
            LevelManager.instance.LoadScene("LoseScreen", Color.red);            
        }

        else
        {
            if (timerStep != null)
                timerStep();
        }

        yield return new WaitForSeconds(1f);
        _CurrentTime = timerDuration;
        _IsResetting = false;
    }
}
