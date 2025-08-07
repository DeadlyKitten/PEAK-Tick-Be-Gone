using UnityEngine;

namespace TickBeGone
{
    internal class BugUtilities
    {
        public static bool TryCensorBug(Transform parent, Vector3 position, Vector3 scale, Material material)
        {
            if (Configuration.UseShapeOverride && BugUtilities.TryGetReplacementBug(out var replacementBug))
            {
                replacementBug.transform.parent = parent;
                replacementBug.transform.localPosition = position;
                replacementBug.transform.localScale = scale;

                if (Configuration.ShapeOverride != Shape.Cat)
                {
                    var renderer = replacementBug.GetComponent<MeshRenderer>();
                    renderer.material = material;

                    var propertyBlock = new MaterialPropertyBlock();
                    renderer.GetPropertyBlock(propertyBlock);

                    if (Configuration.UseColorOverride)
                    {
                        propertyBlock.SetColor("_BaseColor", Configuration.ColorOverride);
                        propertyBlock.SetColor("_Color1", Configuration.ColorOverride);
                    }

                    if (!Configuration.UseOriginalTexture)
                    {

                        propertyBlock.SetTexture("_Texture1", Texture2D.whiteTexture);
                    }

                    renderer.SetPropertyBlock(propertyBlock);
                }
                else
                {
                    replacementBug.transform.localScale *= 1.5f;
                }

                if (replacementBug.TryGetComponent<Collider>(out var newCollider))
                    Component.Destroy(newCollider);

                return true;
            }

            return false;
        }

        public static void CensorOldBug(Renderer bugRenderer)
        {
            var propertyBlock = new MaterialPropertyBlock();
            bugRenderer.GetPropertyBlock(propertyBlock);

            if (Configuration.UseColorOverride)
            {
                propertyBlock.SetColor("_BaseColor", Configuration.ColorOverride);
                propertyBlock.SetColor("_Color1", Configuration.ColorOverride);
            }

            if (!Configuration.UseOriginalTexture)
            {
                propertyBlock.SetTexture("_Texture1", Texture2D.whiteTexture);
            }

            bugRenderer.SetPropertyBlock(propertyBlock);
        }

        internal static bool TryGetReplacementBug(out GameObject bug)
        {
            bug = null;
            if (!Configuration.Enabled) return false;

            switch (Configuration.ShapeOverride)
            {
                case Shape.Sphere:
                    bug = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                    return true;
                case Shape.Cube:
                    bug = GameObject.CreatePrimitive(PrimitiveType.Cube);
                    return true;
                case Shape.Cylinder:
                    bug = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
                    return true;
                case Shape.Cat:
                    bug = GameObject.CreatePrimitive(PrimitiveType.Quad);
                    var renderer = bug.GetComponent<Renderer>();
                    renderer.sharedMaterial.shader = Shader.Find("GD/Face Cards");

                    var propertyBlock = new MaterialPropertyBlock();
                    renderer.GetPropertyBlock(propertyBlock);
                    propertyBlock.SetTexture("_MainTex",  TickPlugin.GetRandomCat());
                    renderer.SetPropertyBlock(propertyBlock);

                    bug.AddComponent<BetterBillboard>();

                    Component.DestroyImmediate(bug.GetComponent<Collider>());
                    return true;
                case Shape.Unchanged:
                default:
                    return false;
            }
        }
    }
}
