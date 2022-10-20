using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EventSystem;
using ShipParts.Ship;
public class SFXManager : MonoBehaviour
{
    [Header("FMOD Event Lists")]
    [SerializeField]
    private List<FMODUnity.EventReference> crowdEvents;
    [SerializeField]
    private List<FMODUnity.EventReference> deathEvents;

    private void Start()
    {
        DontDestroyOnLoad(gameObject);
        Channels.OnPlayerBecomesDeath += PlayDeathSFX;
        Channels.OnGameStart += PlayCrowdSFX;
    }

    private void OnDisable()
    {
        Channels.OnPlayerBecomesDeath -= PlayDeathSFX;
        Channels.OnGameStart -= PlayCrowdSFX;
    }

    void PlayRandomEvent(List<FMODUnity.EventReference> events)
    {
        FMODUnity.RuntimeManager.PlayOneShot(events[Random.Range(0, events.Count)]);
    }

    void PlayDeathSFX(ShipBuilder builder, int playerNumber)
    {
        PlayRandomEvent(deathEvents);
        PlayCrowdSFX();
    }

    void PlayCrowdSFX()
    {
        PlayRandomEvent(crowdEvents);
    }
}
