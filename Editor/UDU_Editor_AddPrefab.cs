using UnityEditor;
using UnityEngine;

namespace UDU
{
    public class InstantiatePrefabFromPath : MonoBehaviour
    {
#if UNITY_EDITOR
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
            else
            {
                Debug.LogError("Controller Connection Prefab not found at path: " + prefabPath);
            }
        }
#endif
    }
}
