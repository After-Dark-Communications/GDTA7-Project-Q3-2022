using Assets.Scripts.ShipSelection;
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
    [SerializeField]
    private Transform joinPlayerScreensParent;

    List<Transform> joinPlayerScreens = new List<Transform>();

    private void Awake()
    {
        foreach (Transform transform in joinPlayerScreensParent)
        {
            joinPlayerScreens.Add(transform);
            transform.gameObject.SetActive(false);
        }

        joinPlayerScreens[0].gameObject.SetActive(true);
    }

    public void OnPlayerJoin(PlayerInput playerInput)
    {
        playerInput.gameObject.transform.SetParent(playerShipSelectionParent);

        playerInput.gameObject.transform.localScale = Vector3.one;
        
        ShowAndHideJoinPlayerButton(playerInput);
    }

    private void ShowAndHideJoinPlayerButton(PlayerInput playerInput)
    {
        joinPlayerScreens[playerInput.playerIndex].gameObject.SetActive(false);

        if (playerInput.playerIndex + 1 >= joinPlayerScreens.Count)
            return;

        joinPlayerScreens[playerInput.playerIndex + 1].gameObject.SetActive(true);
    }
}
