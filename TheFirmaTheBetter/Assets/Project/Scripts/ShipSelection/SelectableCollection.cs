using  Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace  ShipSelection
{
    public class SelectableCollection : MonoBehaviour
    {
        private List<Selectable> selectables = new List<Selectable>();

        private int currentSelectedIndex = 0;

        public List<Selectable> Selectables { get => selectables; }
        public int CurrentSelectedIndex { get => currentSelectedIndex; set => currentSelectedIndex = value; }
    }
}
