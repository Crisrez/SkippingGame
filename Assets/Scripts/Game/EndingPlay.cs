using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using TMPro;

public class EndingPlay : MonoBehaviour
{
    [SerializeField] private PaqueteSubtitles endingJson;
    [SerializeField] private List<LineaSubtitles[]> endingList = new List<LineaSubtitles[]>();

    [SerializeField] private TextMeshProUGUI subtitle;
    [SerializeField] private Animator animator;
    [SerializeField] private float tiempoEntreLineas = 4f;
    [SerializeField] private string typeEnding;

    [SerializeField] private GameObject offlinePanel;

    public string playerName;

    private float timerGeneral = 0f;
    private int indexLinea = 0;
    private int categoria = 0;

    void Awake()
    {
        TextAsset archivo = Resources.Load<TextAsset>("ending");

        if (archivo != null)
        {
            Debug.Log("Archivo JSON de Endings Encontrado");
            endingJson = JsonUtility.FromJson<PaqueteSubtitles>(archivo.text);
            Debug.Log("Archivo JSON de Endings Cargado");
            if (endingJson != null && endingJson.guionBD != null)
            {
                Debug.Log("Datos de los Endings Cargados");
            }
            this.gameObject.SetActive(false);
        }
        else
        {
            Debug.LogError("Ojo: El JSON no se encuentra en la carpeta Resources o el nombre está mal.");
        }

    }


    void Start()
    {
        ConvertirEnListaSegunCategoria();

        playerName = UserInfo.Instance.GetPlayerName();

        AudioManager.Instance.ChangeVolume(UserInfo.Instance.GetVolumenGeneral());

        switch (typeEnding)
        {
            case "good":
                animator.SetTrigger("startGood");
                categoria = 0;
            break;
            case "neutral":
                animator.SetTrigger("startNeutral");
                categoria = 1;
            break;
            case "bad":
                animator.SetTrigger("startBad");
                categoria = 2;
            break;
            case "easterEgg":
                animator.SetTrigger("startEasterEgg");
                categoria = 3;
            break;
        }

        ReproducirFinal(indexLinea);
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
            if (endingList[categoria][index].text.Contains("%PLAYER_NAME%"))
            {
                endingList[categoria][index].text = endingList[categoria][index].text.Replace("%PLAYER_NAME%", playerName);
            }

            subtitle.text = endingList[categoria][index].text;
            indexLinea++;
        }
        else
        {
            Debug.Log("Final del Ending");
            offlinePanel.SetActive(true);
            // Aquí puedes agregar lógica para finalizar el ending, como cargar una nueva escena o mostrar un mensaje final.
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
