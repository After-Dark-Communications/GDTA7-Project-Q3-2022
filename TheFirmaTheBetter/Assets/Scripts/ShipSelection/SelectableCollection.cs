using Assets.Scripts.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.ShipSelection
{
    public class SelectableCollection : MonoBehaviour
    {
        [SerializeField]
        private List<Selectable> selectables;

        private int currentSelectedSelectableIndex = 0;

        public void SelectNextSelectable()
        {
            currentSelectedSelectableIndex = ListLooper.SelectNext(selectables, currentSelectedSelectableIndex);
        }
        public void SelectPreviousSelectable()
        {
            currentSelectedSelectableIndex = ListLooper.SelectPrevious(selectables, currentSelectedSelectableIndex);
        }

        public Selectable CurrentSelectedOption => selectables[currentSelectedSelectableIndex];
    }
}
