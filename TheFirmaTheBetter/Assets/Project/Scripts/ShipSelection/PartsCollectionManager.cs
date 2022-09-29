using Parts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartsCollectionManager : Manager
{
    [SerializeField]
    private List<Engine> engineList;
    [SerializeField]
    private List<Core> coreList;
    [SerializeField]
    private List<Weapon> weaponList;
    [SerializeField]
    private List<SpecialAbility> specialList;

    private List<Part> allParts = new List<Part>();

    private void Awake()
    {
        allParts.AddRange(coreList);
        allParts.AddRange(engineList);
        allParts.AddRange(weaponList);
        allParts.AddRange(specialList);
    }

    public List<Engine> EngineList { get => engineList; }
    public List<Core> CoreList { get => coreList; }
    public List<Weapon> WeaponList { get => weaponList; }
    public List<SpecialAbility> SpecialList { get => specialList; }
    public List<Part> AllParts { get => allParts; }
}
