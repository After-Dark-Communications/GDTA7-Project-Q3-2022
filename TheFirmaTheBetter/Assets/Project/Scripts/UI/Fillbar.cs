using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class FillBar : MonoBehaviour
    {
        [SerializeField]
        protected Image fillImage;

        protected virtual void UpdateFill(float fillAmount)
        {
            fillImage.fillAmount = fillAmount;
        }
    }
}