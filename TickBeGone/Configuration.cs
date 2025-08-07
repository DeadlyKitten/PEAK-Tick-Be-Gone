using System.IO;
using BepInEx;
using BepInEx.Configuration;
using UnityEngine;

namespace TickBeGone
{
    internal static class Configuration
    {
        private static ConfigFile config;

        private static ConfigEntry<bool> enabled;
        private static ConfigEntry<bool> useColorOverride;
        private static ConfigEntry<Color> colorOverride;
        private static ConfigEntry<bool> useOriginalTexture;
        private static ConfigEntry<bool> useShapeOverride;
        private static ConfigEntry<Shape> shapeOverride;
        private static ConfigEntry<bool> useNameOverride;
        private static ConfigEntry<string> nameOverride;
        private static ConfigEntry<bool> useIconOverride;

        public static bool Enabled => enabled.Value;
        public static bool UseColorOverride => useColorOverride.Value;
        public static Color ColorOverride => colorOverride.Value;
        public static bool UseOriginalTexture => useOriginalTexture.Value;
        public static bool UseShapeOverride => useShapeOverride.Value;
        public static Shape ShapeOverride => shapeOverride.Value;
        public static bool UseNameOverride => useNameOverride.Value;
        public static string NameOverride => nameOverride.Value;
        public static bool UseIconOverride => useIconOverride.Value;

        public static void Init()
        {
            config = new ConfigFile(Path.Combine(Paths.ConfigPath, "Tick-Be-Gone.cfg"), true);

            enabled = config.Bind("General", "Enabled", true, "Enable or disable the mod.");

            useColorOverride = config.Bind("Color", "UseColorOverride", true, "Enable color override for the bug.");
            colorOverride = config.Bind("Color", "ColorOverride", Color.magenta, "Color to override the bug's appearance with.");
            useOriginalTexture = config.Bind("Color", "UseOriginalTexture", false, "Use the original texture for the bug.");

            useShapeOverride = config.Bind("Shape", "UseShapeOverride", true, "Enable shape override for the bug.");
            shapeOverride = config.Bind("Shape", "ShapeOverride", Shape.Sphere, "Shape to override the bug's appearance with.");

            useNameOverride = config.Bind("Name", "UseNameOverride", true, "Enable name override for the bug.");
            nameOverride = config.Bind("Name", "NameOverride", "[Redacted]", "Name to override the bug's name with.");

            useIconOverride = config.Bind("Icon", "UseIconOverride", true, "Replace the bug icon with a kitty.");
        }
    }
}
