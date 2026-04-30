using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.InputSystem;

public class MenuManager : MonoBehaviour
{
    [Header("Tutorial")]
    [SerializeField] GameObject panelTutorial;
    [SerializeField] TextMeshProUGUI inputField;
    [SerializeField] GameObject loginPanel;
    [SerializeField] TextMeshProUGUI loadingText;

    private bool continueGame = false;
    private bool finishLoad = false;


    public void Awake()
    {
        loginPanel.SetActive(false);
        panelTutorial.SetActive(false);
    }

    /*public void CambiarVolumen(float valorSlider)
    {
        UserInfo.Instance.SetVolumenGeneral(Mathf.Log10(valorSlider) * 20);
    }*/

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
        while (!Mouse.current.leftButton.wasPressedThisFrame)
        {
            yield return null;
        }
        loginPanel.SetActive(true);

        finishLoad = true;
    }

    public void ContinueToGame()
    {
        UserInfo.Instance.SetPlayerName(inputField.text);
        continueGame = true;
    }
}
