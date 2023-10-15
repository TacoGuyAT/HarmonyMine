using com.mojang.brigadier;
using com.mojang.brigadier.arguments;
using com.mojang.brigadier.builder;
using com.mojang.brigadier.context;
using HarmonyMine.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HarmonyMine.Utils;
public static class Extensions {
    public static void Register(this CommandDispatcher dispatcher, ArgumentBuilder builder) => dispatcher.register((LiteralArgumentBuilder)builder);
    public static ArgumentBuilder Executes(this ArgumentBuilder builder, Func<CommandContext, bool> lambda) => builder.executes((CommandLambda)lambda);
}

