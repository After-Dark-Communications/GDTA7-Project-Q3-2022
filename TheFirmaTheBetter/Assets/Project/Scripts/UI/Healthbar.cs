using EventSystem;
using UnityEngine;

public class Healthbar : ShipStatBar
{
    [SerializeField]
    private Color lowColor;

    [SerializeField]
    private Color mediumColor;

    [SerializeField]
    private Color highColor;

    private void OnEnable()
    {
        Channels.OnHealthChanged += UpdateStatbar;
    }

    private void OnDisable()
    {
        Channels.OnHealthChanged -= UpdateStatbar;
    }

    protected override void UpdateFill(float fillAmount)
    {
        if (fillAmount < 0.33f)
        {
            fillImage.color = lowColor;
        }
        else if (fillAmount > 0.66f)
        {
            fillImage.color = highColor;
        }
        else
        {
            fillImage.color = mediumColor;
        }

        base.UpdateFill(fillAmount);
    }
}
