using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using TMPro;

public class EndingPlay : MonoBehaviour
{
    private PaqueteSubtitles endingJson;
    private List<LineaSubtitles[]> endingList = new List<LineaSubtitles[]>();

    [SerializeField] private TextMeshProUGUI subtitle;
    [SerializeField] private Animator animator;
    [SerializeField] private float tiempoEntreLineas = 4f;
    [SerializeField] private string typeEnding;

    private float timerGeneral = 0f;
    private int indexLinea = 0;
    private int categoria = 0;

    void Awake()
    {
        TextAsset archivo = Resources.Load<TextAsset>("ending");

        if (archivo != null)
        {
            endingJson = JsonUtility.FromJson<PaqueteSubtitles>(archivo.text);
            if (endingJson != null && endingJson.guionBD != null)
            {
                Debug.Log("Datos de los Endings Cargados");
            }
        }
        else
        {
            Debug.LogError("Ojo: El JSON no se encuentra en la carpeta Resources o el nombre está mal.");
        }

        this.gameObject.SetActive(false);
    }


    void Start()
    {
        ConvertirEnListaSegunCategoria();

        switch(typeEnding)
        {
            case "good":
                animator.SetTrigger("StartGood");
                categoria = 0;
            break;
            case "neutral":
                animator.SetTrigger("StartNeutral");
                categoria = 1;
            break;
            case "bad":
                animator.SetTrigger("StartBad");
                categoria = 2;
            break;
            case "easterEgg":
                animator.SetTrigger("StartEasterEgg");
                categoria = 3;
            break;
        }
    }

    void Update()
    {
        timerGeneral += Time.deltaTime;

        if (timerGeneral >= tiempoEntreLineas)
        {
            ReproducirFinal(indexLinea);
            timerGeneral = 0f; // Reiniciar el temporizador
        }
    }

    private void ReproducirFinal(int index)
    {

        if (index < endingList[categoria].Length)
        {
            subtitle.text = endingList[categoria][index].text;
            indexLinea++;
        }
    }

    private void ConvertirEnListaSegunCategoria()
    {
        LineaSubtitles[] arrayTemporal;
        HashSet<string> ending = new HashSet<string>();

        for (int i = 0; i < endingJson.guionBD.Length; i++)
        {
            ending.Add(endingJson.guionBD[i].skin);
        }

        foreach (string categoria in ending)
        {
            arrayTemporal = endingJson.guionBD.Where(p => p.skin == categoria).ToArray();
            endingList.Add(arrayTemporal);
        }

        /*endingList.Add(endingJson.guionBD.Where(p => p.skin == "C").ToArray());
        endingList.Add(endingJson.guionBD.Where(p => p.skin == "M").ToArray());
        endingList.Add(endingJson.guionBD.Where(p => p.skin == "G").ToArray());*/
    }
}
