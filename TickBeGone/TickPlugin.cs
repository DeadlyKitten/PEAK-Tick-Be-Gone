using BepInEx;
using HarmonyLib;

namespace TickBeGone
{
    [BepInPlugin("com.steven.peak.tickbegone", "Tick-Be-Gone", "1.0.0")]
    public class TickPlugin : BaseUnityPlugin
    {
        private void Awake()
        {
            var harmony = new Harmony("com.steven.peak.tickbegone");
            harmony.PatchAll();

            var bugfixer = gameObject.AddComponent<Bugfixer>();
            bugfixer.useLocalCharacter = true;
        }
    }
}
