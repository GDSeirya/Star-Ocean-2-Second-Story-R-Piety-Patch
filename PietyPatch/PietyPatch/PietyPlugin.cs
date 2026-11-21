using BepInEx;
using BepInEx.Unity.IL2CPP;
using BepInEx.Logging;
using HarmonyLib;
using Game;

namespace PietyCrit
{
    [BepInPlugin(Guid, Name, Version)]
    public class Plugin : BasePlugin
    {
        public const string Guid = "com.gd.pietycrit";
        public const string Name = "Piety Crit";
        public const string Version = "1.0.0";

        public static ManualLogSource StaticLog;

        public override void Load()
        {
            StaticLog = Log;

            new Harmony(Guid).PatchAll(typeof(PietyPatch));
            StaticLog.LogInfo($"{Name} loaded {Version}");
        }
    }

    /*
     * IncreaseBaseStatus
     * Interrupt stat increase when it comes from piety.
     * IncreaseBaseStatus only gets called by the Piety function,
     * which means that when you level piety, this triggers, and nothing else
     */
    [HarmonyPatch(typeof(CharacterParameter), "IncreaseBaseStatus")]
    internal static class PietyPatch
    {
        static void Prefix(ref CharacterStatus status, int value)
        {
            status = CharacterStatus.CRITICAL;
            //Plugin.StaticLog.LogInfo($"Piety stat gain redirected to CRITICAL (+{value})");
        }
    }
}
