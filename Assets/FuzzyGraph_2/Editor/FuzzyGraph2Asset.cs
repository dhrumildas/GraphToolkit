using System;
using UnityEditor;
using Unity.GraphToolkit.Editor;

namespace FuzzyGraph2.Editor
{
    // stores the visual node spaghetti for fg2 until the compiler bakes it
    [Serializable]
    [Graph(AssetExtension)]
    public class FuzzyGraph2Asset : Graph
    {
        // new file ext so we don't clobber the og v1 files
        public const string AssetExtension = "fuzzygraph2";

        [MenuItem(
            "Assets/Create/FuzzyGraph 2/FuzzyGraph 2 Graph",
            false,
            10)]
        private static void CreateAssetFile()
        {
            GraphDatabase
                .PromptInProjectBrowserToCreateNewAsset<
                    FuzzyGraph2Asset>();
        }
    }
}