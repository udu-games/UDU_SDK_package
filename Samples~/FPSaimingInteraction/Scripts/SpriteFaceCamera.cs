using UnityEngine;

public class SpriteFaceCamera : MonoBehaviour
{
    private Camera mainCamera;

    void Start()
    {
        // Find the main camera in the scene
        mainCamera = Camera.main;

        if (mainCamera == null)
        {
            Debug.LogError("Main camera not found in the scene. Make sure you have a camera tagged as MainCamera.");
        }
    }

    void Update()
    {
        // Ensure we have a valid reference to the main camera
        if (mainCamera != null)
        {
            // Make the sprite face the camera
            transform.LookAt(transform.position + mainCamera.transform.rotation * Vector3.forward,
                mainCamera.transform.rotation * Vector3.up);
        }
    }
}

