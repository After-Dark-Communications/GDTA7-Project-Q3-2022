using Managers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MapSelection
{
    public class MapCollectionManager : Manager
    {
        List<MapData> allMaps = new List<MapData>();

        [SerializeField]
        List<FFAMapData> fFAMaps = new List<FFAMapData>();
        [SerializeField]
        List<KOTHMapData> kOTHMaps = new List<KOTHMapData>();

        private void Awake()
        {
            allMaps.Clear();

            allMaps.AddRange(fFAMaps);
            allMaps.AddRange(kOTHMaps);
        }

        public List<MapData> AllMaps { get { return allMaps; } }
        public List<FFAMapData> FFAMaps { get { return fFAMaps; } }
        public List<KOTHMapData> KOTHMaps { get { return kOTHMaps; } }
    }
}
