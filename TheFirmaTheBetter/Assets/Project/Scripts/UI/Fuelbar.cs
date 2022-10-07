using EventSystem;

namespace UI
{
    public class FuelBar : ShipStatBar
    {
        private void OnEnable()
        {
            Channels.OnFuelChanged += UpdateStatbar;
        }

        private void OnDisable()
        {
            Channels.OnFuelChanged -= UpdateStatbar;
        }
    }
}