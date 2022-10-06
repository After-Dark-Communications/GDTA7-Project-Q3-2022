using EventSystem;
using ShipParts.Ship;
using ShipSelection.ShipBuilders;
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

        resultsManager = ResultsManager.Instance;
        DisplayResults();
    }

    private void DisplayResults()
    {
        foreach (EndScreenPanel panel in panels)
        {
            panel.gameObject.SetActive(false);
        }

        PlayerStatistics[] results = resultsManager.Results;
        for (int i = 0; i < results.Length; i++)
        {
            previewCams[i].PlaceShipPreview(results[i].gameObject);
            panels[i].gameObject.SetActive(true);
            panels[i].SetPlayerStats(results[i]);
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
