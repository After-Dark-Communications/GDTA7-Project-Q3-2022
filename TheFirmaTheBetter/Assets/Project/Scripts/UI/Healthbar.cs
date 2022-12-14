using EventSystem;
using UnityEngine;

namespace UI
{
    public class Healthbar : ShipStatBar
    {
        [SerializeField]
        private Gradient colorGradient;

        private void Awake()
        {
            Channels.OnHealthChanged += UpdateStatbar;
        }

        private void OnDestroy()
        {
            Channels.OnHealthChanged -= UpdateStatbar;
        }

        protected override void UpdateFill(float fillAmount)
        {
            fillImage.color = colorGradient.Evaluate(fillAmount);
            base.UpdateFill(fillAmount);
        }
    }
}