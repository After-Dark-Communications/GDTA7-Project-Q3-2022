using ShipSelection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace ShipSelection
{
    public class ButtonSelectionManager : MonoBehaviour
    {
        private Color selectedColor;
        private Color normalColor;

        private List<Button> buttons = new List<Button>();

        private void Awake()
        {
            foreach (Button button in transform.GetComponentsInChildren<Button>())
            {
                buttons.Add(button);
            }

            if (buttons.Count <= 0)
                return;

            selectedColor = buttons[0].colors.disabledColor;
            normalColor = buttons[0].colors.normalColor;
        }

        internal void UpdateButtons(Selectionbar selectionBar)
        {
            foreach (Button button in buttons)
            {
                ColorBlock normalBlock = button.colors;
                normalBlock.normalColor = normalColor;
                button.colors = normalBlock;
            }

            int nextIndex = GetNextIndex(selectionBar.CurrentSelectedCollection.CurrentSelectedIndex);

            //buttons[nextIndex].Select();

            Button currentSelectedButton = buttons[selectionBar.CurrentSelectedCollection.CurrentSelectedIndex];
            ColorBlock block = currentSelectedButton.colors;
            block.normalColor = selectedColor;
            currentSelectedButton.colors = block;
        }

        private int GetNextIndex(int currentIndex)
        {
            int toReturn = currentIndex + 1;

            if (toReturn >= buttons.Count)
                toReturn = 0;

            if (toReturn < 0)
                toReturn = buttons.Count - 1;

            return toReturn;
        }
    }
}
