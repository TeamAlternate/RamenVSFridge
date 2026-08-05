using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Jaehee.Editor
{
    [InitializeOnLoad]
    internal static class FireEffectPrefabBuilder
    {
        private const string MaterialPath = "Assets/Jaehee/etc/MAT_RDOffset Fire.mat";
        private const string PrefabPath = "Assets/Jaehee/etc/PF_RDOffset Fire.prefab";

        static FireEffectPrefabBuilder()
        {
            EditorApplication.delayCall += BuildIfNeeded;
        }

        [MenuItem("Tools/Ramen VS Fridge/Rebuild RDOffset Fire Prefab")]
        private static void RebuildFromMenu()
        {
            Build(true);
        }

        private static void BuildIfNeeded()
        {
            GameObject existing = AssetDatabase.LoadAssetAtPath<GameObject>(PrefabPath);
            if (existing != null && existing.GetComponent<ParticleSystem>() != null)
            {
                return;
            }

            Build(false);
        }

        private static void Build(bool overwrite)
        {
            Material fireMaterial = AssetDatabase.LoadAssetAtPath<Material>(MaterialPath);
            if (fireMaterial == null)
            {
                Debug.LogWarning($"RDOffset Fire material was not ready: {MaterialPath}");
                return;
            }

            GameObject existing = AssetDatabase.LoadAssetAtPath<GameObject>(PrefabPath);
            if (!overwrite && existing != null && existing.GetComponent<ParticleSystem>() != null)
            {
                return;
            }

            GameObject root = new GameObject("PF_RDOffset Fire");
            ParticleSystem particles = root.AddComponent<ParticleSystem>();

            ParticleSystem.MainModule main = particles.main;
            main.duration = 2f;
            main.loop = true;
            main.prewarm = true;
            main.startLifetime = new ParticleSystem.MinMaxCurve(1.1f, 1.65f);
            main.startSpeed = new ParticleSystem.MinMaxCurve(0.08f, 0.28f);
            main.startSize = new ParticleSystem.MinMaxCurve(1.15f, 1.65f);
            main.startRotation = new ParticleSystem.MinMaxCurve(-0.12f, 0.12f);
            main.startColor = Color.white;
            main.simulationSpace = ParticleSystemSimulationSpace.Local;
            main.maxParticles = 16;

            ParticleSystem.EmissionModule emission = particles.emission;
            emission.enabled = true;
            emission.rateOverTime = 7f;

            ParticleSystem.ShapeModule shape = particles.shape;
            shape.enabled = true;
            shape.shapeType = ParticleSystemShapeType.Cone;
            shape.angle = 6f;
            shape.radius = 0.12f;
            shape.radiusThickness = 1f;

            ParticleSystem.SizeOverLifetimeModule size = particles.sizeOverLifetime;
            size.enabled = true;
            AnimationCurve sizeCurve = new AnimationCurve(
                new Keyframe(0f, 0.45f),
                new Keyframe(0.18f, 1f),
                new Keyframe(0.72f, 0.85f),
                new Keyframe(1f, 0.2f));
            size.size = new ParticleSystem.MinMaxCurve(1f, sizeCurve);

            ParticleSystem.ColorOverLifetimeModule color = particles.colorOverLifetime;
            color.enabled = true;
            Gradient colorGradient = new Gradient();
            colorGradient.SetKeys(
                new[]
                {
                    new GradientColorKey(new Color(1f, 0.48f, 0.08f), 0f),
                    new GradientColorKey(new Color(1f, 0.12f, 0.015f), 1f)
                },
                new[]
                {
                    new GradientAlphaKey(0f, 0f),
                    new GradientAlphaKey(1f, 0.08f),
                    new GradientAlphaKey(0.9f, 0.72f),
                    new GradientAlphaKey(0f, 1f)
                });
            color.color = colorGradient;

            ParticleSystem.NoiseModule noise = particles.noise;
            noise.enabled = true;
            noise.strength = 0.12f;
            noise.frequency = 0.65f;
            noise.scrollSpeed = 0.45f;
            noise.damping = true;
            noise.quality = ParticleSystemNoiseQuality.High;

            ParticleSystemRenderer renderer = root.GetComponent<ParticleSystemRenderer>();
            renderer.renderMode = ParticleSystemRenderMode.Billboard;
            renderer.alignment = ParticleSystemRenderSpace.View;
            renderer.material = fireMaterial;
            renderer.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
            renderer.receiveShadows = false;
            renderer.sortMode = ParticleSystemSortMode.Distance;
            renderer.minParticleSize = 0f;
            renderer.maxParticleSize = 3f;
            renderer.SetActiveVertexStreams(new List<ParticleSystemVertexStream>
            {
                ParticleSystemVertexStream.Position,
                ParticleSystemVertexStream.Normal,
                ParticleSystemVertexStream.Color,
                ParticleSystemVertexStream.UV,
                ParticleSystemVertexStream.StableRandomXY
            });

            PrefabUtility.SaveAsPrefabAsset(root, PrefabPath);
            Object.DestroyImmediate(root);
            AssetDatabase.SaveAssets();
            Debug.Log($"RDOffset Fire particle prefab created: {PrefabPath}");
        }
    }
}
