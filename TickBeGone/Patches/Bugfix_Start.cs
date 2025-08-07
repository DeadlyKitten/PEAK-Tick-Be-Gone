using HarmonyLib;
using UnityEngine;

namespace TickBeGone.Patches
{
    [HarmonyPatch(typeof(Bugfix), "Start")]
    internal class Bugfix_Start
    {
        private static void Postfix(Bugfix __instance)
        {
            var collider = __instance.GetComponent<SphereCollider>();
            var tickMesh = __instance.GetComponentInChildren<MeshRenderer>();

            if (BugUtilities.TryCensorBug(__instance.transform, collider.center, Vector3.one * collider.radius * 2f, tickMesh.sharedMaterial))
                tickMesh.gameObject.SetActive(false);
            else
                BugUtilities.CensorOldBug(tickMesh);

            if (Configuration.UseIconOverride)
                __instance.bugItem.UIData.icon = TickPlugin.GetRandomCat(true);
        }
    }
}