using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace UI
{
    public class FPSCounter : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _fpsText;
        [SerializeField] private float _hudRefreshRate = 1f;

        private float _timer;

        private void Awake()
        {
#if !DEVELOPMENT_BUILD && !UNITY_EDITOR
            Destroy(_fpsText.gameObject);
            Destroy(this);
#endif
        }
        private void Update()
        {
            if (Time.unscaledTime > _timer)
            {
                int fps = (int)(1f / Time.unscaledDeltaTime);
                _fpsText.text = "<i>FPS:</i> " + fps;
                _timer = Time.unscaledTime + _hudRefreshRate;
            }
        }
    }
}