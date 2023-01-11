using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
//[CreateAssetMenu(fileName = "New Log Command", menuName = "Console Commands/Log Command")]
    public class LogCommand : ConsoleCommand
    {
        public override bool Process(string[] args)
        {
            string logText = string.Join(' ', args);
            Debug.Log(logText);
            return true;
        }
    }
