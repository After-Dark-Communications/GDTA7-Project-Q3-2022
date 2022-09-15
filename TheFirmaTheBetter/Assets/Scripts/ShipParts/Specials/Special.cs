using UnityEngine;
public abstract class Special : Part
{
    [SerializeField]
    private SpecialData specialData;

    public override string partCategoryName => "Special";
}