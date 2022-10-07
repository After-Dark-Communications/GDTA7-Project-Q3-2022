using EventSystem;
using Pooling;
using Projectiles;
using ShipParts.Ship;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MineProjectile : Projectile
{
    [SerializeField]
    private MineData mineData;

    [SerializeField]
    private GameObject mineExplosionPrefab;

    private float currentTime = 0;
    private float currentArmingTime = 0;

    private bool armed = false;
    private bool exploded = false;

    private List<ShipBuilder> buildersInRange = new List<ShipBuilder>();

    private void OnTriggerEnter(Collider other)
    {
        ShipBuilder otherBuilder = other.GetComponentInParent<ShipBuilder>();

        if (otherBuilder == null)
            return;

        ShipBuilder inList = buildersInRange.Find(sb => sb.PlayerNumber == otherBuilder.PlayerNumber); 

        if (inList != null)
            return;

        buildersInRange.Add(otherBuilder);
    }

    private void OnTriggerExit(Collider other)
    {
        ShipBuilder otherBuilder = other.GetComponentInParent<ShipBuilder>();

        if (otherBuilder == null)
            return;

        int index = buildersInRange.FindIndex(b => b.PlayerNumber == otherBuilder.PlayerNumber);

        if (index >= 0)
            return;

        buildersInRange.RemoveAt(index);
    }

    private void Update()
    {
        ArmMine();

        CheckExplode();

        currentTime += Time.deltaTime;

        if (currentTime < mineData.ExplodingTime)
            return;

        Explode();
    }

    private void CheckExplode()
    {
        if (buildersInRange.Count <= 0 || armed == false || exploded)
            return;

        armed = false;

        Explode();
    }

    private void ArmMine()
    {
        if (armed || exploded)
            return;

        currentArmingTime += Time.deltaTime;

        if (currentArmingTime < mineData.ArmingTime)
            return;

        armed = true;
    }

    private void Explode()
    {
        exploded = true;
        Destroy(gameObject);
        GameObject spawnedExplosion  = Instantiate(mineExplosionPrefab);
        spawnedExplosion.transform.position = transform.position;

        foreach (ShipBuilder item in buildersInRange)
        {
            Channels.OnPlayerTakeDamage(item, mineData.Damage, PlayerIndex);
        }

    }
}
