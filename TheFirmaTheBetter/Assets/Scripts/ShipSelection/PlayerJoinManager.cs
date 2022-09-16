using Assets.Scripts.ShipSelection;
using System;
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

    private CamPreviewManager previewManager;

    private List<Transform> joinPlayerScreens = new List<Transform>();

    private void Awake()
    {
        Channels.OnManagerInitialized += OnManagerInitialized;

        foreach (Transform transform in joinPlayerScreensParent)
        {
            joinPlayerScreens.Add(transform);
            transform.gameObject.SetActive(false);
        }

        joinPlayerScreens[0].gameObject.SetActive(true);
    }

    private void OnManagerInitialized(Manager manager)
    {
        if (manager.GetType() != typeof(CamPreviewManager))
            return;

        previewManager = manager as CamPreviewManager;
    }

    public void OnPlayerJoin(PlayerInput playerInput)
    {
        playerInput.gameObject.transform.SetParent(playerShipSelectionParent);

        playerInput.gameObject.transform.localScale = Vector3.one;

        PlayerSelectionScreen playerSelectionScreen = playerInput.gameObject.GetComponent<PlayerSelectionScreen>();
        playerSelectionScreen.PlayerNumber = playerInput.playerIndex;

        ShowAndHideJoinPlayerButton(playerInput);

        SetCamPreview(playerInput);
    }

    private void SetCamPreview(PlayerInput playerInput)
    {
        previewManager.SetCamOutputToPlayerRenderingTexture(playerInput.playerIndex);
        CamPreviewSetter setter = playerInput.gameObject.GetComponentInChildren<CamPreviewSetter>();
        setter.RawImage.texture = previewManager.GetRenderTextureByPlayerIndex(playerInput.playerIndex);
    }

    private void ShowAndHideJoinPlayerButton(PlayerInput playerInput)
    {
        joinPlayerScreens[playerInput.playerIndex].gameObject.SetActive(false);

        if (playerInput.playerIndex + 1 >= joinPlayerScreens.Count)
            return;

        joinPlayerScreens[playerInput.playerIndex + 1].gameObject.SetActive(true);
    }
}