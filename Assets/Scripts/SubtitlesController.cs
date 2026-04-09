using UnityEngine;
using TMPro;
using UnityEngine.UI;


public class SubtitlesController : MonoBehaviour
{
    public TextMeshProUGUI subtitle;
    public PaqueteMonologo guionJson;

    public Button botonSiguiente;
    public Button botonAtras;
    
    public int indice = 0;
    public int maxIndice;


    void Start()
    {
        TextAsset archivo = Resources.Load<TextAsset>("guion");

        if (archivo != null)
        {
            // "Traducción" del JSON a objetos de C#
            guionJson = JsonUtility.FromJson<PaqueteMonologo>(archivo.text);
            
            if (guionJson != null && guionJson.baseDeDatos != null)
            {
                maxIndice = guionJson.baseDeDatos.Length - 1;
                indice = maxIndice;
                ActualizarTexto();
            }
        }
        else
        {
            Debug.LogError("Ojo: No pusiste el JSON en la carpeta Resources o el nombre está mal.");
        }

    }

    void Update()
    {


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
        subtitle.text = guionJson.baseDeDatos[indice].text;
    }
}
