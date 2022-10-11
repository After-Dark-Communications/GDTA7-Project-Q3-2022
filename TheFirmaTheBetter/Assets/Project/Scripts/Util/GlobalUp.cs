using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Util
{
    public class GlobalUp : MonoBehaviour
    {
        [SerializeField, Tooltip("The transform to use the rotation as up for")]
        private Transform GlobalUpwards;

        private static GlobalUp _Instance;

        private void Awake()
        {
            if (_Instance != null && _Instance != this)
            {
                Destroy(gameObject);
                return;
            }
            _Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        public static Transform UP => _Instance.GlobalUpwards;
    }
}