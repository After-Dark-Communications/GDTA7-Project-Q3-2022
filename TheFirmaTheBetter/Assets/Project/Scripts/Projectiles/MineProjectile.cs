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

    private List<ShipBuilder> buildersInRange = new List<ShipBuilder>();

    private void OnTriggerEnter(Collider other)
    {
        ShipBuilder otherBuilder = other.GetComponentInParent<ShipBuilder>();

        if (otherBuilder == null)
            return;

        buildersInRange.Add(otherBuilder);
    }

    private void OnTriggerStay(Collider other)
    {
        if (armed == false)
            return;

        Explode();
    }

    private void OnTriggerExit(Collider other)
    {
        ShipBuilder otherBuilder = other.GetComponentInParent<ShipBuilder>();

        if (otherBuilder == null)
            return;

        int index = buildersInRange.FindIndex(b => b.PlayerNumber == otherBuilder.PlayerNumber);

        buildersInRange.RemoveAt(index);
    }

    private void Update()
    {
        ArmMine();

        currentTime += Time.deltaTime;



        if (currentTime < mineData.ExplodingTime)
            return;

        Explode();
    }

    private void ArmMine()
    {
        if (armed)
            return;

        currentArmingTime += Time.deltaTime;

        if (currentArmingTime < mineData.ArmingTime)
            return;

        armed = true;
    }

    private void Explode()
    {
        GameObject spawnedExplosion  = Instantiate(mineExplosionPrefab);
        spawnedExplosion.transform.position = transform.position;

        foreach (ShipBuilder item in buildersInRange)
        {
            Channels.OnPlayerTakeDamage(item, mineData.Damage);
        }

        Destroy(gameObject);
    }
}
