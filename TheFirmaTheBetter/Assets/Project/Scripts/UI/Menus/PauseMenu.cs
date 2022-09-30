using UnityEngine;

public class PauseMenu : MenuNavigator
{
    public void PauseUnpause()
    {
        gameObject.SetActive(!gameObject.activeInHierarchy);
    }
}
