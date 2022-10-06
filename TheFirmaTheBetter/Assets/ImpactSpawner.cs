using EventSystem;
using Pooling;
using ShipParts.Ship;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImpactSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject impactHitPrefab;

    public void SpawnImpactHitPrefab()
    {
        GameObject spawnedObject = Instantiate(impactHitPrefab);
        spawnedObject.transform.position = transform.position;
    }
}
