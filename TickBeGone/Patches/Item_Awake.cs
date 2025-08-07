using HarmonyLib;
using UnityEngine;

namespace TickBeGone.Patches
{
    [HarmonyPatch(typeof(Item), "Awake")]
    internal class Item_Awake
    {
        private static void Prefix(Item __instance)
        {
            if (__instance.itemID != 95) return;

            var bug = __instance.transform.Find("Bugfix").gameObject;
            var bugRenderer = bug.GetComponent<MeshRenderer>();
            var bugMaterial = bugRenderer.sharedMaterial;

            if (BugUtilities.TryCensorBug(__instance.transform, new Vector3(0, 0.047f, -0.074f), Vector3.one * 0.21511f, bugMaterial))
                bugRenderer.enabled = false;
            else
                BugUtilities.CensorOldBug(bugRenderer);

            if (Configuration.UseIconOverride)
                __instance.UIData.icon = TickPlugin.GetRandomCat(true);
        }
    }
}