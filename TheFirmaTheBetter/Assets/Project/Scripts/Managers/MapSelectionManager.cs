using Managers;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using MapSelection;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

namespace Managers
{
    public class MapSelectionManager : MonoBehaviour
    {
        [SerializeField]
        private MapCollectionManager mapCollectionManager;

        private List<MapSelectable> mapSelectables = new List<MapSelectable>();

        private void Start()
        {
            mapSelectables = GetComponentsInChildren<MapSelectable>().ToList();
            SetupSelectables();

        }

        void SetupSelectables()
        {
            List<MapData> allMaps = mapCollectionManager.AllMaps;
            for (int i = 0; i < mapSelectables.Count; i++)
            {
                MapData mapDataForLoop = allMaps[i];

                mapSelectables[i].Setup(mapDataForLoop);
            }
        }

        public void SelectMap(int indexFromList)
        {
            int lastIndex = mapSelectables.Count - 1;
            int selectedMapIndex;

            if(indexFromList == lastIndex) //The last index should always be the random map.
            {
                int randomSelectedMap = Random.Range(0, lastIndex);
                MapSelectable selectedRandomMapSelectable = mapSelectables[randomSelectedMap];
                selectedMapIndex = selectedRandomMapSelectable.MapSelectableIndex;
            }
            else
            {
                MapSelectable selectedMapSelectable = mapSelectables[indexFromList];
                selectedMapIndex = selectedMapSelectable.MapSelectableIndex;
            }

            SceneSwitchManager.SwitchToSceneWithIndex(selectedMapIndex);
        }
    }
}
