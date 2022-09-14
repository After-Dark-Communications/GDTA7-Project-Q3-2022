using UnityEngine;

public class Projectile : MonoBehaviour, IObjectPoolItem
{

    public void ResetPoolItem()
    {
        // Called whenever the item is returned to the object pool

        Rigidbody projectileRigidbody = GetComponent<Rigidbody>();
        projectileRigidbody.velocity = Vector3.zero;
        projectileRigidbody.angularVelocity = Vector3.zero;
    }

    

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ship"))
        {
            // ship.TakeDamage;
            //Debug.Log("A ship was hit");
        }
        else
        {
            // destroy bullet
            //Debug.Log("Something else was hit: " + other.name);
        }
    }
}
