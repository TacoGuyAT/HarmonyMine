using HarmonyMine.API;
using net.minecraft.server;
using Newtonsoft.Json;

[assembly: ModInfo("ExampleMod", "0.0.0.0", new string[] { "__tacoguy" })]

namespace ExampleMod;

public class ExampleMod : Mod {
    public override void OnInitialize() {
        this.HarmonyInstance.PatchAll();
        File.WriteAllText(DataPath + "test.json", JsonConvert.SerializeObject(Info));
    }
    public override void OnServerStarted(MinecraftServer server) {
        this.Logger.info($"Hello, cruel java world! Running on {server.getServerModName()} {server.getVersion()}.");
    }
}