using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class ShipBuildManager : Manager
{
    #region Singleton
    public static ShipBuildManager Instance;

    void Awake()
    {
        if (Instance == null)
        {
            DontDestroyOnLoad(gameObject);
            Instance = this;
        }

        if (Instance != this)
        {
            Destroy(gameObject);
        }

        SceneManager.sceneLoaded += OnSceneLoaded;
        Channels.OnShipCompleted += OnShipCompleted;
        Channels.OnPlayerJoined += OnPlayerJoined;
    }
    #endregion

    private List<ShipBuilder> shipBuilders = new List<ShipBuilder>();

    private int amountOfPlayersJoined = 0;

    public override void Start() { }

    private void OnSceneLoaded(Scene arg0, LoadSceneMode arg1)
    {
        Channels.OnManagerInitialized.Invoke(this);
    }

    private void OnShipCompleted(ShipBuilder shipBuilder)
    {
        int index = GetIndexOfShipBuilderInList(shipBuilder);

        if (index != -1)
            shipBuilders.RemoveAt(index);

        shipBuilders.Add(shipBuilder);

        if (shipBuilders.Count == amountOfPlayersJoined)
            Channels.OnEveryPlayerReady.Invoke();
    }



    private int GetIndexOfShipBuilderInList(ShipBuilder shipBuilder)
    {
        return shipBuilders.FindIndex(sb => sb.PlayerNumber == shipBuilder.PlayerNumber);
    }

    private void OnPlayerJoined(int playerNumber, InputDevice inputDevice)
    {
        amountOfPlayersJoined = playerNumber + 1;
    }

    public List<ShipBuilder> ShipBuilders => shipBuilders;

    public int AmountOfPlayersJoined => amountOfPlayersJoined; 
}
