using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HarmonyMine.API {
    public class ModInfoAttribute : Attribute {
        public string ModID;
        public ModInfoAttribute(string modid) {
            ModID = modid;
        }
    }   
}
