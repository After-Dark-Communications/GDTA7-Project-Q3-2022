using Cinemachine;
using EventSystem;
using ShipParts.Ship;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace ShipSelection
{
    public class ShipSelectionCamera : MonoBehaviour
    {
        [SerializeField]
        private Transform previewCameraParent;
        [SerializeField]
        private CinemachineVirtualCamera coreCamera;
        [SerializeField]
        private CinemachineVirtualCamera engineCamera;
        [SerializeField]
        private CinemachineVirtualCamera weaponCamera;
        [SerializeField]
        private CinemachineVirtualCamera specialCamera;
        [SerializeField]
        private ShipBuilder shipBuilder;


        private void OnEnable()
        {
            Channels.OnSelectedCategoryChanged += OnCategoryChanged;
            Channels.OnShipCompleted += ShipReady;
        }
        private void OnDisable()
        {
            Channels.OnSelectedCategoryChanged -= OnCategoryChanged;
            Channels.OnShipCompleted -= ShipReady;
        }

        private void OnCategoryChanged(SelectableCollection currentSelectedCollection, int playerNumber)
        {
            if (playerNumber != shipBuilder.PlayerNumber)
            { return; }
            switch (currentSelectedCollection.CategoryName)
            {
                case "Core":
                    SetCameras(core: true);
                    break;
                case "Engine":
                    SetCameras(engine: true);
                    break;
                case "Weapon":
                    SetCameras(weapon: true);
                    break;
                case "Special":
                    SetCameras(special: true);
                    break;
                default:
                    break;
            }
        }

        private void ShipReady(ShipBuilder completedShipBuilder)
        {//disable all cams but the preview camera
            if (completedShipBuilder.PlayerNumber != shipBuilder.PlayerNumber)
            { return; }
            SetCameras(preview: true);
        }

        private void SetCameras(bool preview = false, bool core = false, bool engine = false, bool weapon = false, bool special = false)
        {
            previewCameraParent.gameObject.SetActive(preview);
            coreCamera.enabled = core;
            engineCamera.enabled = engine;
            weaponCamera.enabled = weapon;
            specialCamera.enabled = special;
        }

        public Transform PreviewCameraParent { get => previewCameraParent; }
        public CinemachineVirtualCamera CoreCamera { get => coreCamera; }
        public CinemachineVirtualCamera EngineCamera { get => engineCamera; }
        public CinemachineVirtualCamera WeaponCamera { get => weaponCamera; }
        public CinemachineVirtualCamera SpecialCamera { get => specialCamera; }
    }
}