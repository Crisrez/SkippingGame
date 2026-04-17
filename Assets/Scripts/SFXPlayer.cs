using UnityEngine;
using UnityEngine.Audio;

public class SFXPlayer : MonoBehaviour
{
    private AudioSource audioSource;

    private void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);
        audioSource = GetComponent<AudioSource>();
    }

    public void PlaySFX()
    {
        audioSource.Play();
    }

    public void StopSFX()
    {
        audioSource.Stop();
    }
}
