using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartsCollectionManager : MonoBehaviour
{
    [SerializeField]
    private List<Engine> engineList;
    [SerializeField]
    private List<Core> coreList;
    [SerializeField]
    private List<Weapon> weaponList;
    [SerializeField]
    private List<Special> specialList;

    public List<Engine> EngineList { get => engineList; }
    public List<Core> CoreList { get => coreList; }
    public List<Weapon> WeaponList { get => weaponList; }
    public List<Special> SpecialList { get => specialList; }
}
