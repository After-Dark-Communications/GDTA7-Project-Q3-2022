using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

    public interface IConsoleCommand
    {
        string CommandWord { get; }
        bool Process(string[] args);
    }
