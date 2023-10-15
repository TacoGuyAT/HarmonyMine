using com.mojang.brigadier;
using HarmonyLib;
using HarmonyMine.API;
using net.minecraft.server.command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace HarmonyMine.Patches;

[HarmonyPatch(typeof(CommandManager))]
internal class CommandManagerPatch {
    [HarmonyTranspiler]
    [HarmonyPatch(MethodType.Constructor, new[] { typeof(CommandManager.RegistrationEnvironment) })]
    static IEnumerable<CodeInstruction> CctorTranspiler(IEnumerable<CodeInstruction> instructions) {
        var codes = new List<CodeInstruction>(instructions);

        var ldarg = codes[codes.Count - 16];
        var ldfld = codes[codes.Count - 15];

        codes.InsertRange(
            codes.Count - 19,
            new CodeInstruction[] {
                ldarg,
                ldfld,
                new CodeInstruction(OpCodes.Call, AccessTools.Method(typeof(CommandV0), "RegisterCommands", new [] { typeof(CommandDispatcher) }))
            }
        );

        return codes.AsEnumerable();
    }
}
