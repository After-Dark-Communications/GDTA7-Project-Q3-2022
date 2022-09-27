using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ProjectileData", menuName = "Projectiles/Create new ProjectileData")]
public class ProjectileData : ScriptableObject
{
    [SerializeField]
    private int damage;
    [SerializeField]
    private float projectileSpeed;
    [SerializeField]
    private float armingTime;
    [SerializeField]
    private bool isDoT;
    [Header("On Impact")]
    [SerializeField]
    private GameObject spawnedObjectOnImpact;
    [SerializeField]
    private int amountToSpawn;

    public int Damage { get { return damage; } }
    public float ProjectileSpeed { get { return projectileSpeed; } }
    public float ArmingTime { get { return armingTime; } }
    public bool IsDoT { get { return isDoT; } }
    public GameObject SpawnedObjectOnImpact { get { return spawnedObjectOnImpact; } }
    public int AmountToSpawn { get { return amountToSpawn; } }
}
