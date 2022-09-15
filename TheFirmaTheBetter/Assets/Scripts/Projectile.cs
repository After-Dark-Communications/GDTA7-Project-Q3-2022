using UnityEngine;

public class Projectile : MonoBehaviour, IObjectPoolItem
{
    [SerializeField]
    private ProjectileData projectileData;

    private int projectileDamage;
    private float projectileSpeed;
    private float armingTime;
    private int amountToSpawn;

    private void OnEnable()
    {
        projectileDamage = projectileData.Damage;
        projectileSpeed = projectileData.ProjectileSpeed;
        armingTime = projectileData.ArmingTime;
        amountToSpawn = projectileData.AmountToSpawn;
    }

    public void ResetPoolItem()
    {
        // Called whenever the item is returned to the object pool

        Rigidbody projectileRigidbody = GetComponent<Rigidbody>();
        projectileRigidbody.velocity = Vector3.zero;
        projectileRigidbody.angularVelocity = Vector3.zero;
    }

    public void SpawnObjectOnImpact()
    {
        Debug.Log("Object has spawned");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ship"))
        {
            Debug.Log($"Ship takes {ProjectileDamage}");
            if(amountToSpawn > 0)
            {
                SpawnObjectOnImpact();
            }
            // ship.TakeDamage;
            //Debug.Log("A ship was hit");
        }
        else
        {
            // destroy bullet
            //Debug.Log("Something else was hit: " + other.name);
        }
    }

    public int ProjectileDamage { get { return projectileDamage; } }
    public float ProjectileSpeed { get { return projectileSpeed; } }
    public float ArmingTime { get { return armingTime; } }
    public int AmountToSpawn { get { return amountToSpawn; } }

}
