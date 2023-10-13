using HarmonyMine.API;
using System.Reflection;

[assembly: ModInfo("title")]

namespace ExampleMod;

public class ExampleMod : Mod {
    public void test() {
        var mod = ExampleMod.Create<ExampleMod>(new ModInfoAttribute("modid"));
        HarmonyInstance.PatchAll();
    }
}