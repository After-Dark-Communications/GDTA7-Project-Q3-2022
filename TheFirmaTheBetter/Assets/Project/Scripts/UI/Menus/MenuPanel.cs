using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem.UI;

public class MenuPanel : MonoBehaviour
{
    [SerializeField]
    private GameObject firstSelected;

    public void OpenPanel()
    {
        gameObject.SetActive(true);
        EventSystem.current.SetSelectedGameObject(firstSelected);
    }

    public void ClosePanel()
    {
        EventSystem.current.SetSelectedGameObject(null);
        gameObject.SetActive(false);
    }
}
