using Managers;
using ShipParts;
using ShipParts.Ship;
using ShipSelection;
using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace EventSystem
{
    public static class Channels
    {
        public static InputChannel Input = new InputChannel();
        public static MovementChannel Movement = new MovementChannel();
        public static AnnouncerChannel Announcer = new AnnouncerChannel();
        public static KingOfTheHillChannel KingOfTheHill = new KingOfTheHillChannel();

        public delegate void ChangeFireMode(bool newFireModeValue);
        public delegate void EnergyUsed(int playerNumber, int amount);
        public delegate void EnergyChanged(int playerNumber, float newEnergyPersentage);
        public delegate void RefillEnergy(int playerNumber, int amountToRefill);
        public delegate void PlayerStatsChanged(ShipBuilder shipBuilderThatChanged, ShipStats updatedShipStats);
        public delegate void PlayerBecomesDeath(ShipBuilder shipBuilderThatNeedsDying, int playerIndexOfKiller);
        public delegate void ZoneEntered(GameObject enteredZoneObject);
        public delegate void ManagerInitialized(Manager initializedManager);
        public delegate void ShipPartSelected(Part selectedPart, int playerNumber);
        public delegate void ShipCompleted(ShipBuilder completedShipBuilder);
        public delegate void PlayerJoined(int playerNumber, InputDevice joinedPlayerDevice);
        public delegate void PlayerTakeDamage(ShipBuilder hittedBuilder, int damageAmount, int indexOfPlayerThatShotTheBullet);
        public delegate void EveryPlayerReady(int playersInGameCount);
        public delegate void PlayerSpawned(GameObject spawnedShipBuilderObject, int playerNumber);
        /// <summary>
        ///     Called when firing the gun while not having enough ammo
        /// </summary>
        public delegate void EnergyEmpty();
        public delegate void PlayerHit();
        /// <summary>
        /// Gets called when the health of the player changes
        /// </summary>
        /// <param name="playerNumber">index of the player that got his health changed</param>
        /// <param name="newHealthPersentage">number between 0 and 1</param>
        public delegate void HealthChanged(int playerNumber, float newHealthPersentage);
        public delegate void FuelChanged();
        /// <summary>
        ///     Gets called when the stat gameobject of a player is set to enable
        /// </summary>
        /// <param name="statGameObjectNumber">The number of the enabled stat Game Object: 0 is Ship stats, 1 is Weapon stats</param>
        /// <param name="playerNumber">The index of the player that got its stat gameobject enabled</param>
        public delegate void EnabledStatGameObject(int statGameObjectNumber, int playerNumber);
        public delegate void WeaponFired(FMODUnity.EventReference weaponEventToPlay);
        public delegate void SelectedCategoryChanged(SelectableCollection currentSelectedCollection, int playerNumber);
        public delegate void NavigateUp();
        public delegate void NavigateDown();
        public delegate void GameOver();
        public delegate void ControllerShemeHidden();
        public delegate void ControllerShemeShowing();

        public delegate void PlayerHealed(int healthIncreaseAmount, int playerNumber);
        public delegate void PickupDestroyed();

        public static ChangeFireMode OnChangeFireMode;
        public static EnergyUsed OnEnergyUsed;
        public static EnergyChanged OnEnergyChanged;
        public static RefillEnergy OnRefillEnergy;
        public static PlayerStatsChanged OnPlayerStatsChanged;
        public static PlayerBecomesDeath OnPlayerBecomesDeath;
        public static ZoneEntered OnZoneEntered;
        public static ManagerInitialized OnManagerInitialized;
        public static ShipPartSelected OnShipPartSelected;
        public static ShipCompleted OnShipCompleted;
        public static PlayerJoined OnPlayerJoined;
        public static PlayerTakeDamage OnPlayerTakeDamage;
        public static EveryPlayerReady OnEveryPlayerReady;
        public static PlayerSpawned OnPlayerSpawned;
        public static EnergyEmpty OnEnergyEmpty;
        public static PlayerHit OnPlayerHit;
        public static HealthChanged OnHealthChanged;
        public static EnabledStatGameObject OnEnabledStatGameObject;
        public static WeaponFired OnWeaponFired;
        public static PlayerHealed OnPlayerHealed;
        public static SelectedCategoryChanged OnSelectedCategoryChanged;
        public static NavigateUp OnNavigateUp;
        public static NavigateDown OnNavigateDown;
        public static GameOver OnGameOver;
        public static PickupDestroyed OnPickupDestroyed;
        public static ControllerShemeShowing OnControllerShemeShowing;
        public static ControllerShemeHidden OnControllerShemeHidden;

    }

}