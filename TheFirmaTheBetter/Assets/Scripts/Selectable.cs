using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Selectable : MonoBehaviour
{
    [SerializeField]
    private GameObject selectedObject;
    [SerializeField]
    private GameObject notSelectedObject;

    public void SelectObject()
    {
        selectedObject.SetActive(true);
        notSelectedObject.SetActive(false);
    }
    public void DeSelectObject()
    {
        notSelectedObject.SetActive(true);
        selectedObject.SetActive(false);
    }
}
