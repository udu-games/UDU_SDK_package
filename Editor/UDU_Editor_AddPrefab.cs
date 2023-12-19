using UnityEditor;
using UnityEngine;

namespace UDU
{
#if UNITY_EDITOR
    public class InstantiatePrefabFromPath : MonoBehaviour
    {
        [MenuItem("UDU/Instantiate/Controller Connection")]
        private static void InstantiatePrefab()
        {
            // Check if an instance already exists in the scene
            UDUConsoleConnection existingInstance = FindObjectOfType<UDUConsoleConnection>();

            if (existingInstance != null)
            {
                Debug.Log("Controller Connection Prefab already instantiated in the scene.");
                return; // Don't instantiate a new one
            }

            string dllPrefabPath = "Packages/com.udu_company.udu_sdk/UDU_SDK/Prefabs/Controller_Manager.prefab";

            if (dllPrefabPath != null)
            {
                PrefabLoader(dllPrefabPath);
            }
            else
            {
                string assets_Editor_PrefabPath = "Assets/UDU_SDK/Prefabs/Controller_Manager.prefab";

                if (dllPrefabPath == null)
                {
                    PrefabLoader(assets_Editor_PrefabPath);
                }
                else
                {
                    if (assets_Editor_PrefabPath == null)
                    {
                        Debug.LogError("Controller Connection Prefab not found at path: " + assets_Editor_PrefabPath);
                    }

                    Debug.LogError("Controller Connection Prefab not found at path: " + dllPrefabPath);
                }
            }
        }

        private static void PrefabLoader(string _prefabPath)
        {
            // Load the prefab from the project assets using its path
            GameObject loadedPrefab = AssetDatabase.LoadAssetAtPath(_prefabPath, typeof(GameObject)) as GameObject;

            // Instantiate the prefab into the scene
            GameObject _prefab = Instantiate(loadedPrefab);

            if (_prefab != null)
            {
                Debug.Log("Controller Connection Prefab instantiated successfully!");

                // Add components to the instantiated prefab if needed
                UDUConsoleConnection script = _prefab.GetComponent<UDUConsoleConnection>();
                if (script == null)
                {
                    _prefab.AddComponent<UDUConsoleConnection>();
                    _prefab.AddComponent<UDUOutputsBytesSetter>();
                    _prefab.AddComponent<UDUGetters>();
                    _prefab.AddComponent<ConsoleManagerSingleton>();
                    _prefab.AddComponent<BluetoothPermissions>();
                }

                // apply name change
                _prefab.name = "Controller_Manager";

                // Mark the prefab as dirty to ensure it gets saved
                EditorUtility.SetDirty(_prefab);
            }
            else
            {
                Debug.LogError("Controller Connection Prefab Failed to instantiate.");
            }
        }
    }
#endif
}