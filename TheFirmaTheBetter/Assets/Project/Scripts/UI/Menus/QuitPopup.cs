namespace UI.Menus
{
    public class QuitPopup : MenuPanel
    {
        public void QuitGame()
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.ExitPlaymode();
#else
            Application.Quit();
#endif
        }
    }
}
