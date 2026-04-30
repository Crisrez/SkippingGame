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


    //void Start()
    //{
    //    goodEndingPanel.SetActive(false);
    //    badEndingPanel.SetActive(false);
    //    neutralEndingPanel.SetActive(false);
    //    easterEggEndingPanel.SetActive(false);

    //}

    void Update()
    {
        
    }

    public void ShowGoodEnding(int correctAnswers, bool easterEgg)
    {
        if (easterEgg)
        {
            PlayEnding(easterEggEndingPanel);
            return;
        }
        else 
        {
            switch (correctAnswers)
            {
                case 0:
                    PlayEnding(badEndingPanel);
                    break;
                case 1:
                    PlayEnding(badEndingPanel);
                    break;
                case 2:
                    PlayEnding(neutralEndingPanel);
                    break;
                case 3:
                    PlayEnding(goodEndingPanel);
                    break;
            }
        }
    }

    private void PlayEnding(GameObject endingPanel)
    {
        gamePanel.SetActive(false);
        endingPanel.SetActive(true);
    }
}
