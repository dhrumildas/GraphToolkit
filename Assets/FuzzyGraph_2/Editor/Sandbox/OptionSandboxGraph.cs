using System;
using UnityEditor;
using Unity.GraphToolkit.Editor;

namespace FuzzyGraph2.Editor.Sandbox
{
    [Serializable]
    [Graph(AssetExtension)]
    public sealed class OptionSandboxGraph : Graph
    {
        public const string AssetExtension = "gtkoptionsandbox";

        [MenuItem(
            "Assets/Create/FuzzyGraph 2/Tests/Option Sandbox",
            false,
            200)]
        private static void CreateAssetFile()
        {
            GraphDatabase
                .PromptInProjectBrowserToCreateNewAsset<
                    OptionSandboxGraph>();
        }
    }
}