using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class SimpleMouseLook : MonoBehaviour
{
    public Transform player;
    public float mouseSensitivity;
    public float mouseSensitivityMultiplier;

    private float xRotation = 0f;
    private float yRotation = 0f;
    private Vector2 _BaseMouseRotation;

    private CinemachineVirtualCamera _Vcam;
    private CinemachineBasicMultiChannelPerlin _Noise;

    // Single instance shake
    private IEnumerator m_CurrentShake;

    // Individual noise parameters, added on top of eachother
    private float m_CurrentShakeAmplitude;
    private float m_CurrentShakeFrequency;
    public float CurrentTrembleAmplitude { get; set; }
    public float CurrentTrembleFrequency { get; set; }

    private void Awake()
    {
        _Vcam = GetComponent<CinemachineVirtualCamera>();
        _Noise = _Vcam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
    }

    private void OnEnable()
    {
        DropSystem.playerCollided += Shake;
    }

    private void OnDisable()
    {
        DropSystem.playerCollided -= Shake;
    }

    private void Update()
    {
        MouseRotation();
        _Noise.m_AmplitudeGain = CurrentTrembleAmplitude + m_CurrentShakeAmplitude;
        _Noise.m_FrequencyGain = CurrentTrembleFrequency + m_CurrentShakeFrequency;
    }

    private void Shake()
    {
        SimpleShake(0.7f, 1f, 20f);
    }

    private void MouseRotation()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * mouseSensitivityMultiplier;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * mouseSensitivityMultiplier;

        xRotation -= mouseY;
        yRotation -= mouseX;

        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        player.localRotation = Quaternion.Euler(0f, -yRotation, 0f);
    }

    public void SimpleShake(float timer, float amplitudeGain, float frequencyGain)
    {
        StartCoroutine(SimpleShakeTimer(timer, amplitudeGain, frequencyGain));
    }

    private IEnumerator SimpleShakeTimer(float timer, float amplitudeGain, float frequencyGain)
    {
        m_CurrentShakeAmplitude += amplitudeGain;
        m_CurrentShakeFrequency += frequencyGain;

        yield return new WaitForSeconds(timer);

        m_CurrentShakeAmplitude -= amplitudeGain;
        m_CurrentShakeFrequency -= frequencyGain;
    }
}
