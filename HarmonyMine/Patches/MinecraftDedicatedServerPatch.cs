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

[HarmonyPatch(typeof(MinecraftDedicatedServer))]
internal class MinecraftDedicatedServerPatch {
    [HarmonyPostfix]
    [HarmonyPatch(nameof(MinecraftDedicatedServer.setupServer))]
    static void setupServerPostfix(ref MinecraftServer __instance) {
        Mod.OnServerStartedCallback(__instance);
    }
}
