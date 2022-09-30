using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewSpecialData", menuName = "Part/Create new SpecialData")]
public class SpecialData : PartData 
{
    [Header("SpecialStats")]
    [SerializeField]
    private string description;
    [SerializeField]
    private int abilityCooldown;

    public string Description => description;

    public int AbilityCooldown => abilityCooldown;
}
