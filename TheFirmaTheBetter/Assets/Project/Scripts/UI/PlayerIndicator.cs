using EventSystem;
using ShipSelection;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class PlayerIndicator : MonoBehaviour
    {
        private Camera cam;
        private TMP_Text tmpTextField;
        private ShipInfo shipInfo;

        [SerializeField] private Image border;

        private void Awake()
        {
            shipInfo = GetComponentInParent<ShipInfo>();
            tmpTextField = GetComponentInChildren<TMP_Text>();
            cam = Camera.main;

            Channels.OnControllerShemeHidden += ShowIndicator;

            switch (shipInfo.PlayerNumber)
            {
                case 0:
                    border.color = new Color(1f, 0.247f, 0.2f);
                    break;
                case 1:
                    border.color = Color.green;
                    break;
                case 2:
                    border.color = new Color(1f, 0.968f, 0.2f);
                    break;
                default:
                    border.color = new Color(0f, 1f, 1f);
                    break;
            }

            tmpTextField.SetText($"Player {shipInfo.PlayerNumber + 1}");
        }

        private void OnDestroy()
        {
            Channels.OnControllerShemeHidden -= ShowIndicator;
        }

        private void ShowIndicator()
        {
            Animator animator = GetComponent<Animator>();
            animator.Play(0);
        }

        void Update()
        {
            //transform.LookAt(cam.transform.position);
            // transform.rotation = new Quaternion(transform.rotation.x,0, transform.rotation.z, transform.rotation.w);
            //Got this code below from https://answers.unity.com/questions/1443818/billboard-script-that-only-rotates-horizontally.html
            transform.LookAt(transform.position + cam.transform.rotation * Vector3.forward,
                      cam.transform.rotation * Vector3.up);
            //Vector3 eulerAngles = transform.eulerAngles;
            //eulerAngles.z = 0;
            //transform.eulerAngles = eulerAngles;
        }
    }
}