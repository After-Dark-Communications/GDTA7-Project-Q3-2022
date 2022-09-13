using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewSpecialData", menuName = "Create new SpecialData")]
public class SpecialData : ScriptableObject
{
    private int abilityCooldown;

    public int AbilityCooldown => abilityCooldown;
}
