using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    [SerializeField] AudioMixer mixerMenu;
    [SerializeField] AudioMixer mixerGame;

    public static AudioManager Instance { get; private set; }

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

    public void ChangeVolume(float volume)
    {
        mixerMenu.SetFloat("MenuVol", volume);
        mixerGame.SetFloat("GameVol", volume);

        Debug.Log("El volumen general es: " + volume);
    }


}
