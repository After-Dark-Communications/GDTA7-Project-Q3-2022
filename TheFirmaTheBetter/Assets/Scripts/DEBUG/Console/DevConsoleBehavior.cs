using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.InputSystem;

namespace DEBUG.Console
{
    public class DevConsoleBehavior : MonoBehaviour
    {
        [SerializeField]
        private ConsoleCommand[] commands = new ConsoleCommand[0];

        private static DevConsoleBehavior _Instance;

        private DevConsole devConsole;
        private DevConsole _DevConsole
        {
            get
            {
                if (devConsole != null) { return devConsole; }
                return devConsole = new DevConsole(commands);
            }
        }

        private bool _ShowConsole;
        private string _Input;
        private float _PausedTimeScale;

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

        private void OnGUI()
        {
            if (!_ShowConsole) { return; }
            float y = 0f;

            GUI.Box(new Rect(0, y, Screen.width, 30), "");
            GUI.backgroundColor = Color.black;
            _Input = GUI.TextField(new Rect(10f, y + 5f, Screen.width - 20f, 20f), _Input);
        }

        public void ToggleConsole(InputAction.CallbackContext ctx)
        {
            if (ctx.action.triggered)
            {
                _ShowConsole = !_ShowConsole;
                if (_ShowConsole == true)
                {
                    _PausedTimeScale = Time.timeScale;
                    Time.timeScale = 0;
                }
                else
                {
                    Time.timeScale = _PausedTimeScale;
                }
            }
        }

        public void ProcessCommand(InputAction.CallbackContext ctx)
        {
            if (ctx.action.triggered)
            {
                _DevConsole.ProcessCommand(_Input);
                _Input = string.Empty;
            }
        }

    }
}
