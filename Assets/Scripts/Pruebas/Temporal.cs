using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections;

public class Temporal : MonoBehaviour
{
    [Header("Audio")]
    [SerializeField] AudioSource audio;
    [SerializeField] Slider sliderVolumen;

    [Header("Tutorial")]
    [SerializeField] GameObject panelTutorial;
    [SerializeField] TextMeshProUGUI inputField;
    [SerializeField] GameObject userField;
    [SerializeField] GameObject submitButton;
    [SerializeField] TextMeshProUGUI loadingText;

    private string playerName;
    
    private bool continueGame = false;
    private bool finishLoad = false;

    public static Temporal Instance { get; private set; }


    public void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        submitButton.SetActive(false);
        userField.SetActive(false);
        panelTutorial.SetActive(false);
    }

    public void SetPlayerName()
    {
        playerName = inputField.text;
        Debug.Log("El nombre del jugador es: " + playerName);
    }

    public string GetPlayerName()
    {
        return playerName;
    }

    public void CambioScene()
    {
        StartCoroutine(CargaAsincrona());
    }

    private IEnumerator CargaAsincrona()
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("Game");
        asyncLoad.allowSceneActivation = false;

        panelTutorial.SetActive(true);

        while (!finishLoad)
        {
            yield return null;
        }

        loadingText.text = "<grow>Login to continue...";

        while (!continueGame)
        {
            yield return null;
        }
            asyncLoad.allowSceneActivation = true;

    }

    public void WriterIsFinished()
    {
        userField.SetActive(true);
        submitButton.SetActive(true);

        finishLoad = true;
    }

    public void ContinueToGame()
    {
        SetPlayerName();
        continueGame = true;
    }

    /*private void Start()
    {
        audio.volume = sliderVolumen.value;
        audio.Play();
        StartCoroutine(Volumen());
    }

    private IEnumerator Volumen()
    {
        while (sliderVolumen.value > 0)
        {
            sliderVolumen.value = Mathf.Max(0, sliderVolumen.value - 0.01f);
            audio.volume = sliderVolumen.value;
            yield return new WaitForSeconds(0.1f); // Espera un frame antes de continuar el bucle
        }
    }*/






}
