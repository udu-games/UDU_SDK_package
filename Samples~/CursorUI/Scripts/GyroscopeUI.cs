using UnityEngine;
using UDU;

public class GyroscopeUI : MonoBehaviour
{
    // Speed multiplier to control UI movement
    private int speed = 100;
    [SerializeField] private float sensitivity = 5f;

    // Reference to the UI element you want to move
    private GameObject cursor;
    private RectTransform cursorUiElement;
    private RectTransform canvasRectTransform;

    private bool isConnected = false;

    void Start()
    {
        cursor = transform.Find("Cursor").gameObject;
        cursorUiElement = cursor.GetComponent<RectTransform>();
        canvasRectTransform = GetComponent<RectTransform>();

        EventsSystemHandler.Instance.onTriggerPressSqueezeButton += EnableCursorFeature;
        EventsSystemHandler.Instance.onTriggerReleaseSqueezeButton += DisableCursorFeature;


        EventsSystemHandler.Instance.onControllerConnect += IsConnected;
        EventsSystemHandler.Instance.onTriggerPressSqueezeButton += ResetCursorPosition;
    }

    void Update()
    {

        if (isConnected || UDUGetters.IsControllerConnected())
        {
            MoveCursorUsingController();
        }

        ClampCursorPosition();
    }

    private void EnableCursorFeature()
    {
        cursor.SetActive(true);
    }

    private void DisableCursorFeature()
    {
        cursor.SetActive(false);
    }

    public void MoveCursorUsingController()
    {
        Vector2 movement = new Vector2(-UDUGetters.GetAngularVelocity().z , -UDUGetters.GetAngularVelocity().x * 2.1641f) * speed * sensitivity;

        cursorUiElement.anchoredPosition += movement;
    }

    private void ClampCursorPosition()
    {
        // Get the size of the UI object
        Vector2 size = cursorUiElement.rect.size;

        // Get the size of the canvas
        Vector2 canvasSize = canvasRectTransform.rect.size;

        // Get the position of the UI object relative to the canvas
        Vector2 position = cursorUiElement.anchoredPosition;

        // Calculate the boundaries of the canvas
        float minX = -canvasSize.x / 2 + size.x / 2;
        float maxX = canvasSize.x / 2 - size.x / 2;
        float minY = -canvasSize.y / 2 + size.y / 2;
        float maxY = canvasSize.y / 2 - size.y / 2;

        // Clamp the position of the UI object within the canvas boundaries
        position.x = Mathf.Clamp(position.x, minX, maxX);
        position.y = Mathf.Clamp(position.y, minY, maxY);

        // Update the position of the UI object
        cursorUiElement.anchoredPosition = position;
    }

    private void IsConnected()
    {
        isConnected = true;
    }

    private void ResetCursorPosition()
    {
        // Move the UI element back to its original position
        cursorUiElement.anchoredPosition = new Vector2(0, 0);
    }

}
