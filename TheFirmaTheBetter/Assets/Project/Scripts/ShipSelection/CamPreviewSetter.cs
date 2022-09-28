using UnityEngine;
using UnityEngine.UI;

namespace ShipSelection
{
    [RequireComponent(typeof(RawImage))]
    public class CamPreviewSetter : MonoBehaviour
    {
        [SerializeField]
        private RawImage rawImage;

        public RawImage RawImage { get => rawImage; set => rawImage = value; }
    }
}