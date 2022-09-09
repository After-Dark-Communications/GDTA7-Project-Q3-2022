using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class PlayerJoinManager : MonoBehaviour
{
    [SerializeField]
    private GameObject prefabPlayerShipSelection;
    [SerializeField]
    private Transform playerShipSelectionParent;
    [SerializeField]
    private PlayerInputManager playerInputManager;

    public void OnPlayerJoin(PlayerInput playerInput)
    {
        //Object.Instantiate(prefabPlayerShipSelection, playerShipSelectionParent);
        playerInput.gameObject.transform.SetParent(playerShipSelectionParent);
        
    }
}
