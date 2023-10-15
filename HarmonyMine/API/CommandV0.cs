using com.mojang.brigadier;
using com.mojang.brigadier.arguments;
using com.mojang.brigadier.builder;
using System.Reflection;

namespace HarmonyMine.API;
public abstract class CommandV0 {
    internal static List<CommandV0> _commands = new();
    public static void RegisterCommands(CommandDispatcher dispatcher) {
        foreach(var command in CommandV0._commands)
            command.Register(dispatcher);
    }
    public static LiteralArgumentBuilder Literal(string name) => LiteralArgumentBuilder.literal(name);
    public static RequiredArgumentBuilder Argument(string name, ArgumentType type) => RequiredArgumentBuilder.argument(name, type);
    public abstract void Register(CommandDispatcher dispatcher);
}

