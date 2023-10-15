using com.sun.tools.corba.se.idl;
using HarmonyLib;
using HarmonyMine.API;
using java.util;
using java.util.concurrent;
using org.apache.logging.log4j;
using org.apache.logging.log4j.core.filter;
using System.Reflection;

namespace HarmonyMine;
internal class Program {
    static Logger Logger;
    static Harmony HarmonyInstance = new Harmony("HarmonyMine");

    static void Main(string[] args) {
        {
            // https://github.com/ikvmnet/ikvm/issues/423
            new java.lang.Object(); 
        }
        Logger = LogManager.getLogger();

        Directory.CreateDirectory("libs");
        AppDomain.CurrentDomain.AssemblyResolve += (s, e) => {
            Directory.CreateDirectory("libs");
            foreach(string filename in Directory.GetFiles("libs", "*.dll", SearchOption.AllDirectories))
                if(e.Name == AssemblyName.GetAssemblyName(filename).FullName)
                    return Assembly.LoadFile(Path.GetFullPath(filename));

            foreach(string filename in Directory.GetFiles("mods", "*.dll", SearchOption.TopDirectoryOnly))
                if(e.Name == AssemblyName.GetAssemblyName(filename).FullName) {
                    var assembly = Assembly.LoadFile(Path.GetFullPath(filename));
                    Mod.TryRegister(assembly);
                    return assembly;
                }

            return null;
        };

        Mod.RegisterAssemblyCommands(Assembly.GetExecutingAssembly());

        Directory.CreateDirectory("mods/data");
        foreach(string filename in Directory.GetFiles("mods", "*.dll", SearchOption.TopDirectoryOnly))
            Mod.TryRegister(Assembly.LoadFile(Path.GetFullPath(filename)));

        // https://github.com/ikvmnet/ikvm/issues/298
        Array.Resize(ref args, args.Length + 1);
        args[args.Length - 1] = "-nogui";

        HarmonyInstance.PatchAll();

        net.minecraft.server.Main.main(args);
    }
}