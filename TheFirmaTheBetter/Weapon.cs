using System.Collections;
using UnityEngine;

public class Weapon : Part
{
    [SerializeField]
    private WeaponData weaponData;

    public override string PartName => "Weapon";

    [SerializeField]
    private WeaponData weaponData;

    [SerializeField]
    private Transform[] projectileStartingPoints;

    private ObjectPool projectilesPool;
    private float lastShootTime;

    private void Start()
    {
        projectilesPool = new ObjectPool(weaponData.ProjectilePrefab, 10);
    }

    private void Update()
    {
        if (Input.GetButton("Fire1"))
        {
            FireWeapon();
        }        
    }

    public void FireWeapon()
    {
        if (lastShootTime + 1/weaponData.FireRate < Time.time)
        {
            foreach (Transform point in projectileStartingPoints)
            {
                // Get projectile from pool
                GameObject projectileObject = projectilesPool.RentFromPool();
                projectileObject.transform.SetPositionAndRotation(point.position, point.rotation);

                Vector3 direction = GetShootDirection(point, weaponData.SideSpreadAngle);

                Projectile projectile = projectileObject.GetComponent<Projectile>();

                // Fire projectile
                projectileObject.GetComponent<Rigidbody>().AddForce(direction * projectile.ProjectileSpeed, ForceMode.Impulse);

                // Return projectile after time
                float projectileLifetime = weaponData.Range / projectile.ProjectileSpeed;
                StartCoroutine(ReturnProjectile(projectileObject, projectileLifetime));
            }

            lastShootTime = Time.time;
        }
    }

    private Vector3 GetShootDirection(Transform shootingPoint, float sideSpreadAngle)
    {
        Vector3 direction = shootingPoint.forward;

        Quaternion rotatiton = Quaternion.AngleAxis(Random.Range(-sideSpreadAngle, sideSpreadAngle), Vector3.up);
        direction = rotatiton * direction;

        direction.Normalize();

        return direction;
    }

    IEnumerator ReturnProjectile(GameObject projectile, float seconds)
    {
        yield return new WaitForSeconds(seconds);
        projectilesPool.ReturnToPool(projectile);
    }
}
