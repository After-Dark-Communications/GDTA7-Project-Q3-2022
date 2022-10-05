using ShipParts.Ship;
using UnityEngine;


public class EndScreen : MenuPanel
{
    [SerializeField]
    private ShipPreviewCam[] previewCams;
    [SerializeField]
    private EndScreenPanel[] panels;

    private ResultsManager resultsManager;

    private void OnEnable()
    {
        OpenPanel();
        foreach (EndScreenPanel panel in panels)
        {
            panel.gameObject.SetActive(false);
        }

        resultsManager = ResultsManager.Instance;
        DisplayResults();
    }

    private void DisplayResults()
    {
        ShipBuilder[] results = resultsManager.Results;
        for (int i = 0; i < results.Length; i++)
        {
            previewCams[i].PlaceShipPreview(results[i].gameObject);
            panels[i].gameObject.SetActive(true);
        }
    }
    public void Rematch()
    {
        SceneSwitchManager.LoadFirstScene();
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
