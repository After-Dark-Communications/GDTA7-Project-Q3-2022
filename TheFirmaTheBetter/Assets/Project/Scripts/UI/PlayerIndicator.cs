using ShipSelection;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace UI
{
    public class PlayerIndicator : MonoBehaviour
    {
        private Camera cam;

        private TMP_Text tmpTextField;

        private ShipInfo shipInfo;

        private void Awake()
        {
            shipInfo = GetComponentInParent<ShipInfo>();
            tmpTextField = GetComponentInChildren<TMP_Text>();
            cam = Camera.main;

            tmpTextField.SetText($"Player {shipInfo.PlayerNumber + 1}");
        }

        void Update()
        {
            transform.LookAt(transform.position + cam.transform.rotation * Vector3.forward,
                      cam.transform.rotation * Vector3.up);
        }
    }
}