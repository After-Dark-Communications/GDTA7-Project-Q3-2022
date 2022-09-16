using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace DEBUG.Console
{
    public abstract class ConsoleCommand : ScriptableObject, IConsoleCommand
    {
        [SerializeField]
        private string commandWord = string.Empty;

        public string CommandWord => commandWord;

        public abstract bool Process(string[] args);
    }
}
