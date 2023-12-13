using UnityEditor;
using UnityEngine;

namespace UDU
{
#if UNITY_EDITOR
    public class InstantiatePrefabFromPath : MonoBehaviour
    {
        [MenuItem("UDU/Instantiate/Controller Connection Prefab")]
        private static void InstantiatePrefab()
        {
            // Check if an instance already exists in the scene
            UDUConsoleConnection existingInstance = FindObjectOfType<UDUConsoleConnection>();

            if (existingInstance != null)
            {
                Debug.Log("Controller Connection Prefab already instantiated in the scene.");
                return; // Don't instantiate a new one
            }

            string prefabPath = "Packages/com.udu_company.udu_sdk/UDU_SDK/Prefabs/Controller_Manager.prefab";
            GameObject prefab = (GameObject)AssetDatabase.LoadAssetAtPath(prefabPath, typeof(GameObject));

            if (prefab != null)
            {
                PrefabLoader(prefab);
            }
            else
            {
                string assets_Editor_PrefabPath = "Assets/UDU_SDK/Prefabs/Controller_Manager.prefab";
                GameObject assets_Editor_prefab = (GameObject)AssetDatabase.LoadAssetAtPath(assets_Editor_PrefabPath, typeof(GameObject));

                if (prefab == null)
                {
                    PrefabLoader(assets_Editor_prefab);
                }
                else
                {
                    if (assets_Editor_prefab == null)
                    {
                        Debug.LogError("Controller Connection Prefab not found at path: " + assets_Editor_PrefabPath);
                    }

                    Debug.LogError("Controller Connection Prefab not found at path: " + prefabPath);
                }
            }
        }

        private static void PrefabLoader(GameObject prefab)
        {
            // Instantiate the prefab into the scene
            GameObject instantiatedPrefab = Instantiate(prefab);

            if (instantiatedPrefab != null)
            {
                Debug.Log("Controller Connection Prefab instantiated successfully!");

                // Add components to the instantiated prefab if needed
                UDUConsoleConnection script = instantiatedPrefab.GetComponent<UDUConsoleConnection>();
                if (script == null)
                {
                    script = instantiatedPrefab.AddComponent<UDUConsoleConnection>();
                    instantiatedPrefab.AddComponent<UDUOutputsBytesSetter>();
                    instantiatedPrefab.AddComponent<UDUGetters>();
                    instantiatedPrefab.AddComponent<ConsoleManagerSingleton>();
                    instantiatedPrefab.AddComponent<BluetoothPermissions>();
                }

                // Mark the prefab as dirty to ensure it gets saved
                EditorUtility.SetDirty(instantiatedPrefab);

                instantiatedPrefab.name = "Controller_Manager";
            }
            else
            {
                Debug.LogError("Controller Connection Prefab Failed to instantiate.");
            }
        }
    }
#endif
}
