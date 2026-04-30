using UnityEngine;
using System.Collections;

public class UserInfo : MonoBehaviour
{
    public static UserInfo Instance { get; private set; }
    
    private string playerName;
    private float volumenGeneral = 1f;


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

    public void SetPlayerName(string name)
    {
        playerName = name;
        Debug.Log("El nombre del jugador es: " + playerName);
    }

    public string GetPlayerName()
    {
        return playerName;
    }

    public void SetVolumenGeneral(float valor)
    {
        volumenGeneral = Mathf.Log10(valor) * 20;
    }

    public float GetVolumenGeneral()
    {
        return volumenGeneral;
    }

}
