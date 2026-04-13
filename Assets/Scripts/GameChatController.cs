using UnityEngine;
using TMPro;

public class GameChatController : MonoBehaviour
{
    [SerializeField] private PaqueteChat chatJson;

    [SerializeField] private TextMeshProUGUI chatText;

    private float timerGeneral;

    private int indiceMensaje = 0;

    [SerializeField] private float tiempoEntreMensajes; 

    /*public int alineacionMensajes = 0;
    public int i = 0;*/


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
    }

    void Update()
    {
        timerGeneral += Time.deltaTime;

        if (timerGeneral >= tiempoEntreMensajes) // Por ejemplo, cada 0.5 segundos
        {
            MostrarMensajeChat();
            timerGeneral = 0f; // Reiniciar el temporizador
        }
    }

    private void MostrarMensajeChat()
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

                //i++;            

                if (indiceMensaje >= chatJson.chatBD.Length)
                {
                    indiceMensaje = 0; // Reiniciar el índice para repetir los mensajes
                }
                /*if (i == alineacionMensajes)
                {
                    chatText.alignment = TextAlignmentOptions.BottomLeft;
                }*/
            }

        }
    }
}
