using UnityEngine;


public class MonologoData
{
    
}

[System.Serializable]
public class LineaMonologo
{
    public string texto;
    public Sprite traje;
}

[System.Serializable]
public class PaqueteMonologo
{
    public LineaMonologo[] jason;
}