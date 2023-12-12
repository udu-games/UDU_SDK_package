using UnityEditor;
using UnityEngine;

#if UNITY_EDITOR
namespace UDU
{
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

            string prefabPath = "Packages/com.udu_company.udu_sdk/UDU_SDK/Prefabs/Console_Manager.prefab";
            GameObject prefab = (GameObject)AssetDatabase.LoadAssetAtPath(prefabPath, typeof(GameObject));

            if (prefab != null)
            {
                GameObject instantiatedPrefab = PrefabUtility.InstantiatePrefab(prefab) as GameObject;

                if (instantiatedPrefab != null)
                {
                    Debug.Log("Controller Connection Prefab instantiated successfully!");

                    //TODO : Maybe need to add components to the prefab as a new ? 
                    UDUConsoleConnection script = instantiatedPrefab.GetComponent<UDUConsoleConnection>();
                    if (script == null)
                    {
                        instantiatedPrefab.AddComponent<UDUConsoleConnection>();
                        instantiatedPrefab.AddComponent<UDUOutputsBytesSetter>();
                        instantiatedPrefab.AddComponent<UDUGetters>();
                        instantiatedPrefab.AddComponent<ConsoleManagerSingleton>();
                        instantiatedPrefab.AddComponent<BluetoothPermissions>();
                    }
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
    }
}
#endif