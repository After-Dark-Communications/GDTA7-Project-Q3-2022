using EventSystem;

public class AmmoBar : ShipStatBar
{
    private void OnEnable()
    {
        Channels.OnAmmoChanged += UpdateStatbar;
    }

    private void OnDisable()
    {
        Channels.OnAmmoChanged -= UpdateStatbar;
    }
}
