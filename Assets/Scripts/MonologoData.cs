using UnityEngine;


[System.Serializable]
public class LineaMonologo
{
    public Sprite skin;
    public string text;
}

[System.Serializable]
public class PaqueteMonologo
{
    public LineaMonologo[] baseDeDatos;
}