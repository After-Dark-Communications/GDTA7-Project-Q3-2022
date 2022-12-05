using EventSystem;
using MapSelection;
using ShipParts.Ship;
using ShipSelection.ShipBuilders;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Managers
{
    public class MapSelectionManager : MonoBehaviour
    {
        [SerializeField]
        private MapCollectionManager mapCollectionManager;
        [SerializeField]
        private TextMeshProUGUI titleText;

        private const string _selectionPrefix = "Player", _selectionSuffix = "choose a map";

        private MapSelectable[] mapSelectables;

        private void Awake()
        {
            SetChoosablePlayer();
        }

        private void Start()
        {
            mapSelectables = GetComponentsInChildren<MapSelectable>();//.ToList();
            SetupSelectables();

        }

        void SetupSelectables()
        {
            List<MapData> allMaps = mapCollectionManager.AllMaps;
            for (int i = 0; i < mapSelectables.Length; i++)
            {
                MapData mapDataForLoop = allMaps[i];

                mapSelectables[i].Setup(mapDataForLoop);
            }
        }

        public void SelectMap(int indexFromList)
        {
            int lastIndex = mapSelectables.Length - 1;
            int selectedMapIndex;

            if (indexFromList == lastIndex) //The last index should always be the random map.
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

        private void SetChoosablePlayer()
        {
            if (ShipBuildManager.Instance.FirstPlayerReady == null)
            { return; }
            ShipBuilder controllingPlayer = ShipBuildManager.Instance.FirstPlayerReady;//.PlayerDevice;
            for (int i = 0; i < InputSystem.devices.Count; i++)
            {
                if (InputSystem.devices[i] != controllingPlayer.PlayerDevice)
                {
                    InputSystem.DisableDevice(InputSystem.devices[i]);
                }
                else
                {
                    InputSystem.EnableDevice(InputSystem.devices[i]);
                }
            }

            titleText.text = $"{_selectionPrefix} {controllingPlayer.PlayerNumber + 1}, {_selectionSuffix}";
        }
    }
}
