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

// https://github.com/astei/lazydfu/
// https://github.com/ikvmnet/ikvm/issues/342
[HarmonyPatch(typeof(DataFixerBuilder))]
internal class DataFixerBuilderPatch {
    static Logger LOGGER = LogManager.getLogger();
    static Executor NOP_EXECUTOR = new NOPExecutor();

    [HarmonyPrefix]
    [HarmonyPatch(nameof(DataFixerBuilder.build), typeof(Executor))]
    static void buildPrefix(ref Executor executor) {
        executor = NOP_EXECUTOR;
    }
}

internal sealed class NOPExecutor : Executor {
    internal NOPExecutor() { }

    [HideFromJava]
    public void execute(Runnable A_1) { }
}