using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class HoverEffect : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public CanvasGroup sliderCanvasGroup; // Arrastra el Slider aquí
    public Image fondoImagen;

    void Start()
    {
        sliderCanvasGroup.alpha = 0;
        sliderCanvasGroup.blocksRaycasts = false; // Para que no estorbe al inicio
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        SetOpacity(1.0f);
        sliderCanvasGroup.alpha = 1;
        sliderCanvasGroup.blocksRaycasts = true; // Ahora puedes interactuar con él
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        // Solo ocultamos si el mouse NO está sobre el slider o la imagen
        // Unity maneja esto automáticamente si el Slider es hijo
        SetOpacity(0.5f);
        sliderCanvasGroup.alpha = 0;
        sliderCanvasGroup.blocksRaycasts = false;
    }

    private void SetOpacity(float alpha)
    {
        Color c = fondoImagen.color;
        c.a = alpha;
        fondoImagen.color = c;
    }
}
