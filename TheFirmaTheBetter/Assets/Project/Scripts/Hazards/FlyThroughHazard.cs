using Collisions;
using EventSystem;
using ShipParts.Ship;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Hazards
{
    public class FlyThroughHazard : Hazard
    {
        [SerializeField]
        private float timeToTakeDamage = 30;

        private List<ICollidable> shipsCollidersThatEntered = new List<ICollidable>();

        private float timer = 0;
        private bool damagePaused = false;

        private void Start()
        {
            Channels.OnPlayerBecomesDeath += RemoveDeadPlayerFromTrigger;
            Channels.OnRoundOver += OnRoundOver;
            Channels.OnRoundStarted += OnRoundStarted;
        }

        private void OnDestroy()
        {
            Channels.OnPlayerBecomesDeath -= RemoveDeadPlayerFromTrigger;
            Channels.OnRoundOver -= OnRoundOver;
            Channels.OnRoundStarted -= OnRoundStarted;
        }

        private void Update()
        {
            if (damagePaused || shipsCollidersThatEntered.Count == 0)
                return;

            timer += Time.deltaTime;
            if (timer >= timeToTakeDamage)
            {
                DoDamageToAllShips(shipsCollidersThatEntered);
                timer = 0;
            }
        }

        private void DoDamageToAllShips(List<ICollidable> EnteredShips)
        {
            List<ICollidable> ships = EnteredShips;
            for (int i = 0; i < shipsCollidersThatEntered.Count; i++)
            {
                shipsCollidersThatEntered[i].HandleCollision(this, null);
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            ICollidable collisionObject = other.GetComponentInParent<ICollidable>();
            AddCollidable(collisionObject);
        }

        private void OnTriggerExit(Collider other)
        {
            ICollidable collisionObject = other.GetComponentInParent<ICollidable>();
            RemoveCollidable(collisionObject);
        }

        private void RemoveDeadPlayerFromTrigger(ShipBuilder deadPlayer, int playerNumber)
        {
            ICollidable collidable = deadPlayer.GetComponent<ICollidable>();
            RemoveCollidable(collidable);
        }

        private void RemoveAllFromTrigger()
        {
            shipsCollidersThatEntered.Clear();
        }

        private void AddCollidable(ICollidable collidable)
        {
            if (collidable != null)
            {
                if (collidable is ShipCollision)
                {
                    if (shipsCollidersThatEntered.Contains(collidable))
                        return;
                    shipsCollidersThatEntered.Add(collidable);
                }
            }
        }

        private void RemoveCollidable(ICollidable collidable)
        {
            if (collidable != null)
            {
                if (collidable is ShipCollision)
                {
                    if (shipsCollidersThatEntered.Contains(collidable))
                    {
                        shipsCollidersThatEntered.Remove(collidable);

                        if (shipsCollidersThatEntered.Count <= 0)
                        {
                            // Reset the timer on last player to exit
                            timer = 0;
                        }
                    }
                }
            }
        }

        private void OnRoundOver(int roundIndex, int winnerIndex)
        {
            PauseDamage();
        }

        private void OnRoundStarted(int roundIndex, int numberOfRounds)
        {
            RemoveAllFromTrigger();
            UnpauseDamage();
        }

        private void PauseDamage()
        {
            damagePaused = true;
        }

        private void UnpauseDamage()
        {
            damagePaused = false;
        }
    }
}