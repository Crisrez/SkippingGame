using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Collections;

public class TimelineColors : MonoBehaviour
{
    [SerializeField] private Slider timelineSlider;
    [SerializeField] private RectTransform[] secciones; // Asigna aquí los RectTransforms de cada sección
    [SerializeField] private int[] sizeSecciones = new int[5];
    [SerializeField] private SubtitlesController guion;
    [SerializeField] private List<LineaSubtitles> guionList;
    [SerializeField] private float offsetSeccion; // Ajusta este valor para controlar el espacio entre secciones

    void Awake()
    {
        guionList = guion.GetGuionList(); // Asegúrate de que el guion esté cargado antes de actualizar el timeline
    }

    void Start()
    {
        StartCoroutine(Espera());
    }

    void Update()
    {
        ActualizarTimeLine();
    }

    private IEnumerator Espera()
    {
        yield return new WaitForSeconds(0.1f); // Espera un breve momento para asegurarte de que el guion esté cargado
        DefinirTamanosSecciones();
    }

    void DefinirTamanosSecciones()
    {
        int finalSize = guionList.Count;
        int seccion = 0;
        string seccionAnterior = guionList[0].skin;

        for (int i = 0; i < guionList.Count; i++)
        {
            if (seccionAnterior != guionList[i].skin)
            {
                sizeSecciones[seccion] = i;
                seccion++;
                seccionAnterior = guionList[i].skin;
            }
        }
        sizeSecciones[seccion] = finalSize;
    }

    void ActualizarTimeLine()
    {
        timelineSlider.maxValue = guionList.Count -1;
        timelineSlider.value = guion.indice;
        DefinirTamanosSecciones();
        OnSliderChanged();
    }

    public void OnSliderChanged()
    {
        int totalSize = guionList.Count - 1;
        
        for (int i = 0; i < secciones.Length; i++)
        {
            float inicio = (i == 0) ? 0 : sizeSecciones[i - 1];
            float fin = sizeSecciones[i];

            float porcentajeInicio = inicio / totalSize;
            float porcentajeFin = fin / totalSize;

            float progresoSeccion = Mathf.Clamp((timelineSlider.value - inicio) / (fin - inicio), 0, 1);
            float anchorMaxX = Mathf.Lerp(porcentajeInicio, porcentajeFin, progresoSeccion);

            secciones[i].anchorMin = new Vector2(porcentajeInicio, 0);
            secciones[i].anchorMax = new Vector2(anchorMaxX, 1);

            if (i > 0)
            {
                secciones[i].offsetMin = new Vector2(offsetSeccion, 0);
            }
            else
            {
                secciones[i].offsetMin = new Vector2(-offsetSeccion, 0);
            }

            secciones[i].offsetMax = new Vector2(0, 0);

        }

    }
}
