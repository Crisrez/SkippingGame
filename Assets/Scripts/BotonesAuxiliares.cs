using UnityEngine;
using UnityEngine.SceneManagement;

public class BotonesAuxiliares : MonoBehaviour
{
    public void BackMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void ExitGame()
    {
        Application.Quit();
    }

}