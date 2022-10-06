using Projectiles;
using ShipParts.Ship;
using UnityEngine;

public class HookShotProjectile : Projectile
{
    private const float HookConnectedTime = 4f;

    private float currentConnectedTime  = 0;
    private float currentArmingTime = 0;

    private bool armed = false;

    private ShipBuilder target;
    private Rigidbody firerer;

    private SpringJoint joint;

    private void Awake()
    {
        joint = GetComponent<SpringJoint>();
    }

    public void SetJointFirer(Rigidbody shipbuilder)
    {
        firerer = shipbuilder;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (armed)
            return;

        ShipBuilder shipBuilder = other.gameObject.GetComponentInParent<ShipBuilder>();

        if (shipBuilder == null)
            return;

        joint.connectedBody = firerer;
        target = shipBuilder;
    }

    private void Update()
    {
        if (target == null)
            return;

        FollowHookShotWithTarget();

        currentConnectedTime += Time.deltaTime;

        if (currentConnectedTime < HookConnectedTime)
            return;

        Destroy(transform.parent.gameObject);
    }

    public void ArmHook()
    {
        if (armed)
            return;

        currentArmingTime += Time.deltaTime;

        if (currentArmingTime < ProjectileData.ArmingTime)
            return;

        armed = true;
    }

    private void FollowHookShotWithTarget()
    {
        if (target == null)
            return;

        transform.parent.position = target.transform.position;
    }
}