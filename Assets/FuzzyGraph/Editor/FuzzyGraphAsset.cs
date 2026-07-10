using System;
using UnityEditor;
using Unity.GraphToolkit.Editor;

namespace FuzzyGraph.Editor
{
    [Serializable]
    [Graph(AssetExtension)]
    public class FuzzyGraphAsset : Graph
    {
        public const string AssetExtension = "fuzzygraph";

        [MenuItem("Assets/Create/New Fuzzy Graph", false, 10)]
        private static void CreateAssetFile()
        {
            GraphDatabase.PromptInProjectBrowserToCreateNewAsset<FuzzyGraphAsset>();
        }
    }
}