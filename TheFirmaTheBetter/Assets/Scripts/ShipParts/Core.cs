using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Parts
{
    [AddComponentMenu("Parts/Core")]
    public class Core : Part
    {
        [SerializeField]
        private CoreData coreData;

        public override string PartName => "Core";

        public override void Setup() { }
    }
}