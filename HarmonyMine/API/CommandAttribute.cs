using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HarmonyMine.API {
    public class CommandAttribute : Attribute {
        public string Command { get; init; }
        public CommandAttribute(string command) {
            Command = command;
        }
    }
    public class CommandUsageAttribute : Attribute {
        public string Usage { get; init; }
        public CommandUsageAttribute(string usage) {
            Usage = usage;
        }
    }
}
