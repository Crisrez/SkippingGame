using UnityEngine;

public class EndingController : MonoBehaviour
{
    [Header("Ending Panels")]
    [SerializeField] private GameObject goodEndingPanel;
    [SerializeField] private GameObject badEndingPanel;
    [SerializeField] private GameObject neutralEndingPanel;
    [SerializeField] private GameObject easterEggEndingPanel;
    
    [Header("General Panels")]
    [SerializeField] private GameObject gamePanel;


    void Start()
    {
        goodEndingPanel.SetActive(false);
        badEndingPanel.SetActive(false);
        neutralEndingPanel.SetActive(false);
        easterEggEndingPanel.SetActive(false);

    }

    void Update()
    {
        
    }

    public void ShowGoodEnding(int correctAnswers, bool easterEgg)
    {
        gamePanel.SetActive(false);

        if (easterEgg)
        {
            easterEggEndingPanel.SetActive(true);
            return;
        }
        
        switch (correctAnswers){
            case 0:
                badEndingPanel.SetActive(true);
            break;
            case 1:
                badEndingPanel.SetActive(true);
            break;
            case 2:
                neutralEndingPanel.SetActive(true);
            break;
            case 3:
                goodEndingPanel.SetActive(true);
            break;
        }

    }
}
