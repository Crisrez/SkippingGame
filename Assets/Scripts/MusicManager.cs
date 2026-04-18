using System.Collections;
using UnityEngine;

/// <summary>
/// Controla la música de la escena actual.
/// Reproduce, detiene, ajusta volumen y puede apagarse con fade out.
/// Al finalizar el fade, destruye este objeto.
/// </summary>
public class MusicManager : MonoBehaviour
{
    [Header("Music Source")]
    [SerializeField] private AudioSource musicSource;

    [Header("Fade Settings")]
    [SerializeField] private float fadeOutDuration = 1f;

    private Coroutine fadeCoroutine;
    private bool isFadingOut = false;

    private void Start()
    {
        PlayMusic();
    }

    /// <summary>
    /// Reproduce la música si no está sonando.
    /// </summary>
    public void PlayMusic()
    {
        if (musicSource == null)
        {
            Debug.LogError("MusicPlayer: no hay AudioSource asignado.");
            return;
        }

        if (musicSource.isPlaying) return;

        musicSource.Play();
    }

    /// <summary>
    /// Detiene la música inmediatamente.
    /// </summary>
    public void StopMusic()
    {
        if (musicSource == null)
        {
            Debug.LogError("MusicPlayer: no hay AudioSource asignado.");
            return;
        }

        musicSource.Stop();
    }

    /// <summary>
    /// Ajusta el volumen de la música.
    /// </summary>
    public void SetVolume(float volume)
    {
        if (musicSource == null)
        {
            Debug.LogError("MusicPlayer: no hay AudioSource asignado.");
            return;
        }

        musicSource.volume = Mathf.Clamp01(volume);
    }

    /// <summary>
    /// Devuelve el volumen actual.
    /// </summary>
    public float GetVolume()
    {
        if (musicSource == null)
        {
            Debug.LogError("MusicPlayer: no hay AudioSource asignado.");
            return 0f;
        }

        return musicSource.volume;
    }

    /// <summary>
    /// Inicia el fade out de la música y destruye este objeto al terminar.
    /// Ideal para llamarlo desde un botón o evento UI.
    /// </summary>
    public void FadeOutAndDestroy()
    {
        if (musicSource == null)
        {
            Debug.LogError("MusicPlayer: no hay AudioSource asignado.");
            return;
        }

        if (isFadingOut) return;

        if (fadeCoroutine != null)
        {
            StopCoroutine(fadeCoroutine);
        }

        fadeCoroutine = StartCoroutine(FadeOutAndDestroyCoroutine());
    }

    /// <summary>
    /// Hace fade out gradual, detiene la música y destruye el GameObject.
    /// </summary>
    private IEnumerator FadeOutAndDestroyCoroutine()
    {
        isFadingOut = true;

        float startVolume = musicSource.volume;
        float elapsedTime = 0f;

        while (elapsedTime < fadeOutDuration)
        {
            elapsedTime += Time.unscaledDeltaTime;
            float t = elapsedTime / fadeOutDuration;
            musicSource.volume = Mathf.Lerp(startVolume, 0f, t);
            yield return null;
        }

        musicSource.volume = 0f;
        musicSource.Stop();

        Destroy(gameObject);
    }
}