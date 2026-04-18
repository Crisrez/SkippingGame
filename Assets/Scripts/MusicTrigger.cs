using UnityEngine;

/// <summary>
/// Activa música cuando aparece la poll (sin tocar PollController)
/// </summary>
public class MusicTrigger : MonoBehaviour
{
    [Header("Poll UI")]
    [SerializeField] private GameObject pollPanel;

    [Header("Audio")]
    [SerializeField] private AudioSource pollMusic;

    private bool hasTriggered = false;
    private bool previousState = false;

    private void Start()
    {
        if (pollMusic != null)
        {
            pollMusic.Stop();
        }

        if (pollPanel != null)
        {
            previousState = pollPanel.activeInHierarchy;
        }
    }

    private void Update()
    {
        if (pollPanel == null || pollMusic == null)
            return;

        bool currentState = pollPanel.activeInHierarchy;

        // Detecta el momento exacto en que aparece la poll
        if (!previousState && currentState && !hasTriggered)
        {
            pollMusic.Play();
            hasTriggered = true;
        }

        previousState = currentState;
    }
}