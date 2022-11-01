using EventSystem;
using ShipParts.Ship;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RespawnIndicator : MonoBehaviour
{
    private Camera cam;
    private TMP_Text timerText;
    private ShipBuilder shipThatWillRespawn;

    [SerializeField]
    private int timeToRespawn = 10;
    private float timer;

    bool isEnabled;
    // Start is called before the first frame update
    void Start()
    {
        timer = timeToRespawn;
        timerText = GetComponentInChildren<TMP_Text>();
        cam = Camera.main;
        timerText.SetText($"{timer}");
    }
    private void OnEnable()
    {
        isEnabled = true;
    }
    public void SetOwnerOfIndicator(ShipBuilder owner)
    {
        shipThatWillRespawn = owner;
    }

    // Update is called once per frame
    void Update()
    {
        if (isEnabled == false) return;
        transform.LookAt(transform.position + cam.transform.rotation * Vector3.forward,
          cam.transform.rotation * Vector3.up);

        HandleTimer();
    }

    private void HandleTimer()
    {
        timer -= Time.deltaTime;
        float roundedTimer = Mathf.Round(timer);
        timerText.SetText($"{roundedTimer}");
        if (timer <= 0)
        {
            Channels.KingOfTheHill.OnKingOfTheHillPlayerRespawn?.Invoke(shipThatWillRespawn);
            this.gameObject.SetActive(false);
        }
    }

    private void OnDisable()
    {
        isEnabled = false;
    }
}
