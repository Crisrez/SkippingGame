using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;


public class SubtitlesController : MonoBehaviour
{
    public TextMeshProUGUI subtitle;
    public PaqueteSubtitles guionJson;
    [SerializeField] private List<LineaSubtitles> guionList = new List<LineaSubtitles>();
    [SerializeField] private PollController pollController;

    private bool pollTriggered = false;

    public Button botonSiguiente;
    public Button botonAtras;
    public Animator animator;

    public int indice = 0;
    public int maxIndice;
    public int indiceEnd;

    [SerializeField] private float timerGeneral = 0f;
    [SerializeField] private float tiempoEntreLineas = 4.5f; // Tiempo en segundos entre cada línea

    public string skinActual;
    public string skinPrevious;


    void Start()
    {
        TextAsset archivo = Resources.Load<TextAsset>("guion");

        if (archivo != null)
        {
            guionJson = JsonUtility.FromJson<PaqueteSubtitles>(archivo.text);

            if (guionJson != null && guionJson.guionBD != null)
            {
                ConvertirAListaSinEnd();
            }
        }
        else
        {
            Debug.LogError("Ojo: El JSON no se encuentra en la carpeta Resources o el nombre está mal.");
        }

    }

    private void ConvertirAListaSinEnd()
    {
        for (int i = 0; i < guionJson.guionBD.Length; i++)
        {
            if (guionJson.guionBD[i].skin != "end")
            {
                guionList.Add(guionJson.guionBD[i]);
            }
            else
            {
                indiceEnd = i;
                maxIndice = guionList.Count - 1;
                indice = maxIndice;
                botonSiguiente.gameObject.SetActive(false);
                botonAtras.gameObject.SetActive(false);
                Comienzo();
                break;
            }
        }
    }

    void Update()
    {
        timerGeneral += Time.deltaTime;

        if (pollTriggered) { return; }

        if (timerGeneral >= tiempoEntreLineas)
        {
            if (guionList.Count != guionJson.guionBD.Length)
            {
                Comienzo();
                timerGeneral = 0f; // Reiniciar el temporizador
            }
            else
            {
                botonAtras.gameObject.SetActive(true);
                pollController.PollActivated();
                pollTriggered = true;
            }
        }

    }

    private void Comienzo()
    {
        guionList.Add(guionJson.guionBD[guionList.Count]);
        indice = guionList.Count - 1;
        maxIndice = indice;
        ActualizarStream();
    }

    public void AvanzarLínea()
    {
        indice++;
        botonAtras.gameObject.SetActive(true);

        if (indice >= maxIndice)
        {
            indice = maxIndice;
            botonSiguiente.gameObject.SetActive(false);
        }

        ActualizarStream();
    }

    public void RetrocederLínea()
    {
        indice--;
        botonSiguiente.gameObject.SetActive(true);

        if (indice <= 0)
        {
            indice = 0;
            botonAtras.gameObject.SetActive(false);
        }

        ActualizarStream();
    }

    public void ActualizarStream()
    {
        subtitle.text = guionList[indice].text;
        skinActual = guionJson.guionBD[indice].skin;

        if (skinActual != skinPrevious)
        {
            switch (skinActual)
            {
                case "sponsor":
                    animator.SetTrigger("isSponsor");
                    break;

                case "cooking":
                    animator.SetTrigger("isCooking");
                    break;

                case "gaming":
                    animator.SetTrigger("isGaming");
                    break;
                default:
                    animator.SetTrigger("isHappy");
                    break;
            }
            skinPrevious = skinActual;
        }

    }

    public List<LineaSubtitles> GetGuionList()
    {
        return guionList;
    }

}
