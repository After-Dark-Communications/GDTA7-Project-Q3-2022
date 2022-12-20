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
        private const string DisabledBooleanName = "Disabled";
        private const string IsHoveredBooleanName  = "IsHovered";
        private const string ShipIsCompletedBooleanName = "ShipCompleted";
        private Color selectedColor;
        private Color normalColor;

        private List<Animator> buttonAnimators = new List<Animator>();

        private void Awake()
        {
            foreach (Button button in transform.GetComponentsInChildren<Button>())
            {
                buttonAnimators.Add(button.GetComponent<Animator>());
            }
        }

        internal void UpdateButtons(Selectionbar selectionBar)
        {
            Animator animatorSelectedButton = buttonAnimators[selectionBar.CurrentSelectedCollection.CurrentSelectedIndex];
            animatorSelectedButton.SetBool(DisabledBooleanName, true);
        }

        public void ResetButtons()
        {
            foreach (Animator buttonAnimator in buttonAnimators)
            {
                buttonAnimator.SetBool(DisabledBooleanName, false);
                // try to also disable the hover
               // buttonAnimator.SetBool(IsHoveredBooleanName, false);
            }
        }
        public void UpdateButtonAt (int index)
        {
            Animator currentButtonAnimator = buttonAnimators[index];
            currentButtonAnimator.SetBool(DisabledBooleanName, true);
        }
        public void ResetButtonAt(int index)
        {
            Animator currentButtonAnimator = buttonAnimators[index];
            currentButtonAnimator.SetBool(DisabledBooleanName, false);
            //Debug.Log("Reset Button " + index + " FALSE");
        }

        private int GetNextIndex(int currentIndex)
        {
            int toReturn = currentIndex + 1;

            if (toReturn >= buttonAnimators.Count)
                toReturn = 0;

            if (toReturn < 0)
                toReturn = buttonAnimators.Count - 1;

            return toReturn;
        }

        internal void UpdateHoverEffectAt(int index, bool state)
        {
            Animator currentButtonAnimator = buttonAnimators[index];
            currentButtonAnimator.SetBool(IsHoveredBooleanName, state);
        }

        /// <summary>
        /// Called when a Player is ready with their ship selection
        /// Updates all button animators to be in "normal" state and sets the "iscopleted" boolean to be true.
        /// </summary>
        public void DisableAllButtons()
        {
            ///Animator animatorSelectedButton = buttonAnimators[selectionBar.CurrentSelectedCollection];
            foreach (Animator buttonAnimator in buttonAnimators)
            {
                buttonAnimator.SetBool(DisabledBooleanName, false);
                buttonAnimator.SetBool(IsHoveredBooleanName, false);
                buttonAnimator.SetBool(ShipIsCompletedBooleanName, true);
            }

        }
    }
}
