using ShipSelection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Project.Scripts.ShipSelection
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
            Button currentSelectedButton = buttons[selectionBar.CurrentSelectedCollection.CurrentSelectedIndex];
            Animator animatorSelectedButton = currentSelectedButton.GetComponent<Animator>();
            animatorSelectedButton.SetBool("Disabled", true);
        }

        public void ResetButtons()
        {
            foreach (Button button in buttons)
            {
                Animator animator = button.GetComponent<Animator>();
                animator.SetBool("Disabled", false);
            }
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
