using HarmonyLib;

namespace TickBeGone.Patches
{
    [HarmonyPatch(typeof(Bugfix), nameof(Bugfix.AttachBug))]
    internal class Bugfix_AttachBug
    {
#if DEBUG
        private static void Postfix(ref float ___lifeTime)
        {
            ___lifeTime = 250f;
        }
#endif
    }
}
