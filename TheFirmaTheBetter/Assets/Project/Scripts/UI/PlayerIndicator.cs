using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UI
{
    public class PlayerIndicator : MonoBehaviour
    {
        [SerializeField]
        private Camera cam;

        void Update()
        {
            if (cam == null)
                return ;
            transform.LookAt(cam.transform.position);
        }
    }
}