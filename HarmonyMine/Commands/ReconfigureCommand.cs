using com.mojang.brigadier;
using com.mojang.brigadier.arguments;
using com.mojang.brigadier.builder;
using com.mojang.brigadier.context;
using HarmonyMine.API;
using HarmonyMine.Utils;
using net.minecraft.command.argument;
using net.minecraft.server.command;
using net.minecraft.server.network;
using net.minecraft.text;

namespace HarmonyMine.Commands;
public class ReconfigureCommand : CommandV0 {
    public override void Register(CommandDispatcher dispatcher) {
        dispatcher.Register(Literal("reconfigure")
            .then(Argument("mod", TextArgumentType.text())
                .Executes(ctx => Reload(ctx, TextArgumentType.getTextArgument(ctx, "mod"))))
            .Executes(ctx => Reload(ctx))
        );
    }

    // TODO: properly check command source
    // TODO: implement configs
    public bool Reload(CommandContext ctx, Text? modName = null) {
        var source = (ServerCommandSource)ctx.getSource();

        if(modName == null) {   // reload all mods
            source.sendFeedback(new TranslatableText("commands.reload.success.multiple", new object[] { Mod.Mods.Count }), true);
            return true;
        } else if(false) {     // check if this mod exists and reload it
            Mod? mod;
            source.sendFeedback(new TranslatableText("commands.reload.success.single", new object[] { mod.Info.ModID }), true);
            return true;
        }

        source.sendFeedback(new TranslatableText("commands.reload.invalidMod", new object[] { Mod.Mods.Count }), true);
        return false;
    }
}
