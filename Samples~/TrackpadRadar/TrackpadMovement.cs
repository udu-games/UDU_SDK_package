using UDU;
using UnityEngine;
using UnityEngine.UI;

public class TrackpadMovement : MonoBehaviour
{
    private RectTransform rectTrans;
    private RectTransform parentRectTrans;

    private Text normalizedTrackpadValuesText;
    private Text currentSpeedText;

    private InputField speedInputField;

    private Vector3 trackpadInput; // get UDU.TrackpadCoordinates

    private float movementSpeed = 25; // Adjust this value to control the force applied ( in the scene )

    private void Start()
    {
        InitSetVariableComponents();
    }

    private void Update()
    {
        if (!UDUGetters.IsControllerConnected()) return;

        trackpadInput = UDUGetters.GetTrackpadCoordinates();

        PlayerTrackpadMovement();
        SetAndUpdateGUI();
    }

    // moving the player with the trackpad.. similar to using a joystick
    private void PlayerTrackpadMovement()
    {
        if (UDUGetters.IsTrackpadPressed())
        {
            // Map trackpad input to the screen coordinates
            Vector2 screenPos = new Vector2(
                parentRectTrans.rect.width * 0.5f * trackpadInput.x,
                parentRectTrans.rect.height * 0.5f * trackpadInput.y
            );

            // Calculate the new position based on user input
            Vector2 newPosition = parentRectTrans.anchoredPosition + screenPos;

            // Calculate the distance from the center of the circular area
            float distance = Vector2.Distance(newPosition, parentRectTrans.anchoredPosition);

            // Clamp the distance to stay within the circular movement area
            float radius = Mathf.Min(parentRectTrans.rect.width * 0.5f, parentRectTrans.rect.height * 0.5f) - 25f;
            distance = Mathf.Clamp(distance, 0f, radius);

            // Normalize the direction vector and multiply by the clamped distance
            Vector2 direction = (newPosition - parentRectTrans.anchoredPosition).normalized;
            Vector2 clampedPosition = parentRectTrans.anchoredPosition + direction * distance;

            // Smoothly interpolate between the current position and the new position
            Vector2 lerpedPosition = Vector2.Lerp(rectTrans.anchoredPosition, clampedPosition, movementSpeed * Time.deltaTime);

            // Set the new position of the UI element
            rectTrans.anchoredPosition = lerpedPosition;
        }
        if (!UDUGetters.IsTrackpadPressed())
        {
            // Smoothly interpolate between the current position and the new position
            Vector2 lerpedPosition = Vector2.Lerp(rectTrans.anchoredPosition, parentRectTrans.anchoredPosition, movementSpeed * Time.deltaTime);
            rectTrans.anchoredPosition = lerpedPosition;
        }
    }

    // initially set up varibales to get && find correct components
    private void InitSetVariableComponents()
    {
        rectTrans = GetComponent<RectTransform>();
        parentRectTrans = GameObject.Find("TrackpadBackground").GetComponent<RectTransform>();
        normalizedTrackpadValuesText = GameObject.Find("NormalizedTrackpadValuesText").GetComponent<Text>();
        currentSpeedText = GameObject.Find("CurrentSpeedText").GetComponent<Text>();
        speedInputField = GameObject.Find("ChangeSpeedInputField").GetComponent<InputField>();
    }

    // Constantly update the GUI with the correct values
    private void SetAndUpdateGUI()
    {
        currentSpeedText.text = $"Current speed: {movementSpeed}";
        normalizedTrackpadValuesText.text = $"Normalized Trackpad:\n(X:{trackpadInput.x}, Y:{trackpadInput.y}) ";
    }

    // attached the inputfield component.. when the user finishes an input
    public void GUIUpdateCurrentSpeed()
    {
        movementSpeed = int.Parse(speedInputField.text);
    }
}