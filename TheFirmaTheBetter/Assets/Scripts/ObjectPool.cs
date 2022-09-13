using System.Collections.Generic;
using UnityEngine;

public class ObjectPool
{
    protected readonly Transform poolParent;
    private readonly GameObject prefab;

    private readonly List<GameObject> availableObjectsInPool = new List<GameObject>();

    public ObjectPool(GameObject prefabToPool)
    {
        prefab = prefabToPool;
        this.poolParent = new GameObject(GetPoolName(prefab)).transform;
    }

    public ObjectPool(GameObject prefabToPool, int startAmountInPool)
    {
        prefab = prefabToPool;
        this.poolParent = new GameObject(GetPoolName(prefab)).transform;
        AddToPool(startAmountInPool);
    }

    public GameObject RentFromPool()
    {
        FillPoolIfNeeded();
        
        GameObject toRent = availableObjectsInPool[0];
        toRent.SetActive(true);
        availableObjectsInPool.RemoveAt(0);

        return toRent; 
    }

    public void ReturnToPool(GameObject toReturn)
    {
        IObjectPoolItem item = toReturn.GetComponent<IObjectPoolItem>();

        if (item == null)
            return;

        item.ResetPoolItem();

        toReturn.SetActive(false);
        ResetGameObject(toReturn);

        availableObjectsInPool.Add(toReturn);
    }

    private string GetPoolName(GameObject prefab)
    {
        return $"{prefab.name}s Pool";
    }

    private void FillPoolIfNeeded()
    {
        if (availableObjectsInPool.Count <= 0)
        {
            AddToPool(1);
        }
    }

    private void AddToPool(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            GameObject instance = GameObject.Instantiate(prefab);

            ResetGameObject(instance);

            availableObjectsInPool.Add(instance);
        }
    }

    private void ResetGameObject(GameObject toReset)
    {
        toReset.transform.position = Vector3.zero;
        toReset.transform.rotation = Quaternion.identity;
        toReset.transform.localScale = Vector3.one;
        toReset.transform.SetParent(poolParent, false);
    }
}
