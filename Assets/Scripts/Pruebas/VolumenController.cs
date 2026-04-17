using UnityEngine;
using UnityEngine.UI;

public class VolumenController : MonoBehaviour
{
    [SerializeField] private Slider slider;

    void Awake()
    {
        slider = gameObject.GetComponent<Slider>();
    }

    void Start()
    {
        slider.gameObject.SetActive(false);
        slider.value = MusicPlayer.Instance.GetVolume();
        slider.onValueChanged.AddListener(SetVolumen);
    }

    void SetVolumen(float value)
    {
        MusicPlayer.Instance.SetVolume(value);
    }

    public void ShowSlider()
    {
        slider.gameObject.SetActive(true);
    }

    public void HideSlider()
    {
        slider.gameObject.SetActive(false);
    }

}
