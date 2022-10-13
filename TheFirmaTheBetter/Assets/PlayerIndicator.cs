using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIndicator : MonoBehaviour
{
    [SerializeField]
    private Camera cam;

    void Update()
    {
        transform.LookAt(cam.transform.position);
    }
}
