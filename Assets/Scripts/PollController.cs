using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class PollController : MonoBehaviour
{
    private PaquetePoll pollJson;
    private List<LineaPoll[]> pollList = new List<LineaPoll[]>();

    [Header("UI Elements")]
    [SerializeField] private GameObject pollPanel;
    [SerializeField] private TextMeshProUGUI questionText;
    [SerializeField] private Button aButton;
    [SerializeField] private Button bButton;
    [SerializeField] private Button cButton;
    [SerializeField] private Button dButton;
    [SerializeField] private TextMeshProUGUI aText;
    [SerializeField] private TextMeshProUGUI bText;
    [SerializeField] private TextMeshProUGUI cText;
    [SerializeField] private TextMeshProUGUI dText;
    [SerializeField] private Slider sliderTimer;

    [Header("Times Settings")]
    [SerializeField] private float startGame;
    [SerializeField] private float pollDuration;
    [SerializeField] private float timeColdown;

    [Header("Ending")]
    [SerializeField] private EndingController endingController;
    private int correctAnswers = 0;
    private int totalAnswers = 0;

    private int indiceQuestion = 0;
    private System.Random rng = new System.Random();
    private int randomIndex;



    void Start()
    {
        TextAsset archivo = Resources.Load<TextAsset>("poll");

        if (archivo != null)
        {
            pollJson = JsonUtility.FromJson<PaquetePoll>(archivo.text);

            if (pollJson != null && pollJson.pollBD != null)
            {
                Debug.Log("Datos de las Polls Cargados");
            }
        }
        else
        {
            Debug.LogError("Ojo: El JSON no se encuentra en la carpeta Resources o el nombre está mal.");
        }

        pollPanel.SetActive(false);

        ConvertirEnListaSegunCategoria();

        //StartCoroutine(StreamStart());
        //StartCoroutine(CooldownActivated());
    }

    void Update()
    {
    }

    private IEnumerator StreamStart()
    {
        yield return new WaitForSeconds(startGame);
        PollActivated();

    }

    public void PollActivated()
    {
        aButton.interactable = true;
        bButton.interactable = true;
        cButton.interactable = true;
        dButton.interactable = true;

        //questionText.text = pollJson.pollBD[indiceQuestion].quest;
        //aText.text = pollJson.pollBD[indiceQuestion].opcA;
        //bText.text = pollJson.pollBD[indiceQuestion].opcB;
        //cText.text = pollJson.pollBD[indiceQuestion].opcC;
        //dText.text = pollJson.pollBD[indiceQuestion].opcD;

        randomIndex = rng.Next(pollList[indiceQuestion].Length);

        questionText.text = pollList[indiceQuestion][randomIndex].quest;

        List<string> options = new List<string>
        {
            pollList[indiceQuestion][randomIndex].opcA,
            pollList[indiceQuestion][randomIndex].opcB,
            pollList[indiceQuestion][randomIndex].opcC,
            pollList[indiceQuestion][randomIndex].opcD
        };

        // MEJORAR LUEGO DE LA ENTREGA 

        aText.text = options[rng.Next(options.Count)];
        options.Remove(aText.text);
        bText.text = options[rng.Next(options.Count)];
        options.Remove(bText.text);
        cText.text = options[rng.Next(options.Count)];
        options.Remove(cText.text);
        dText.text = options[rng.Next(options.Count)];
        
        pollPanel.SetActive(true);

        StartCoroutine(PollTimer());
        
    }

    private IEnumerator PollTimer()
    {
        float timer = pollDuration;
        sliderTimer.maxValue = pollDuration;
        sliderTimer.value = timer;
    
        while (timer > 0)
        {
            timer -= Time.deltaTime;
            sliderTimer.value = timer;
            yield return null;
        }
        
        StartCoroutine(EndPoll());
    }

    private IEnumerator EndPoll()
    {
        pollPanel.SetActive(false);

        if (indiceQuestion < pollList.Count - 1)
        {
            yield return new WaitForSeconds(timeColdown);
            indiceQuestion++;
            PollActivated();
        }
        else
        {
            Debug.Log("¡Fin de las preguntas! Total Correctas: " + correctAnswers);
            StreamEnd();
        }

    }

    private void StreamEnd()
    {
        Debug.Log("¡Gracias por participar en la encuesta!");
        
        /*if (totalAnswers == 0)
        {
            endingController.ShowGoodEnding(correctAnswers, true);
        }*/
        /*else
        {*/
            endingController.ShowGoodEnding(correctAnswers, false);
        //}
        // Aquí podrías mostrar un resumen de resultados o reiniciar el juego, etc.
    }

    public void clickedButton(string idButton)
    {
        totalAnswers++;

        switch (idButton)
        {
            case "A":
                OnAnswerSelected(aText.text);
                break;
            case "B":
                OnAnswerSelected(bText.text);
                break;
            case "C":
                OnAnswerSelected(cText.text);
                break;
            case "D":
                OnAnswerSelected(dText.text);
                break;

        }
    }

    private void OnAnswerSelected(string selectedOption)
    {
        string correctOption = pollList[indiceQuestion][randomIndex].validAnswer;

        if (selectedOption == correctOption)
        {
            correctAnswers++;
            Debug.Log("Respuesta Correcta! Total Correctas: " + correctAnswers);
        }
        else
        {
            Debug.Log("Respuesta Incorrecta. La respuesta correcta era: " + correctOption);
        }

        aButton.interactable = false;
        bButton.interactable = false;
        cButton.interactable = false;
        dButton.interactable = false;
    }

    public int GetCorrectAnswers()
    {
        return correctAnswers;
    }

    private void ConvertirEnListaSegunCategoria()
    {
        LineaPoll[] arrayTemporal;
        HashSet<string> categorias = new HashSet<string>();

        for (int i = 0; i < pollJson.pollBD.Length; i++)
        {
            categorias.Add(pollJson.pollBD[i].id);
        }

        foreach (string categoria in categorias)
        {
            arrayTemporal = pollJson.pollBD.Where(p => p.id == categoria).ToArray();
            pollList.Add(arrayTemporal);
        }

        /*poolList.Add(pollJson.pollBD.Where(p => p.id == "C").ToArray());
        poolList.Add(pollJson.pollBD.Where(p => p.id == "M").ToArray());
        poolList.Add(pollJson.pollBD.Where(p => p.id == "G").ToArray());*/
    }
}
