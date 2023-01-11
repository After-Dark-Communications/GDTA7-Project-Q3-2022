using UnityEngine;

namespace UI.Menus
{
    public abstract class Menu : MonoBehaviour
    {
        [Tooltip("The default panel should be the first in the list")]
        [SerializeField]
        private MenuPanel[] menuPanels;

        protected MenuPanel currentPanel;

        private void OnEnable()
        {
            CloseAllPanles();
            OpenDefaultPanel();
        }

        public void OpenMenuPanel(MenuPanel menuPanel)
        {
            if (currentPanel != null) currentPanel.ClosePanel();
            menuPanel.OpenPanel();
            currentPanel = menuPanel;
        }

        public void OpenDefaultPanel()
        {
            OpenMenuPanel(menuPanels[0]);
        }

        private void CloseAllPanles()
        {
            foreach (MenuPanel menuPanel in menuPanels)
            {
                menuPanel.ClosePanel();
            }
        }
    }
}