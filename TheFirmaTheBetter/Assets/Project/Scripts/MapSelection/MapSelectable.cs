using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace MapSelection
{
    public class MapSelectable : MonoBehaviour
    {
        [SerializeField]
        private TMP_Text mapSelectableName;
        [SerializeField]
        private Image mapSelectableImage;
        [SerializeField]
        private TMP_Text mapSelectableGameMode;
        [SerializeField]
        private TMP_Text gameModeDescription;

        private int mapSelectableIndex;

        public void Setup(MapData mapData)
        {
            mapSelectableName.text = mapData.MapName;
            mapSelectableImage.sprite = mapData.MapPicture;
            mapSelectableGameMode.text = mapData.GameMode;
            mapSelectableIndex = mapData.MapSceneIndex;
            gameModeDescription.text = mapData.GameModeDescription;
        }

        public TMP_Text MapSelectableName { get { return mapSelectableName; } }
        public Image MapSelectableImage { get { return mapSelectableImage; } }
        public TMP_Text MapSelectableGameMode { get { return mapSelectableGameMode; } }
        public int MapSelectableIndex { get { return mapSelectableIndex; } }
        public TMP_Text MapSelectableDescription { get { return gameModeDescription; } }
    }
}
