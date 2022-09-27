using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(RawImage))]
public class CamPreviewSetter : MonoBehaviour
{
    [SerializeField]
    private RawImage rawImage;

    public RawImage RawImage { get => rawImage; set => rawImage = value; }
}