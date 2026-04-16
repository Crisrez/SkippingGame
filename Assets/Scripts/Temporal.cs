using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections;

public class Temporal : MonoBehaviour
{
    [SerializeField] private string playerName;
    [SerializeField] TextMeshProUGUI inputField;
    [SerializeField] Button submitButton;

    [SerializeField] Slider sliderVolumen;
    [SerializeField] AudioSource audio;

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

    public void SetPlayerName()
    {
        playerName = inputField.text;
        Debug.Log("Player Name set to: " + playerName);
        CambioScene();
    }

    public string GetPlayerName()
    {
        return playerName;
    }

    private void CambioScene()
    {
        // Aquí puedes agregar la lógica para cambiar de escena, por ejemplo:
        SceneManager.LoadScene("Game");
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
