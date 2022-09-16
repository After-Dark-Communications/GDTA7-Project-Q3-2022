using UnityEngine;
using UnityEngine.UI;

public class Fillbar : MonoBehaviour
{
    [SerializeField]
    protected Image fillImage;

    protected virtual void UpdateFill(float fillAmount)
    {
        fillImage.fillAmount = fillAmount;
    }
}
