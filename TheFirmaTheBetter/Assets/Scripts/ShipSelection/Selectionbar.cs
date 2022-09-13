using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Selectionbar : MonoBehaviour
{
    [SerializeField]
    private PlayerNumber playerNumber;

    private List<Selectable> selectionOptions = new List<Selectable>();

    private int currentSelectedIndex;

    private void Start()
    {
        //foreach (Selectable selectable in transform)
        //{
        //    selectionOptions.Add(selectable);
        //}

        //TODO: Add subscribtion to events
    }

    public void OnNavigate()
    {

    }

    private void SelectNext()
    {
        currentSelectedIndex++;

        if (currentSelectedIndex >= selectionOptions.Count)
        {
            currentSelectedIndex = 0;
        }

        UpdateCurrentSelectionGraphic();
    }

    private void SelectPrevious()
    {
        currentSelectedIndex--;

        if (currentSelectedIndex < 0)
        {
            currentSelectedIndex = selectionOptions.Count - 1;
        }

        UpdateCurrentSelectionGraphic();
    }

    private void UpdateCurrentSelectionGraphic()
    {
        DeselectAllSelectables();
        selectionOptions[currentSelectedIndex].SelectObject();
    }

    private void DeselectAllSelectables()
    {
        foreach(Selectable selectable in selectionOptions)
        {
            selectable.DeSelectObject();
        }
    }
}
