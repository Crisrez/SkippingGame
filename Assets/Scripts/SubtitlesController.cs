using UnityEngine;
using TMPro;
using UnityEngine.UI;


public class SubtitlesController : MonoBehaviour
{
    public TextMeshProUGUI subtitle;
    public string[] guion;

    //public TextMeshProUGUI chat;
    
    public Button botonSiguiente;
    public Button botonAtras;
    
    public int indice = 0;
    public int maxIndice;
    //public int ;


    void Start()
    {
        maxIndice = guion.Length - 1;
        indice = maxIndice;
    }

    void Update()
    {
        subtitle.text = guion[indice];
        //chat.text = "¡Hola! Soy un mensaje de chat.";


    }

    public void AvanzarLínea()
    {
        if (indice < maxIndice)
        {
            indice++; 
            ActualizarTexto();
        }
        else
        {
            Debug.Log("Has llegado al final del texto.");
        }
    }

    public void RetrocederLínea()
    {
        if (indice > 0)
        {
            indice--;
            ActualizarTexto();
        }
        else
        {
            Debug.Log("Estás en el inicio del texto.");
        }
    }

    public void ActualizarTexto()
    {
        subtitle.text = guion[indice];
    }
}
