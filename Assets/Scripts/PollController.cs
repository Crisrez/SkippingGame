using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.InputSystem;

public class PollController : MonoBehaviour
{
    [SerializeField] private PaquetePoll pollJson;

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
    [SerializeField] private float pollDuration;
    [SerializeField] private float timeColdown;
    [SerializeField] private float startGame;

    public float timerGeneral;
    public TextMeshProUGUI timerGeneralText;

    [SerializeField] private int correctAnswers = 0;

    public int indiceQuestion = 0;



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

        StartCoroutine(StreamStart());
        //StartCoroutine(CooldownActivated());
    }

    void Update()
    {
        timerGeneral += Time.deltaTime;

        timerGeneralText.text = "Tiempo: " + Mathf.FloorToInt(timerGeneral).ToString() + "s";
    }

    private IEnumerator StreamStart()
    {
        yield return new WaitForSeconds(startGame);
        PollActivated();

    }

    private void PollActivated()
    {
        aButton.interactable = true;
        bButton.interactable = true;
        cButton.interactable = true;
        dButton.interactable = true;

        questionText.text = pollJson.pollBD[indiceQuestion].quest;
        aText.text = pollJson.pollBD[indiceQuestion].opcA;
        bText.text = pollJson.pollBD[indiceQuestion].opcB;
        cText.text = pollJson.pollBD[indiceQuestion].opcC;
        dText.text = pollJson.pollBD[indiceQuestion].opcD;

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
        
        yield return new WaitForSeconds(timeColdown);

        if (indiceQuestion < pollJson.pollBD.Length - 1)
        {
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
        // Aquí podrías mostrar un resumen de resultados o reiniciar el juego, etc.
    }




    public void clickedButton(string idButton)
    {
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
        string correctOption = pollJson.pollBD[indiceQuestion].validAnswer;

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


}
