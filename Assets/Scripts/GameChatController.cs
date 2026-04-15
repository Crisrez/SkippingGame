using UnityEngine;
using System.Collections.Generic;
using TMPro;

public class GameChatController : MonoBehaviour
{
    private PaqueteChat chatJson;
    private List<LineaChat> chatList;
    [SerializeField] private TextMeshProUGUI chatText;

    [SerializeField] private float tiempoEntreMensajes; 
    [SerializeField] private bool habilitarChat;

    [Header("Opciones de Estilo - temporales?")]
    [SerializeField] private string colorOpen = "<color=\"red\">";
    [SerializeField] private string colorClose = "</color>";
    [SerializeField] private string negritaOpen = "<b>";
    [SerializeField] private string negritaClose = "</b>";

    private float timerGeneral = 0f;
    private System.Random rng = new System.Random();




    void Start()
    {
        TextAsset archivo = Resources.Load<TextAsset>("chat");

        if (archivo != null)
        {
            chatJson = JsonUtility.FromJson<PaqueteChat>(archivo.text);

            if (chatJson != null && chatJson.chatBD != null)
            {
                Debug.Log("Datos del Chat Cargados");
            }
        }
        else
        {
            Debug.LogError("Ojo: El JSON no se encuentra en la carpeta Resources o el nombre está mal.");
        }

        ConvertirEnLista();
    }

    void Update()
    {
        timerGeneral += Time.deltaTime;

        if (timerGeneral >= tiempoEntreMensajes) // Por ejemplo, cada 0.5 segundos
        {
            //MostrarMensajeChat();
            MostrarMensajeLista();
            timerGeneral = 0f; // Reiniciar el temporizador
        }
    }

    /*private void MostrarMensajeChat()
    {
        string mensaje = "";

        if (chatJson != null && chatJson.chatBD != null && chatJson.chatBD.Length > 0)
        {
            if (indiceMensaje < chatJson.chatBD.Length)
            {
                mensaje = chatJson.chatBD[indiceMensaje].user;
                mensaje += ": " + chatJson.chatBD[indiceMensaje].emotes;
                indiceMensaje++;
                chatText.text += mensaje + "\n";

                if (indiceMensaje >= chatJson.chatBD.Length)
                {
                    indiceMensaje = 0; // Reiniciar el índice para repetir los mensajes
                }
                
            }

        }
    }*/

    private void ConvertirEnLista()
    {
        chatList = new List<LineaChat>(chatJson.chatBD);
    }

    private void MostrarMensajeLista()
    {
        string mensajeCompleto = "";

        if (chatList != null && chatList.Count > 0)
        {
            int indiceAleatorio = rng.Next(chatList.Count);
            LineaChat mensaje = chatList[indiceAleatorio];

            if (!habilitarChat)
            {
                mensajeCompleto = colorOpen + negritaOpen + mensaje.user + ": " + negritaClose + colorClose + mensaje.emotes;
            }
            else
            {
                mensajeCompleto = colorOpen + negritaOpen + mensaje.user + ": " + negritaClose + colorClose + mensaje.text;
            }

            chatText.text += mensajeCompleto + "\n";
            chatList.RemoveAt(indiceAleatorio); // Eliminar el mensaje mostrado para evitar repeticiones
        }
        else
        {
            ConvertirEnLista(); // Volver a llenar la lista si se han mostrado todos los mensajes
        }
    }
}
