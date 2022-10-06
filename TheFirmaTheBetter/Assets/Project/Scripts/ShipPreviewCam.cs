using UnityEngine;

public class ShipPreviewCam : MonoBehaviour
{
    [SerializeField]
    private GameObject shipParent;

    public void PlaceShipPreview(GameObject ship)
    {
        foreach (Transform child in shipParent.transform)
        {
            child.gameObject.SetActive(false);
        }

        ship.transform.SetParent(shipParent.transform);
        ship.transform.localPosition = Vector3.zero;
        ship.transform.rotation = Quaternion.Euler(Vector3.zero);
        ship.SetActive(true);
    }

    private void OnDrawGizmosSelected()
    {
        // Creates an outline for where the ship will be placed
        Gizmos.DrawWireCube(shipParent.transform.position, Vector3.one);
    }
}
