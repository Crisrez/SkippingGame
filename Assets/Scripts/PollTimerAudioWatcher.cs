using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Observa el estado visual del timer y reproduce:
/// - un loop continuo mientras el timer corre
/// - un one-shot final cuando el tiempo llega a cero
/// No modifica la lógica del PollController.
/// </summary>
public class PollTimerAudioWatcher : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject pollPanel;
    [SerializeField] private Slider sliderTimer;

    [Header("Loop Audio")]
    [SerializeField] private AudioSource timerLoopSource;

    [Header("End Audio")]
    [SerializeField] private AudioSource timeUpSource;
    [SerializeField] private AudioClip timeUpClip;

    [Header("Settings")]
    [SerializeField] private float minSliderValueToPlay = 0.01f;

    private float previousSliderValue;
    private bool isTimerLoopPlaying = false;
    private bool hasPlayedTimeUp = false;
    private bool timerWasRunning = false;

    private void Start()
    {
        if (sliderTimer != null)
            previousSliderValue = sliderTimer.value;

        if (timerLoopSource != null)
            timerLoopSource.Stop();
    }

    private void Update()
    {
        if (pollPanel == null || sliderTimer == null || timerLoopSource == null)
            return;

        bool pollIsVisible = pollPanel.activeInHierarchy;
        bool sliderHasTimeLeft = sliderTimer.value > minSliderValueToPlay;
        bool sliderIsChanging = !Mathf.Approximately(sliderTimer.value, previousSliderValue);

        bool timerIsRunning = pollIsVisible && sliderHasTimeLeft && sliderIsChanging;

        // Si el timer arrancó, habilitamos el loop y reseteamos el flag del sonido final
        if (timerIsRunning)
        {
            StartTimerLoop();
            timerWasRunning = true;
            hasPlayedTimeUp = false;
        }

        // Si el tiempo terminó después de haber estado corriendo, disparamos el final
        if (timerWasRunning && pollIsVisible && !sliderHasTimeLeft && !hasPlayedTimeUp)
        {
            StopTimerLoop();
            PlayTimeUpSound();

            hasPlayedTimeUp = true;
            timerWasRunning = false;
        }

        // Si el poll desaparece por cualquier otro motivo, apagamos el loop
        if (!pollIsVisible && isTimerLoopPlaying)
        {
            StopTimerLoop();
        }

        previousSliderValue = sliderTimer.value;
    }

    private void StartTimerLoop()
    {
        if (isTimerLoopPlaying)
            return;

        timerLoopSource.Play();
        isTimerLoopPlaying = true;
    }

    private void StopTimerLoop()
    {
        if (!isTimerLoopPlaying)
            return;

        timerLoopSource.Stop();
        isTimerLoopPlaying = false;
    }

    private void PlayTimeUpSound()
    {
        if (timeUpSource == null || timeUpClip == null)
            return;

        timeUpSource.PlayOneShot(timeUpClip);
    }
}