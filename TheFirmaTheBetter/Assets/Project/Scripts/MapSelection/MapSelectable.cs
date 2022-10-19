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

    private int mapSelectableIndex;

    public void Setup(MapData mapData)
    {
        mapSelectableName.text = mapData.MapName;
        mapSelectableImage = mapData.MapPicture;
        mapSelectableGameMode.text = mapData.GameMode;
        mapSelectableIndex = mapData.MapSceneIndex;
    }

    public TMP_Text MapSelectableName { get { return mapSelectableName; } }
    public Image MapSelectableImage { get { return mapSelectableImage; } }
    public TMP_Text MapSelectableGameMode { get { return mapSelectableGameMode; } }
    public int MapSelectableIndex { get { return mapSelectableIndex; } }
}