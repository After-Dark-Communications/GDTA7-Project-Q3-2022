using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShipParts
{
    public class DataModifier
    {
        private float _modifierValue = 0f;

        public float ModifierValue { get => _modifierValue; set => _modifierValue = value; }
    }
}
