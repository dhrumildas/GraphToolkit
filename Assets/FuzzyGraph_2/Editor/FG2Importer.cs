using System;
using System.IO;
using FuzzyGraph2.Runtime;
using Unity.GraphToolkit.Editor;
using UnityEditor.AssetImporters;
using UnityEngine;

namespace FuzzyGraph2.Editor
{
    [ScriptedImporter(
        1,
        FuzzyGraph2Asset.AssetExtension)]
    public sealed class FuzzyGraph2Importer :
        ScriptedImporter
    {
        public override void OnImportAsset(
            AssetImportContext ctx)
        {
            // saving the graph loops back through here
            FuzzyGraph2Asset graph =
                GraphDatabase
                    .LoadGraphForImporter<
                        FuzzyGraph2Asset>(
                        ctx.assetPath);

            if (graph == null)
            {
                ctx.LogImportError(
                    $"couldn't load fuzzygraph2: {ctx.assetPath}");

                return;
            }

            try
            {
                RuntimeFuzzyGraph2 runtimeGraph =FG2Compiler.Compile(graph,out FuzzyGraph2CompileReport report);

                runtimeGraph.name =
                    Path.GetFileNameWithoutExtension(
                        ctx.assetPath);

                ctx.AddObjectToAsset(
                    "RuntimeFuzzyGraph2",
                    runtimeGraph);

                ctx.SetMainObject(runtimeGraph);

                // tiny receipt so we know what baked
                Debug.Log(
                    report.ToLogLine(runtimeGraph.name));
            }
            catch (Exception exception)
            {
                ctx.LogImportError(
                    $"fuzzygraph2 compile failed: "
                    + exception.Message);
            }
        }
    }
}