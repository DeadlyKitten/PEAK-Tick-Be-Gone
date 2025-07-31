using HarmonyLib;
using UnityEngine;

namespace TickBeGone.Patches
{
    [HarmonyPatch(typeof(Item), "Start")]
    internal class Item_Start
    {
        private static void Postfix(Item __instance)
        {
            if (__instance.itemID != 95) return;

            var bug = __instance.transform.Find("Bugfix").gameObject;
            var bugMaterial = bug.GetComponent<MeshRenderer>().sharedMaterial;

            var sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            sphere.transform.parent = __instance.transform;
            sphere.transform.localPosition = new Vector3(0, 0.047f, -0.074f);
            sphere.transform.localScale = Vector3.one * 0.21511f;
            sphere.GetComponent<MeshRenderer>().sharedMaterial = bugMaterial;

            bug.SetActive(false);
        }
    }
}
