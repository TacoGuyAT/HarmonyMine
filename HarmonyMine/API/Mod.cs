using HarmonyLib;
using net.minecraft.server;
using org.apache.logging.log4j;
using System.Collections.ObjectModel;
using System.Reflection;

namespace HarmonyMine.API;

public abstract class Mod {
    public static ReadOnlyCollection<Mod> Mods => _mods.AsReadOnly();
    internal static List<Mod> _mods = new();

    public ModInfoAttribute Info { get; internal set; }
    protected internal Logger Logger = LogManager.getLogger();
    protected internal Harmony HarmonyInstance { get; internal set; }
    public string DataPath { get; internal set; }
    internal Assembly _assembly { get; set; }
    /// <summary>
    /// Constructor is ran before mod has started to initialize. You might want to use <see cref="OnPostInitialize">OnInitialize()</see> instead.
    /// </summary>
    public Mod() { }

    internal static bool TryRegister(Assembly assembly) {
        if(assembly.GetCustomAttribute(typeof(ModInfoAttribute)) is ModInfoAttribute modInfo) {
            var mods = assembly.GetExportedTypes().Where(t => t.IsSubclassOf(typeof(Mod)));
            if(mods.Count() <= 0) return false;

            Mod mod = (Mod)Activator.CreateInstance(mods.First())!;
            mod.Info = modInfo;
            mod.HarmonyInstance = new Harmony(modInfo.ModID);
            mod.DataPath = Path.Combine(Directory.GetParent(assembly.Location)!.FullName, "data", modInfo.ModID) + Path.DirectorySeparatorChar;
            mod._assembly = assembly;

            Directory.CreateDirectory(mod.DataPath);

            OnPostInitializeEvent += mod.OnPostInitialize;
            OnServerStartedEvent += mod.OnServerStarted;

            RegisterAssemblyCommands(assembly);

            mod.OnInitialize();

            _mods.Add(mod);
            return true;
        }
        return false;
    }

    internal static void RegisterAssemblyCommands(Assembly assembly) {
        var commands = assembly.GetExportedTypes().Where(t => t.IsSubclassOf(typeof(CommandV0)) && !t.IsAbstract);
        foreach(var command in commands) {
            CommandV0._commands.Add((CommandV0)Activator.CreateInstance(command)!);
        }
    }

    /// <summary>
    /// Called after base class is created and ready for mod initialization code
    /// </summary>
    public virtual void OnInitialize() { }
    /// <summary>
    /// Called after each mod has initialized, but before server started launching
    /// </summary>
    public virtual void OnPostInitialize() { }
    public delegate void OnPostInitializeDelegate();
    internal static event OnPostInitializeDelegate? OnPostInitializeEvent;
    internal static void OnPostInitializeCallback() {
        if(OnPostInitializeEvent != null)
            OnPostInitializeEvent.Invoke(); 
    }

    public virtual void OnServerStarted(MinecraftServer server) { }
    public delegate void OnServerStartedDelegate(MinecraftServer server);
    internal static event OnServerStartedDelegate? OnServerStartedEvent;
    internal static void OnServerStartedCallback(MinecraftServer server) { 
        if(OnServerStartedEvent != null) 
            OnServerStartedEvent.Invoke(server); 
    }
}
