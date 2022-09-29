using UnityEngine;

public class MenuNavigator : MonoBehaviour
{
    [SerializeField]
    private MenuPanel startingPanel;

    protected MenuPanel currentPanel;

    private void OnEnable()
    {
        OpenMenuPanel(startingPanel);
    }

    public void OpenMenuPanel(MenuPanel menuPanel)
    {
        if (currentPanel != null) currentPanel.ClosePanel();
        menuPanel.OpenPanel();
        currentPanel = menuPanel;
    }
}
