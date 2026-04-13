using UnityEngine;

public class ChatData
{
    
}

[System.Serializable]
public class LineaChat
{
    public string user;
    public string emotes;
    public string text;
}

[System.Serializable]
public class PaqueteChat
{
    public LineaChat[] chatBD;
}