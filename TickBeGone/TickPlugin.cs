using System.Reflection;
using BepInEx;
using HarmonyLib;
using UnityEngine;

namespace TickBeGone
{
    [BepInPlugin("com.steven.peak.tickbegone", "Tick-Be-Gone", "2.0.0")]
    public class TickPlugin : BaseUnityPlugin
    {
        private static Texture2D catTexture = new Texture2D(2,2);

        private void Awake()
        {
            Configuration.Init();

            var harmony = new Harmony("com.steven.peak.tickbegone");
            harmony.PatchAll();

            LoadCat();
#if DEBUG
            var bugfixer = gameObject.AddComponent<Bugfixer>();
            bugfixer.useLocalCharacter = true;
#endif
        }

        private void LoadCat()
        {
            var assembly = Assembly.GetExecutingAssembly();
            var catPaths = assembly.GetManifestResourceNames();

            var index = Random.Range(0, catPaths.Length);

            var stream = assembly.GetManifestResourceStream(catPaths[index]);
            if (stream == null)
            {
                Debug.LogError($"Failed to load resource: {catPaths[index]}");
                return;
            }
            var textureData = new byte[stream.Length];
            stream.Read(textureData, 0, (int) stream.Length);
            var texture = new Texture2D(2, 2);
            texture.LoadImage(textureData);
            catTexture = texture;
        }

        public static Texture2D GetRandomCat(bool useLastIndex = false) => catTexture;
    }
}