using HarmonyLib;
using HarmonyMine.API;
using net.minecraft.server;
using net.minecraft.server.dedicated;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HarmonyMine.Patches;

[HarmonyPatch(typeof(MinecraftServer))]
internal class MinecraftServerPatch {
    [HarmonyPrefix]
    [HarmonyPatch(nameof(MinecraftServer.getServerModName))]
    static bool getServerModNamePrefix(ref string __result) {
        __result = "HarmonyMine";
        return false;
    }
}
