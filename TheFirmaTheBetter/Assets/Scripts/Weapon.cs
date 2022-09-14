using UnityEngine;

public class Weapon : Part
{
    public override string PartName => "Weapon";

    [SerializeField]
    private WeaponData weaponData;

    [SerializeField]
    private Transform[] projectileStartingPoints;

    private float lastShootTime;

    // TEMPORARY
    [SerializeField]
    private int bulletSpeed = 10;
    [SerializeField]
    private float bulletRange = 2;

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
                // Spawn projectile
                GameObject projectile = Instantiate(weaponData.ProjectilePrefab, point.position, point.rotation);

                Vector3 direction = GetShootDirection(point, weaponData.SideSpreadAngle);

                // Fire projectile
                projectile.GetComponent<Rigidbody>().AddForce(direction * bulletSpeed, ForceMode.Impulse);

                // Destroy projectile after time
                Destroy(projectile, bulletRange);
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
}
