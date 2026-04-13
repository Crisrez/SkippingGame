using UnityEngine;


[System.Serializable]
public class LineaSubtitles
{
    public string skin;
    public string text;
}

[System.Serializable]
public class PaqueteSubtitles
{
    public LineaSubtitles[] guionBD;
}