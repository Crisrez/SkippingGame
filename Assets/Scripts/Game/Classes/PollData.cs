using UnityEngine;

public class PollData
{

}

[System.Serializable]
public class LineaPoll
{
    public string id;
    public string quest;
    public string opcA;
    public string opcB;
    public string opcC;
    public string opcD;
    public string validAnswer;
}

[System.Serializable]
public class PaquetePoll
{
    public LineaPoll[] pollBD;
}
