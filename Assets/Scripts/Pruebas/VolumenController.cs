using UnityEngine;
using UnityEngine.UI;

public class VolumenController : MonoBehaviour
{
    [SerializeField] private Slider slider;
    [SerializeField] private Image imageVolume;

    [SerializeField] private Sprite highSprite;
    [SerializeField] private Sprite midSprite;
    [SerializeField] private Sprite lowSprite;

    void Awake()
    {
        //slider = gameObject.GetComponent<Slider>();
    }

    void Start()
    {
        slider.gameObject.SetActive(false);
        slider.value = UserInfo.Instance.GetVolumenGeneral();
        slider.onValueChanged.AddListener(SliderChange);
        slider.onValueChanged.AddListener(SpriteVolume);
    }

    void SliderChange(float value)
    {
        UserInfo.Instance.SetVolumenGeneral(value);
        AudioManager.Instance.ChangeVolume(UserInfo.Instance.GetVolumenGeneral());
    }

    public void ShowHideSlider()
    {
        slider.gameObject.SetActive(!slider.gameObject.activeSelf);
    }

    private void SpriteVolume(float value)
    {
        if (value >= 0.6f)
        {
            imageVolume.sprite = highSprite;
        }
        else if (value >= 0.3f)
        {
            imageVolume.sprite = midSprite;
        }
        else
        {
            imageVolume.sprite = lowSprite;
        }
    }

}
