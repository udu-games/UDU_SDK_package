using System.IO;
using UnityEditor;
using UnityEngine;

#if UNITY_EDITOR
public class UDU_Editor_CreateAndroidManifest : MonoBehaviour
{
    [MenuItem("UDU/Create Android Manifest")]
    static void CreateAndroidFolderAndManifest()
    {
        // Define the paths
        string targetPluginsFolderPath = "Assets/Plugins";
        string targetAndroidFolderPath = Path.Combine(targetPluginsFolderPath, "Android");
        string sourceManifestPath = "Packages/com.udu_company.udu_sdk/Plugins/Android/AndroidManifest.xml";

        // Create the "Plugins" folder if it doesn't exist
        if (!AssetDatabase.IsValidFolder(targetPluginsFolderPath))
        {
            AssetDatabase.CreateFolder("Assets", "Plugins");
        }

        // Create the "Android" folder if it doesn't exist
        if (!AssetDatabase.IsValidFolder(targetAndroidFolderPath))
        {
            AssetDatabase.CreateFolder(targetPluginsFolderPath, "Android");
        }

        // Copy the AndroidManifest file to the target folder
        string targetManifestPath = Path.Combine(targetAndroidFolderPath, "AndroidManifest.xml");
        File.Copy(sourceManifestPath, targetManifestPath, true);

        // Refresh the AssetDatabase to make Unity aware of the changes
        AssetDatabase.Refresh();

        Debug.Log("Android folder and manifest created successfully!");
    }
}
#endif
