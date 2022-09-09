using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Selectionbar : MonoBehaviour
{
    //[SerializeField]
    //private PlayerNumber playerNumber;

    private List<Selectable> selectionOptions = new List<Selectable>();

    private int currentSelectedIndex;

    private void Start()
    {
        foreach (Selectable selectable in transform)
        {
            selectionOptions.Add(selectable);
        }

        //TODO: Add subscribtion to event
    }

    //public void SelectNext(InputEventArgs args)
    //{
    //    if (IsMyInput(inputTarget))
    //        return;

    //    currentSelectedIndex++;
       
    //    if (currentSelectedIndex >= selectionOptions.Count)
    //    {
    //        currentSelectedIndex = 0;
    //    }

    //    UpdateCurrentSelectionGraphic();
    //}

    //public void SelectPrevious(InputEventArgs args)
    //{
    //    if (IsMyInput(inputTarget))
    //        return;

    //    currentSelectedIndex--;

    //    if (currentSelectedIndex < 0)
    //    {
    //        currentSelectedIndex = selectionOptions.Count - 1;
    //    }

    //    UpdateCurrentSelectionGraphic();
    //}

    //private bool IsMyInput(PlayerNumber inputTarget)
    //{
    //    if (playerNumber != inputTarget)
    //        return false;

    //    return true;
    //}

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
