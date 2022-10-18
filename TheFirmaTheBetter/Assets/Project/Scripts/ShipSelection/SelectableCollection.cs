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

        private string categoryName;

        public List<Selectable> Selectables { get => selectables; }
        public int CurrentSelectedIndex { get => currentSelectedIndex; set => currentSelectedIndex = value; }
        public string CategoryName { get => categoryName; set => categoryName = value; }
    }
}
