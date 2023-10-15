using com.mojang.brigadier;
using com.mojang.brigadier.context;
using com.sun.org.apache.xml.@internal.security.transforms.implementations;
using IKVM.Attributes;
using java.lang;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HarmonyMine.Utils;

public class CommandLambda : Command {
    Func<CommandContext, bool> action { get; init; }

    public CommandLambda(Func<CommandContext, bool> action) {
        this.action = action;
    }

    [HideFromJava]
    public int run(CommandContext commandContext) => action.Invoke(commandContext) ? 1 : 0;
    public static implicit operator CommandLambda(Func<CommandContext, bool> action) => new CommandLambda(action);
}
