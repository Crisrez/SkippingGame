using UnityEngine;
using UnityEngine.UI;

public class VolumenController : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private float volumen = 1f;
    [SerializeField] private Slider slider;

    void Awake()
    {
        audioSource.Play();
    }

    void Start()
    {
        slider.value = volumen;
        slider.onValueChanged.AddListener(SetVolumen);
    }

    void Update()
    {
        audioSource.volume = volumen;
    }

    void SetVolumen(float value)
    {
        volumen = value;
        audioSource.volume = volumen; 
    }
}
