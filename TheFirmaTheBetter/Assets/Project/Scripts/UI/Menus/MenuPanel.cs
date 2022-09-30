using UnityEngine;
using UnityEngine.EventSystems;

public class MenuPanel : MonoBehaviour
{
    [SerializeField]
    private GameObject firstSelected;

    public void OpenPanel()
    {
        gameObject.SetActive(true);
        UnityEngine.EventSystems.EventSystem.current.SetSelectedGameObject(firstSelected);
    }

    public void ClosePanel()
    {
        UnityEngine.EventSystems.EventSystem.current.SetSelectedGameObject(null);
        gameObject.SetActive(false);
    }
}
