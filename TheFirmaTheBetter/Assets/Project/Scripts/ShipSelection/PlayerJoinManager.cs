using EventSystem;
using Managers;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace ShipSelection
{
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
            ShipBuilderInput.EnableAllInputsHard();
        }

        private void OnDisable()
        {
            Channels.OnManagerInitialized -= OnManagerInitialized;
        }

        private void OnManagerInitialized(Manager manager)
        {
            if (manager.GetType() != typeof(CamPreviewManager))
                return;

            previewManager = manager as CamPreviewManager;
        }

#if UNITY_EDITOR
        public void OnPlayerJoin(PlayerInput playerInput, bool isDebug = true)
        {
            OnPlayerJoin(playerInput);
            Channels.Input.OnShipCompletedInput(playerInput.playerIndex);
        }
#endif

        public void OnPlayerJoin(PlayerInput playerInput)
        {
            InputDevice playerGamepad = playerInput.GetDevice<InputDevice>();

            playerInput.gameObject.transform.SetParent(playerShipSelectionParent);

            playerInput.gameObject.transform.localScale = Vector3.one;
            playerInput.gameObject.transform.localPosition = Vector3.zero;
            PlayerSelectionScreen playerSelectionScreen = playerInput.gameObject.GetComponent<PlayerSelectionScreen>();
            int playerNumber = playerInput.playerIndex;
            playerSelectionScreen.PlayerNumber = playerNumber;


            playerSelectionScreen.GetComponentInChildren<ShipStatsManager>().PlayerIndex = playerNumber;

            ShowAndHideJoinPlayerButton(playerInput);

            ShowAndHideJoinPlayerButton(playerInput);

            SetCamPreview(playerInput);

            Channels.OnPlayerJoined?.Invoke(playerSelectionScreen.PlayerNumber, playerGamepad);
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

        public GameObject PrefabPlayerShipSelection => prefabPlayerShipSelection; 
    }
}