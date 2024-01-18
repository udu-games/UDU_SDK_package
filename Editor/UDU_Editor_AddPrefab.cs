using System.IO;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace UDU
{
#if UNITY_EDITOR
    public class InstantiatePrefabFromPath : MonoBehaviour
    {
        [MenuItem("UDU/Instantiate/Controller Connection")]
        private static void InstantiatePrefab()
        {
            // Check if an instance already exists in the scene
            UDUControllerConnection existingInstance = FindObjectOfType<UDUControllerConnection>();

            if (existingInstance != null)
            {
                Debug.Log("Controller Connection Prefab already instantiated in the scene.");
                return; // Don't instantiate a new one
            }

            string dllPrefabPath = "Packages/com.udu_company.udu_sdk/UDU_SDK/Prefabs/Controller_Manager.prefab";

            if (File.Exists(dllPrefabPath))
            {
                PrefabLoader(dllPrefabPath);
            }
            else
            {
                string assets_Editor_PrefabPath = "Assets/UDU_SDK/Prefabs/Controller_Manager.prefab";

                if (!File.Exists(dllPrefabPath))
                {
                    PrefabLoader(assets_Editor_PrefabPath);
                }
                else
                {
                    if (!File.Exists(assets_Editor_PrefabPath))
                    {
                        Debug.LogError("Controller Connection Prefab not found at path: " + assets_Editor_PrefabPath);
                    }

                    Debug.LogError("Controller Connection Prefab not found at path: " + dllPrefabPath);
                }
            }
        }

        private static void PrefabLoader(string _prefabPath)
        {
            // Add an eventSystem component to the scene if there is none
            CheckAndAddEventSystem();

            // Load the prefab from the project assets using its path
            GameObject loadedPrefab = AssetDatabase.LoadAssetAtPath(_prefabPath, typeof(GameObject)) as GameObject;

            // Instantiate the prefab into the scene
            GameObject _prefab = Instantiate(loadedPrefab);

            if (_prefab != null)
            {
                Debug.Log("Controller Connection Prefab instantiated successfully!");

                // Add components to the instantiated prefab if needed
                UDUControllerConnection script = _prefab.GetComponent<UDUControllerConnection>();
                if (script == null)
                {
                    _prefab.AddComponent<UDUControllerConnection>();
                    _prefab.AddComponent<UDUOutputsBytesSetter>();
                    _prefab.AddComponent<UDUGetters>();
                    _prefab.AddComponent<ControllerManagerSingleton>();
                    _prefab.AddComponent<BluetoothPermissions>();
                }

                // apply name change
                _prefab.name = "Controller_Manager";

                // Mark the prefab as dirty to ensure it gets saved
                EditorUtility.SetDirty(_prefab);

                // Create reconnect button
                if (_prefabPath.ToLower().Contains("packages"))
                    CreateReconnectButton("Packages/com.udu_company.udu_sdk/UDU_SDK/Textures/ReconnectImage.png", _prefab, new Vector2(0, -600), new Vector2(500, 250));
                else if (_prefabPath.ToLower().Contains("assets"))
                    CreateReconnectButton("Assets/UDU_SDK/Textures/ReconnectImage.png", _prefab, new Vector2(0, -600), new Vector2(500, 250));
            }
            else
            {
                Debug.LogError("Controller Connection Prefab Failed to instantiate.");
            }
        }

        private static Button CreateReconnectButton(string assetPath, GameObject parent, Vector2 position, Vector2 size)
        {
            // Create a new button GameObject
            GameObject buttonGameObject = new GameObject("ReconnectButton");

            buttonGameObject.transform.SetParent(parent.transform.GetChild(0).transform);
            Button button = buttonGameObject.AddComponent<Button>();
            RectTransform rectTransform = buttonGameObject.AddComponent<RectTransform>();

            // Set the RectTransform properties
            rectTransform.sizeDelta = size;
            rectTransform.anchoredPosition = position;

            // add image background and apply button image
            buttonGameObject.AddComponent<Image>();
            button.targetGraphic = button.GetComponent<Image>();

            Sprite imageSprite = AssetDatabase.LoadAssetAtPath<Sprite>(assetPath);
            button.GetComponent<Image>().sprite = imageSprite;

            button.gameObject.SetActive(false);
            return button;
        }

        // check and add an eventSystem component to the scene if there is none
        private static void CheckAndAddEventSystem()
        {
            EventSystem _eventSystem = FindObjectOfType<EventSystem>();
            if(_eventSystem == null)
            {
                GameObject eventSystemObject = new GameObject("EventSystem");
                eventSystemObject.AddComponent<EventSystem>();
                eventSystemObject.AddComponent<StandaloneInputModule>();
                Debug.Log("Created an EventSystem component and added it to the scene.");
            }
        }
    }
#endif
}