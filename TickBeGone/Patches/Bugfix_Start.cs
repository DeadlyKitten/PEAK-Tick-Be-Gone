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

            var sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            sphere.transform.parent = __instance.transform;
            sphere.transform.localPosition = collider.center;
            sphere.transform.localScale = Vector3.one * collider.radius * 2f;
            sphere.GetComponent<MeshRenderer>().sharedMaterial = tickMesh.sharedMaterial;

            tickMesh.gameObject.SetActive(false);
        }
    }
}
