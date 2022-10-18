using EventSystem;
using ShipSelection;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

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
