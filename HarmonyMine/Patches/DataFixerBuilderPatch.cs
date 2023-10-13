using com.mojang.datafixers;
using HarmonyLib;
using IKVM.Attributes;
using java.lang;
using java.util.concurrent;
using java.util.function;
using net.minecraft;
using org.apache.logging.log4j;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace HarmonyMine.Patches;

[HarmonyPatch(typeof(DataFixerBuilder))]
internal class DataFixerBuilderPatch {
    static Logger LOGGER = LogManager.getLogger();
    static Executor NOP_EXECUTOR = new NOPExecutor();

    //[HarmonyPrefix]
    //[HarmonyPatch(nameof(DataFixerBuilder.addFixer), typeof(DataFix))]
    //[MethodImpl(MethodImplOptions.NoInlining)]
    //public static bool addFixerPrefix(ref DataFixerBuilder __instance, DataFix fix) {
    //    LOGGER.info($"DataFix added: {fix}");
    //    return true;
    //}

    //[HarmonyPrefix]
    //[HarmonyPatch(nameof(DataFixerBuilder.addSchema), typeof(int), typeof(int), typeof(BiFunction))]
    //[MethodImpl(MethodImplOptions.NoInlining)]
    //public static bool addSchemaPrefix(ref DataFixerBuilder __instance, int version, int subVersion, BiFunction factory) {
    //    LOGGER.info($"Schema added ({version} {subVersion})");
    //    return true;
    //}

    [HarmonyReversePatch]
    [HarmonyPrefix]
    [HarmonyPatch(nameof(DataFixerBuilder.build), typeof(Executor))]
    [MethodImpl(MethodImplOptions.NoInlining)]
    static void buildPrefix(ref DataFixerBuilder __instance, ref DataFixer __result, ref Executor executor) {
        //LOGGER.info($"Type: {executor.GetType()}");
        executor = NOP_EXECUTOR;
        //__result = __instance.build(NOP_EXECUTOR);
        //return true;
    }
}

sealed class NOPExecutor : Executor {
    internal NOPExecutor() { }

    [HideFromJava]
    public void execute(Runnable A_1) { }
}