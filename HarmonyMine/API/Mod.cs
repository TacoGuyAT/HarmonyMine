using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HarmonyMine.API;

public abstract class Mod {
    protected Harmony HarmonyInstance { get; init; }
    public static T Create<T>(ModInfoAttribute modInfo) where T : Mod, new() {
        var mod = new T { HarmonyInstance = new Harmony(modInfo.ModID) };
        return new T();
    }
}
