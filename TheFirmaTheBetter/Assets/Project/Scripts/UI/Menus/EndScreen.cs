using Managers;
using ShipSelection;
using UnityEngine;
using EventSystem;


namespace UI.Menus
{
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
            PlayerResult[] results = ResultsManager.Instance.Results;

            if (results == null || results.Length == 0)
            { return; }

            foreach (EndScreenPanel panel in panels)
            {
                panel.gameObject.SetActive(false);
            }

            for (int i = 0; i < results.Length; i++)
            {
                previewCams[i].PlaceShipPreview(results[i].gameObject);
                panels[i].gameObject.SetActive(true);
                panels[i].SetPlayerStats(results[i]);
            }
        }
        public void Rematch()
        {
            Channels.OnReturnToTitleScreen?.Invoke();
            SceneSwitchManager.SwitchToFirstScene();
        }
    }
}