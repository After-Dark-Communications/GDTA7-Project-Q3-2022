using Assets.Project.Scripts.Collision;
using UnityEngine;

public class Projectile : MonoBehaviour, IObjectPoolItem, ICollidable
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
        Rigidbody projectileRigidbody = GetComponent<Rigidbody>();
        projectileRigidbody.velocity = Vector3.zero;
        projectileRigidbody.angularVelocity = Vector3.zero;
    }

    public void SpawnObjectOnImpact()
    {
        GameObject impactParticleObject = Instantiate(projectileData.SpawnedObjectOnImpact, transform.position, transform.rotation);

        ParticleSystem impactParticleSystem = impactParticleObject.GetComponent<ParticleSystem>();
        float impactTime = 0;
        if (impactParticleSystem != null)
        {
            impactTime = impactParticleSystem.main.duration;
        }

        Destroy(impactParticleObject, impactTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        ICollidable collisionObject = other.GetComponentInParent<ICollidable>();

        if (collisionObject != null)
        {
            collisionObject.HandleCollision(this);

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

    public void HandleCollision<T1>(T1 objectThatHit) where T1 : ICollidable { }

    public void DestroySelf()
    {
        ResetPoolItem();
    }

    public int ProjectileDamage { get { return projectileDamage; } }
    public float ProjectileSpeed { get { return projectileSpeed; } }
    public float ArmingTime { get { return armingTime; } }
    public int AmountToSpawn { get { return amountToSpawn; } }

}
