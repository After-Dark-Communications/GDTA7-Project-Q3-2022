using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
using ShipParts.Ship;
using ShipSelection;
using EventSystem;

namespace Audio
{
    public class ShipSoundManager : MonoBehaviour
    {
        [Header("Engine")]
        [SerializeField]
        private StudioEventEmitter engineEmitter;

        [Tooltip("The multiplier used for manipulating the RPM parameter")]
        [SerializeField]
        private float RPMSpeed;

        [Header("Health")]
        [SerializeField]
        private StudioEventEmitter healthEmitter;

        [Range(0f, 100f)]
        private float rpm;
        [Range(0f, 100f)]
        private float health;
        private int playerNumber;

        // Start is called before the first frame update
        void Start()
        {
            try
            {
                playerNumber = GetComponent<ShipInfo>().PlayerNumber;
            }
            catch
            {

            }
            Channels.Movement.OnShipMove += SetRPM;
            Channels.OnHealthChanged += SetHealth;
            Channels.OnEnableEngine += EnableEngine;
            Channels.OnDisableEngine += DisableEngine;
            healthEmitter.SetParameter("Player_Health", 100f);
        }

        private void OnDisable()
        {
            Channels.Movement.OnShipMove -= SetRPM;
            Channels.OnHealthChanged -= SetHealth;
            Channels.OnEnableEngine -= EnableEngine; 
            Channels.OnDisableEngine -= DisableEngine;
        }

        private void SetRPM(Vector2 movement, int playerNumber)
        {
            if (this.playerNumber == playerNumber)
            {
                rpm = movement.x + movement.y;
                if (rpm < 0)
                {
                    rpm = rpm * -1;
                }
                if (engineEmitter.IsPlaying())
                {
                    engineEmitter.SetParameter("RPM", rpm * RPMSpeed);
                }
            }
        }

        private void SetHealth(int playerNumber, float health)
        {
            if (this.playerNumber == playerNumber)
            {
                this.health = health * 100;
                healthEmitter.SetParameter("Player_Health", this.health);
                if (this.health <= 20 && this.health >= 0)
                {
                    if (!healthEmitter.IsPlaying())
                    {
                        healthEmitter.Play();
                    }
                }
            }
        }

        private void DisableEngine()
        {
            engineEmitter.Stop();
        }

        private void EnableEngine()
        {
            engineEmitter.Play();
        }

    }
}