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

    [SerializeField] private float timerGeneral;
    public TextMeshProUGUI timerGeneralText;

    [SerializeField] private int correctAnswers = 0;

    public int indiceQuestion = 0;
    //System.Random rng = new System.Random();






    void Start()
    {
        TextAsset archivo = Resources.Load<TextAsset>("poll");

        if (archivo != null)
        {
            // "Traducción" del JSON a objetos de C#
            pollJson = JsonUtility.FromJson<PaquetePoll>(archivo.text);

            if (pollJson != null && pollJson.pollBD != null)
            {
                Debug.Log("Datos de las Polls Cargados");
            }
        }
        else
        {
            Debug.LogError("Ojo: No pusiste el JSON en la carpeta Resources o el nombre está mal.");
        }

        pollPanel.SetActive(false);

        StartCoroutine(StreamStart());
        //StartCoroutine(CooldownActivated());
    }

    void Update()
    {
        /*if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            indiceQuestion = rng.Next(0, pollJson.pollBD.Length);
            questionText.text = pollJson.pollBD[indiceQuestion].quest;
            aText.text = pollJson.pollBD[indiceQuestion].opcA;
            bText.text = pollJson.pollBD[indiceQuestion].opcB;
            cText.text = pollJson.pollBD[indiceQuestion].opcC;
            dText.text = pollJson.pollBD[indiceQuestion].opcD;
        }*/


        timerGeneral += Time.deltaTime;

        timerGeneralText.text = "Tiempo: " + Mathf.FloorToInt(timerGeneral).ToString() + "s";
    }

    private IEnumerator StreamStart()
    {
        yield return new WaitForSeconds(startGame);
        StartCoroutine(PollActivated());

    }

    private IEnumerator PollActivated()
    {
        questionText.text = pollJson.pollBD[indiceQuestion].quest;
        aText.text = pollJson.pollBD[indiceQuestion].opcA;
        bText.text = pollJson.pollBD[indiceQuestion].opcB;
        cText.text = pollJson.pollBD[indiceQuestion].opcC;
        dText.text = pollJson.pollBD[indiceQuestion].opcD;

        pollPanel.SetActive(true);

        StartCoroutine(PollTimer());
        yield return new WaitForSeconds(timeColdown);

        if (indiceQuestion < pollJson.pollBD.Length - 1)
        {
            indiceQuestion++;
            StartCoroutine(PollActivated());
        }
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
        
        EndPoll();
    }

    private void EndPoll()
    {
        pollPanel.SetActive(false);
        // Aquí puedes agregar lógica para evaluar las respuestas y actualizar el contador de respuestas correctas
    }

}
