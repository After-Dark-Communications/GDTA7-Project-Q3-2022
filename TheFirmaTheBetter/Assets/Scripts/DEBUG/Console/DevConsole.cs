using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace DEBUG.Console
{
    public class DevConsole
    {
        private readonly IEnumerable<IConsoleCommand> commands;
        public DevConsole(IEnumerable<IConsoleCommand> commands)
        {
            this.commands = commands;
        }

        /// <summary>Takes the given command string and splits into the command and args, then processes.</summary>
        public void ProcessCommand(string InputValue)
        {
            if (string.IsNullOrWhiteSpace(InputValue)) { return; }

            string[] inputSplit = System.Text.RegularExpressions.Regex.Split(InputValue, "(?<=^[^\"]*(?:\"[^\"]*\"[^\"]*)*) (?=(?:[^\"]*\"[^\"]*\")*[^\"]*$)"); ;

            string commandInput = inputSplit[0];
            string[] args = inputSplit.Skip(1).ToArray();

            ProcessCommand(commandInput, args);
        }
        /// <summary>Processes the given command with args</summary>
        public void ProcessCommand(string CommandInput, string[] args)
        {
            foreach (IConsoleCommand command in commands)
            {
                if (!CommandInput.Equals(command.CommandWord, StringComparison.OrdinalIgnoreCase))
                {
                    continue;
                }

                if (command.Process(args))
                {
                    return;
                }
            }
        }
    }

}