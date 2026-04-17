using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    [SerializeField] private AudioSource audioMenu;
    [SerializeField] private AudioSource audioGame;

    public static MusicPlayer Instance { get; private set; }

    public void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void PlayMusic()
    {
        if (audioMenu.isPlaying) return;
        audioMenu.Play();
    }

    public void StopMusic()
    {
        audioMenu.Stop();
    }

    public void SetVolume(float volume)
    {
        audioGame.volume = volume;
        audioMenu.volume = volume;
    }

    public float GetVolume()
    {
        return audioGame.volume;
    }

    public void ChangeMusic()
    {
        audioMenu.Stop();
        audioMenu.Play();
        audioGame.Play();
    }
}
