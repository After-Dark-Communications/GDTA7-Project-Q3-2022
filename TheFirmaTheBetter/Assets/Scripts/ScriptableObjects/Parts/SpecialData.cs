using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewSpecialData", menuName = "Part/Create new SpecialData")]
public class SpecialData : PartData 
{
    [Header("SpecialStats")]
    [SerializeField]
    private int abilityCooldown;

    public int AbilityCooldown => abilityCooldown;
}
