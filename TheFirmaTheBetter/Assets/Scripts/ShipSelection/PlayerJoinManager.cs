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

    private Vector2 player1Position = Vector2.left + Vector2.up;
    private Vector2 player2Position = Vector2.right + Vector2.up;
    private Vector2 player3Position = Vector2.right + Vector2.down;
    private Vector2 player4Position = Vector2.left + Vector2.down;

    public void OnPlayerJoin(PlayerInput playerInput)
    {
        playerInput.gameObject.transform.SetParent(playerShipSelectionParent);

        SetAnchorPoint(playerInput);
    }

    private void SetAnchorPoint(PlayerInput playerInput)
    {
        RectTransform rectTransform = playerInput.transform.GetComponent<RectTransform>();

        switch (playerInput.playerIndex)
        {
            case 1:
                rectTransform.anchorMax = player1Position;
                rectTransform.anchorMin = player1Position;
                rectTransform.pivot = player1Position;
                break;
            case 2:
                rectTransform.anchorMax = player2Position;
                rectTransform.anchorMin = player2Position;
                rectTransform.pivot = player2Position;
                break;
            case 3:
                rectTransform.anchorMax = player3Position;
                rectTransform.anchorMin = player3Position;
                rectTransform.pivot = player3Position;
                break;
            case 4:
                rectTransform.anchorMax = player4Position;
                rectTransform.anchorMin = player4Position;
                rectTransform.pivot = player4Position;
                break;
            default:
                break;
        }

        rectTransform.ForceUpdateRectTransforms();
    }
}
