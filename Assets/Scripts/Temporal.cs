using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.Audio;
using UnityEngine.InputSystem;

public class Temporal : MonoBehaviour
{
    [Header("Tutorial")]
    [SerializeField] GameObject panelTutorial;
    [SerializeField] TextMeshProUGUI inputField;
    [SerializeField] GameObject loginPanel;
    [SerializeField] TextMeshProUGUI loadingText;

    [SerializeField] AudioMixer mixerMenu;
    [SerializeField] AudioMixer mixerGame;

    private string playerName;
    private float volumenGeneral;

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

        loginPanel.SetActive(false);
        panelTutorial.SetActive(false);
    }

    void Start()
    {
        loginPanel.SetActive(false);
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

    public void SetVolumenGeneral()
    {
        mixerMenu.SetFloat("MenuVol", volumenGeneral);
        mixerGame.SetFloat("GameVol", volumenGeneral);

        Debug.Log("El volumen general es: " + volumenGeneral);
    }

    public void CambiarVolumen(float valorSlider)
    {
        // El valor debe estar entre 0.0001 y 1 (usamos logaritmos para el audio)
        volumenGeneral = Mathf.Log10(valorSlider) * 20;
        SetVolumenGeneral();
    }

    public float GetVolumenGeneral()
    {
        return volumenGeneral;
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
        loadingText.text = "<grow>Click to Login...";

        StartCoroutine(WaitLogin());
    }

    private IEnumerator WaitLogin()
    {
        while (!Input.GetMouseButtonDown(0))
        {
            yield return null;
        }
        loginPanel.SetActive(true);

        finishLoad = true;
    }

    public void ContinueToGame()
    {
        SetPlayerName();
        continueGame = true;
    }
}
