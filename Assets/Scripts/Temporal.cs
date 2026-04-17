using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections;

public class Temporal : MonoBehaviour
{
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
}
