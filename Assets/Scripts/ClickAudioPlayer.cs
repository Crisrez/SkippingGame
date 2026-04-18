using UnityEngine;

public class ClickAudioPlayer : MonoBehaviour
{
    public AudioSource audioSource;

    public void PlayClickSound()
    {
        if (audioSource == null)
        {
            Debug.LogError("No hay AudioSource asignado en ClickAudioPlayer");
            return;
        }

        audioSource.Play();
    }
}