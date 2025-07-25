using UnityEngine;
using UnityEditor;

public class AssetFlagCleaner
{
    [MenuItem("GameObject/Clean Asset Save Flags")]
    private static void CleanAssetFlags()
    {
        // Define the paths to your specific problem assets
        string[] assetPaths = new string[]
        {
            "Assets/Resources/Assets_2D/Care Screen Assets/Clean Assets/Trash Lineart.png",
            "Assets/Resources/Assets_2D/Care Screen Assets/Clean Assets/Poo Lineart.png",
            "Assets/Resources/Assets_2D/ball.png"
            // Add any other asset paths that give you this warning
        };

        foreach (string path in assetPaths)
        {
            Object asset = AssetDatabase.LoadAssetAtPath<Object>(path);
            if (asset != null)
            {
                // Clear the DontSave flag
                asset.hideFlags &= ~HideFlags.DontSave;
                EditorUtility.SetDirty(asset);
                Debug.Log($"Cleaned flags for asset: {path}");
            }
            else
            {
                Debug.LogWarning($"Could not find asset at path: {path}");
            }
        }

        // Save the changes to the assets
        AssetDatabase.SaveAssets();
        Debug.Log("Asset flag cleaning complete!");
    }
}