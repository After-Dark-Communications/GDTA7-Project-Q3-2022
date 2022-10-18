using Managers;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

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
            MapSelectable selectedMapSelectable = mapSelectables[indexFromList];
            int selectedMapIndex = selectedMapSelectable.MapSelectableIndex;
            SceneSwitchManager.SwitchToSceneWithIndex(selectedMapIndex);
        }
    }
}
