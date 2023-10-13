using HarmonyLib;
using HarmonyMine.API;
using java.util;
using java.util.concurrent;
using org.apache.logging.log4j;
using System.Reflection;

namespace HarmonyMine;
internal class Program {
    static Logger LOGGER;
    static Harmony HarmonyInstance = new Harmony("HarmonyMine");
    static List<Mod> MODS = new();

    static void Main(string[] _args) {
        // https://github.com/ikvmnet/ikvm/issues/423
        new java.lang.Object(); 
        LOGGER = LogManager.getLogger();

        HarmonyInstance.PatchAll();

        AppDomain.CurrentDomain.AssemblyResolve += (s, e) => {
            var requestedAssembly = e.Name;
            Console.WriteLine(requestedAssembly);
            foreach(string filename in Directory.GetFiles("libs")) {
                Console.WriteLine(AssemblyName.GetAssemblyName(filename));
            }

            throw new NotImplementedException();
        };


        foreach(string filename in Directory.GetFiles("mods")) {
            if(filename.EndsWith(".dll")) {
                var assembly = Assembly.LoadFile(filename);
                var modInfo = assembly.GetCustomAttribute(typeof(ModInfoAttribute));
                if(modInfo == null) continue;
                var mods = assembly.GetExportedTypes().Where(t => t.IsSubclassOf(typeof(Mod)));
                if(mods.Count() <= 0) continue;
                var mod = mods.First();

                MODS.Add((Mod)mod.GetConstructor(new Type[] { typeof(ModInfoAttribute) })!.Invoke(new object[] { modInfo }));
            }
        }

        var args = new List<string>(_args);

        args.Add("--nogui");

        net.minecraft.server.Main.main(args.ToArray());
    }
}