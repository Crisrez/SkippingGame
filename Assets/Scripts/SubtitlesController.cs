using UnityEngine;
using TMPro;
using UnityEngine.UI;


public class SubtitlesController : MonoBehaviour
{
    public TextMeshProUGUI subtitle;
    public PaqueteSubtitles guionJson;

    public Button botonSiguiente;
    public Button botonAtras;
    public Animator animator;
    
    public int indice = 0;
    public int maxIndice;

    public string skinActual;
    public string skinPrevious;


    void Start()
    {
        TextAsset archivo = Resources.Load<TextAsset>("guion");

        if (archivo != null)
        {
            guionJson = JsonUtility.FromJson<PaqueteSubtitles>(archivo.text);
            
            if (guionJson != null && guionJson.guionBD != null)
            {
                maxIndice = guionJson.guionBD.Length - 1;
                indice = maxIndice;
                ActualizarTexto();
            }
        }
        else
        {
            Debug.LogError("Ojo: El JSON no se encuentra en la carpeta Resources o el nombre está mal.");
        }

    }

    void Update()
    {


    }

    public void AvanzarLínea()
    {
        /*if (indice < maxIndice)
        {*/
            indice++;
            botonAtras.gameObject.SetActive(true);

            if (indice >= maxIndice)
            {
                indice = maxIndice; 
                botonSiguiente.gameObject.SetActive(false); 
            }

            ActualizarTexto();
            ActualizarSprite();
        /*}
        else
        {
            Debug.Log("Has llegado al final del texto.");
        }*/
    }

    public void RetrocederLínea()
    {
        /*if (indice > 0)
        {*/
            indice--;
            botonSiguiente.gameObject.SetActive(true);

            if (indice <= 0)
            {
                indice = 0; 
                botonAtras.gameObject.SetActive(false); 
            }

            ActualizarTexto();
            ActualizarSprite();
        /*}
        else
        {
            Debug.Log("Estás en el inicio del texto.");
        }*/
    }

    public void ActualizarTexto()
    {
        subtitle.text = guionJson.guionBD[indice].text;
    }

    public int GetIndice()
    {
        return indice;
    }

    public void ActualizarSprite()
    {
        skinActual = guionJson.guionBD[indice].skin;

        if (skinActual != skinPrevious)
        {
            switch (skinActual)
            {
                case "happy":
                    animator.SetTrigger("isHappy");
                    break;

                case "sponsor":
                    animator.SetTrigger("isSponsor");
                    break;

                case "cooking":
                    animator.SetTrigger("isCooking");
                    break;

                case "gaming":
                    animator.SetTrigger("isGaming");
                    break;
            }
            skinPrevious = skinActual;
        }

    }

}
