using ShipParts;
using ShipParts.Ship;
using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace EventSystem
{
    public static class Channels
    {
    public static InputChannel Input = new InputChannel();
    public static MovementChannel Movement = new MovementChannel();

        /// <summary> Callled when firemode has to change to the new value
        /// <para><see cref="bool" /> new firemode value</para>
        /// </summary>
        public static Action<bool> OnChangeFireMode;
        /// <summary>
        /// <para><see cref="int" /> PlayerNumber</para>
        /// <para><see cref="int" /> amount of energy used</para>
        /// </summary>
        public static Action<int, int> OnEnergyUsed;
        /// <summary>
        /// <para><see cref="int" /> Player number</para>
        /// <para><see cref="float" /> New energy persentage </para>
        /// </summary>
        public static Action<int, float> OnEnergyChanged;
        /// <summary>
        /// <para><see cref="int" /> Player number</para>
        /// <para><see cref="int" /> amount of energy to refill</para>
        /// </summary>
        public static Action<int, int> OnRefillEnergy;
        /// <summary>
        /// <para><see cref="ShipBuilder" /> The shipbuilder that is changed</para>
        /// <para><see cref="ShipStats" /> The updated shipstats</para>
        /// </summary>
        public static Action<ShipBuilder, ShipStats> OnPlayerStatsChanged;
        /// <summary>
        /// <para><see cref="ShipBuilder" /> The shipbuilder that is in need of dying</para>
        /// </summary>
        public static Action<ShipBuilder> OnPlayerBecomesDeath;
        /// <summary>
        /// <para><see cref="GameObject" /> Object of the entered zone</para>
        /// </summary>
        public static Action<GameObject> OnZoneEntered;
        /// <summary>
        /// <para><see cref="Manager" /> The initialized manager</para>
        /// </summary>
        public static Action<Manager> OnManagerInitialized;
        /// <summary>
        /// <para><see cref="Part" /> Ship part that has been selected</para>
        /// <para><see cref="int" /> Player number</para>
        /// </summary>
        public static Action<Part, int> OnShipPartSelected;
        /// <summary>
        /// <para><see cref="ShipBuilder" /> Builder from the completed ship</para>
        /// </summary>
        public static Action<ShipBuilder> OnShipCompleted;
        /// <summary>
        /// <para><see cref="int" /> Player number</para>
        /// <para><see cref="InputDevice" /> The joined player device</para>
        /// </summary>
        public static Action<int, InputDevice> OnPlayerJoined;
        /// <summary>
        /// <para><see cref="ShipBuilder" /> Shipbuilder object that got hit by the bullet</para>
        /// <para><see cref="int" /> DamageAmount</para>
        /// </summary>
        public static Action<ShipBuilder, int> OnPlayerTakeDamage;
        public static Action OnEveryPlayerReady;
        /// <summary>
        /// <para><see cref="GameObject" /> The shipbuilder object that is spawned</para>
        /// <para><see cref="int" /> The player number </para>
        /// </summary>
        public static Action<GameObject, int> OnPlayerSpawned;

        /// <summary>
        /// <para><see cref="int" /> Player number</para>
        /// <para><see cref="float" /> New health persentage </para>
        /// </summary>
        public static Action<int, float> OnHealthChanged;
        /// <summary>
        /// <para><see cref="int" /> Player number</para>
        /// <para><see cref="float" /> New fuel persentage </para>
        /// </summary>
        public static Action<int, float> OnFuelChanged;

    }

}