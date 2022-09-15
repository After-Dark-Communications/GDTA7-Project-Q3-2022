using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Parts
{
    [AddComponentMenu("Parts/Very Fast Engine")]
    public class VeryFastEngine : Engine
    {
        private float minimumTimeToFallOut;
        private float maximumTimeToFallOut;
    }
}
