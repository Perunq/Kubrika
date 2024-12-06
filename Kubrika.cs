using BepInEx;
using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kubrika
{
    internal static class ModInfo
    {
        internal const string Guid = "perunq.elinplugins.kubrika";
        internal const string Name = "Kubrika";
        internal const string Version = "1.0";
    }
    [BepInPlugin(ModInfo.Guid, ModInfo.Name, ModInfo.Version)]
    public class Kubrika : BaseUnityPlugin
    {
        internal static Kubrika Instance { get; private set; }

        private void Awake()
        {
            Instance = this;

            Log("My mod...loaded?!");
            var harmony = new Harmony("perunq.elinplugins.kubrika");
            harmony.PatchAll();

        }

        public static void Log(object payload)
        {
            Instance.Logger.LogInfo(payload);
        }
    }
}
