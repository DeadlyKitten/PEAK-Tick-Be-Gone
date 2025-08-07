using System;
using System.Text.RegularExpressions;
using HarmonyLib;

namespace TickBeGone.Patches
{
    [HarmonyPatch(typeof(LocalizedText), nameof(LocalizedText.GetText), new Type[] { typeof(string), typeof(bool) })]
    internal class LocalizedText_ReplaceCustomValues
    {
        private static Regex regex = new Regex(@"\btick\b", RegexOptions.IgnoreCase | RegexOptions.Compiled);

        private static bool Prepare() => Configuration.UseNameOverride;

        private static void Postfix(ref string __result)
        {
            if (string.IsNullOrEmpty(__result)) return;

            __result = regex.Replace(__result, Configuration.NameOverride);
        }
    }
}
