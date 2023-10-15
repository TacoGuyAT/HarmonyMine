using com.sun.tools.corba.se.idl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HarmonyMine.API {
    public class ModInfoAttribute : Attribute {
        public string ModID;
        public string Version = "0.0.0.0";
        public string[] Authors = new string[] { };
        public string? URL;
        public ModInfoAttribute(string modid, string version = "0.0.0.0") {
            ModID = modid;
            Version = version;
        }
        public ModInfoAttribute(string modid, string version, string[] authors) {
            ModID = modid;
            Version = version;
            Authors = authors;
        }
    }
}
