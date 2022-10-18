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

        public TMP_Text MapSelectableName { get { return mapSelectableName; } }
        public Image MapSelectableImage { get { return mapSelectableImage; } }
        public TMP_Text MapSelectableGameMode { get { return mapSelectableGameMode; } }
    }
}