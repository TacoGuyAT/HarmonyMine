using com.sun.org.apache.xpath.@internal;
using HarmonyLib;
using java.util;
using net.minecraft.server.dedicated;
using net.minecraft.util.registry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace HarmonyMine.Patches;

[HarmonyPatch(typeof(ServerPropertiesHandler))]
internal class ServerPropertiesHandlerPatch {
    [HarmonyTranspiler]
    [HarmonyPatch(MethodType.Constructor, new[] { typeof(Properties), typeof(DynamicRegistryManager) })]
    static IEnumerable<CodeInstruction> CctorTranspiler(IEnumerable<CodeInstruction> instructions) {
        var codes = new List<CodeInstruction>(instructions);
        codes[56] = new CodeInstruction(OpCodes.Ldstr, $"An auto-ported Minecraft server");
        return codes;
    }
}
