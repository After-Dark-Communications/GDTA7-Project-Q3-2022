using Managers;
using ShipParts;
using ShipParts.Ship;
using ShipSelection;
using System;
using UnityEngine;
using UnityEngine.InputSystem;
using Audio;
using static EventSystem.Channels;
using Data;

namespace EventSystem
{
    public static class Channels
    {
        public static InputChannel Input = new InputChannel();
        public static MovementChannel Movement = new MovementChannel();
        public static AnnouncerChannel Announcer = new AnnouncerChannel();
        public static KingOfTheHillChannel KingOfTheHill = new KingOfTheHillChannel();

        public delegate void ChangeFireMode(bool newFireModeValue, int playerNumber);
        public delegate void EnergyUsed(int playerNumber, int amount);
        public delegate void EnergyChanged(int playerNumber, float newEnergyPersentage);
        public delegate void RefillEnergy(int playerNumber, int amountToRefill);

        public delegate void DisplayableStatsChanged(int playerIndex, Part changedPart, ShipStats selectedShipStats, ShipStats hoveredShipStats);
        public delegate void PlayerBecomesDeath(ShipBuilder shipBuilderThatNeedsDying, int playerIndexOfKiller);
        public delegate void ZoneEntered(GameObject enteredZoneObject);
        public delegate void ManagerInitialized(Manager initializedManager);
        public delegate void ShipPartHovered(Part hoveredPart, int playerNumber);
        public delegate void ShipPartSelected(Part selectedPart, int playerNumber);
        public delegate void ShipCompleted(ShipBuilder completedShipBuilder);
        public delegate void PlayerJoined(int playerNumber, InputDevice joinedPlayerDevice);
        public delegate void PlayerTakeDamage(ShipBuilder hittedBuilder, int damageAmount, int indexOfPlayerThatShotTheBullet);
        public delegate void EveryPlayerReady(int playersInGameCount);
        public delegate void PlayerSpawned(GameObject spawnedShipBuilderObject, int playerNumber);
        public delegate void PlayerRespawned(int playerNumber);
        public delegate void PlayerDespawned(int playerNumber);
        public delegate void EnergyZoneMoved();
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
        public delegate void NavigateUp(int playerNumber);
        public delegate void NavigateDown(int playerNumber);
        public delegate void GameOver();
        public delegate void RoundOver(int roundIndex, int winnerIndex);
        public delegate void RoundStarted(int roundIndex, int numberOfRounds);
        public delegate void GameStart();
        public delegate void ControllerShemeHidden();
        public delegate void ControllerShemeShowing();
        public delegate void ReturnToTitleScreen();
        public delegate void LoadBuildingScene();
        public delegate void StartDeathMath();

        public delegate void PlayerHealed(int healthIncreaseAmount, int playerNumber);
        public delegate void PickupDestroyed();

        public delegate void ShipAnimationManagerLoaded();
        public delegate void CountdownDone();

        public delegate void QuitGame();
        public delegate void SpecialUsed(ShipBuilder shipBuilderThatUsedSpecial);
        public delegate void SpecialReady(ShipBuilder shipBuilder);
        public delegate void PlayerBarsLoaded(ShipBuilder shipBuilder);
        public delegate void AudioSettingsEvent(VolumeSettingsData data);
        public delegate void AudioSettingsChangedEvent(float value);

        public static ChangeFireMode OnChangeFireMode;
        public static EnergyUsed OnEnergyUsed;
        public static EnergyChanged OnEnergyChanged;
        public static RefillEnergy OnRefillEnergy;
        public static DisplayableStatsChanged OnDisplayabeStatsChanged;
        public static PlayerBecomesDeath OnPlayerBecomesDeath;
        public static ZoneEntered OnZoneEntered;
        public static ManagerInitialized OnManagerInitialized;
        public static ShipPartHovered OnShipPartHovered;
        public static ShipPartSelected OnShipPartSelected;
        public static ShipCompleted OnShipCompleted;
        public static PlayerJoined OnPlayerJoined;
        public static PlayerTakeDamage OnPlayerTakeDamage;
        public static EveryPlayerReady OnEveryPlayerReady;
        public static PlayerSpawned OnPlayerSpawned;
        public static PlayerRespawned OnPlayerRespawned;
        public static PlayerDespawned OnPlayerDespawned;
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
        public static RoundOver OnRoundOver;
        public static RoundStarted OnRoundStarted;
        public static PickupDestroyed OnPickupDestroyed;
        public static GameStart OnGameStart;
        public static ControllerShemeShowing OnControllerShemeShowing;
        public static ControllerShemeHidden OnControllerShemeHidden;
        public static ShipAnimationManagerLoaded OnShipAnimationManagerLoaded;
        public static CountdownDone OnCountdownDone;
        public static QuitGame OnQuitGame;
        public static EnergyZoneMoved OnEnergyZoneMoved;
        public static SpecialUsed OnSpecialUsed;
        public static PlayerBarsLoaded OnPlayerBarsLoaded;
        public static SpecialReady OnSpecialReady;
        public static ReturnToTitleScreen OnReturnToTitleScreen;
        public static LoadBuildingScene OnLoadBuildingScene;
        public static AudioSettingsEvent OnAudioSettingsSaved;
        public static AudioSettingsChangedEvent OnMasterValueChanged;
        public static AudioSettingsChangedEvent OnMusicChangedEvent;
        public static AudioSettingsChangedEvent OnSFXChangedEvent;
        public static AudioSettingsChangedEvent OnVoiceChangedEvent;
        public static StartDeathMath OnStartDeathMatch;
    }

}