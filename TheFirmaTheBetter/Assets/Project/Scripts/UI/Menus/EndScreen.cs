using UnityEngine;


public class EndScreen : MenuPanel
{
    [SerializeField]
    private ShipPreviewCam[] previewCams;
    [SerializeField]
    private EndScreenPanel[] panels;

    private void OnEnable()
    {
        OpenPanel();
        DisplayResults();
    }

    private void DisplayResults()
    {
        foreach (EndScreenPanel panel in panels)
        {
            panel.gameObject.SetActive(false);
        }

        PlayerStatistics[] results = ResultsManager.Instance.Results;
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
