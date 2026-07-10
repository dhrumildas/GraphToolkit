using System.IO;
using FuzzyGraph.Runtime;
using Unity.GraphToolkit.Editor;
using UnityEditor.AssetImporters;
using UnityEngine;

namespace FuzzyGraph.Editor
{
    [ScriptedImporter(1, FuzzyGraphAsset.AssetExtension)]
    public class FuzzyGraphImporter : ScriptedImporter
    {
        public override void OnImportAsset(
            AssetImportContext ctx)
        {
            FuzzyGraphAsset graph =
                GraphDatabase.LoadGraphForImporter<FuzzyGraphAsset>(
                    ctx.assetPath);

            if (graph == null)
            {
                Debug.LogError(
                    $"Failed to load FuzzyGraph asset: {ctx.assetPath}");

                return;
            }

            RuntimeFuzzyGraph runtimeGraph =
                FuzzyGraphCompiler.Compile(graph);

            runtimeGraph.name =
                Path.GetFileNameWithoutExtension(ctx.assetPath);

            ctx.AddObjectToAsset(
                "RuntimeFuzzyGraph",
                runtimeGraph);

            ctx.SetMainObject(runtimeGraph);
        }
    }
}