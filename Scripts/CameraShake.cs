using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraShake : MonoBehaviour
{
    private CinemachineVirtualCamera _virtualCamera;
    private CinemachineBasicMultiChannelPerlin _cameraNoise;

    private void Start() {
        _virtualCamera = FindObjectOfType<CinemachineVirtualCamera>();
        _cameraNoise = _virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
    }

    public void Shake(float intensity, float duration, float fadeInTime = 0, float fadeOutTime = 0) {
        StopAllCoroutines();
        StartCoroutine(DoShake(intensity, duration, fadeInTime, fadeOutTime));
    }

    private IEnumerator DoShake(float intensity, float duration, float fadeInTime, float fadeOutTime) {
        float startTime = Time.time;
        _cameraNoise.m_AmplitudeGain = 0;

        //Fade In
        while(Time.time - startTime < fadeInTime) {
            float fadeAmount = Mathf.Lerp(0, intensity, (Time.time - startTime) / fadeInTime);
            _cameraNoise.m_AmplitudeGain = fadeAmount * intensity;
            yield return null;
        }

        //Main Shake
        while (Time.time - startTime < fadeInTime + duration)
        {
            _cameraNoise.m_AmplitudeGain = intensity;
            yield return null;
        }

        //Fade Out
        while (Time.time - startTime < fadeInTime + duration + fadeOutTime)
        {
            float fadeAmount = Mathf.Lerp(intensity, 0, (Time.time - (startTime + fadeInTime + duration)) / fadeInTime);
            _cameraNoise.m_AmplitudeGain = fadeAmount * intensity;
            yield return null;
        }

        _cameraNoise.m_AmplitudeGain = 0;
    }
}
