using EventSystem;
using UnityEngine;

public class DebugPlayerSpawner : MonoBehaviour
{
    public GameObject playerPrefab;
    public int playerCount;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < playerCount; i++)
        {
            GameObject player = Instantiate(playerPrefab, transform.position + new Vector3(i * 2, 0, i * 2), transform.rotation);
            Channels.OnPlayerSpawned?.Invoke(player, i + 1);
        }

        Channels.OnHealthChanged.Invoke(1, 0.9f);
        Channels.OnHealthChanged.Invoke(2, 0.5f);
        Channels.OnHealthChanged.Invoke(3, 0.2f);
        Channels.OnFuelChanged.Invoke(1, 0.4f);
        Channels.OnFuelChanged.Invoke(2, 0.6f);
        Channels.OnFuelChanged.Invoke(3, 0.8f);
        Channels.OnEnergyChanged.Invoke(1, 0.1f);
        Channels.OnEnergyChanged.Invoke(2, 0.5f);
        Channels.OnEnergyChanged.Invoke(3, 1f);
    }
}
