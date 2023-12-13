using UnityEngine;

public class UDUGetters : UDUAbstractBytesSetters
{
    #region Private static console data
    private static long timestamp;
    private static bool isConnected;
    private static Vector3 acceleration;
    private static Vector3 angularVelocity;
    private static Quaternion orientation;
    private static float magneticHeading;
    private static Vector3 normalizedTrackpadCoordinates;

    private Vector3 trackpadCoordinates;
    private Vector3 previousTrackpadValue;
    private Vector3 currentTrackpadValue;
    private float lastCheckTime;
    private static bool isTrackpadPressed;
    private float[] differenceBuffer = new float[7];
    private float trackpadMinX = 500f, trackpadMaxX = 1350f, trackpadMinY = 375f, trackpadMaxY = 1012f; // averaging trackpad values(X,Y) over multiple console
    #endregion

    private void Start()
    {
        EventsSystemHandler.Instance.onConsoleConnect += IsConsoleConnectedFromBase;
        InitializeTrackpad();
    }

    private void Update()
    {
        GetAccelerationFromBase();
        GetTimestampFromBase();
        GetAngularVelocityFromBase();
        GetOrientationFromBase();
        GetTrackpadCoordinatesFromBase();
        GetMagneticHeadingFromBase();

        TrackpadValueCheck();
        TrackpadDirection();
    }

    #region Getting static console data
    /// <summary>
    /// Returns the current timestamp, or the current system time. Helpful for calculating, measuring specific events that occur and for debugging.
    /// </summary>
    /// <returns></returns>
    public static long GetTimestamp()
    {
        return timestamp;
    }
    /// <summary>
    /// Returns the UDU Console acceleration as a Vector3.
    /// </summary>
    /// <returns></returns>
    public static Vector3 GetAcceleration()
    {
        return acceleration;
    }
    /// <summary>
    /// Returns the UDU Console angular velocity as a Vector3.
    /// </summary>
    /// <returns></returns>
    public static Vector3 GetAngularVelocity()
    {
        return angularVelocity;
    }
    /// <summary>
    /// Returns the UDU Console orientation as a Quaternion.
    /// </summary>
    /// <returns></returns>
    public static Quaternion GetOrientation()
    {
        return orientation;
    }
    /// <summary>
    /// Returns the UDU Console trackpad coordinates as a Vector3. X being the vertical axis, Y the horizontal axis, Z the depth inside the trackpad.
    /// Improved trackpad coordinates are now normalized & rounded. They are mapped to be (X,Y (-1.0f, 1.0f)). Z is still retrieving raw trackpada data.
    /// </summary>
    /// <returns></returns>
    public static Vector3 GetTrackpadCoordinates()
    {
        return normalizedTrackpadCoordinates;
    }
    /// <summary>
    /// Returns the UDU Console angle compared to magnetic north as a float.
    /// </summary>
    /// <returns></returns>
    public static float GetMagneticHeading()
    {
        return magneticHeading;
    }
    /// <summary>
    /// Return true if the console is connected, false otherwise.
    /// </summary>
    /// <returns></returns>
    public static bool IsConsoleConnected()
    {
        return isConnected;
    }
    /// <summary>
    /// Return true if the trackpad is being pressed, false otherwise.
    /// </summary>
    /// <returns></returns>
    public static bool IsTrackpadPressed()
    {
        return isTrackpadPressed;
    }

    #endregion

    #region Getting console data from base
    private void GetTimestampFromBase()
    {
        timestamp = base._timestamp;
    }

    private void GetAccelerationFromBase()
    {
        acceleration = base._acceleration;
    }

    private void GetAngularVelocityFromBase()
    {
        angularVelocity = base._angularVelocity;
    }

    private void GetOrientationFromBase()
    {
        orientation = base._orientation;
    }

    private void GetTrackpadCoordinatesFromBase()
    {
        if (isTrackpadPressed)
        {
            trackpadCoordinates = base._trackpadCoordinates;
        }
        else
        {
            trackpadCoordinates = Vector3.zero;
        }
    }

    private void GetMagneticHeadingFromBase()
    {
        magneticHeading = base._magneticHeading;
    }

    private void IsConsoleConnectedFromBase(bool isConsoleConnected)
    {
        isConnected = isConsoleConnected;
    }

    #endregion

    #region Trackpad data management
    private Vector3 GetRawTrackpadCoordinates()
    {
        return trackpadCoordinates;
    }

    private void InitializeTrackpad()
    {
        currentTrackpadValue = base._trackpadCoordinates;
        previousTrackpadValue = currentTrackpadValue;
        for (int i = 0; i < differenceBuffer.Length; i++)
        {
            differenceBuffer[i] = 0f; // Initialize the buffer with 0 values.
        }
    }

    private void TrackpadValueCheck()
    {
        if (Time.time - lastCheckTime >= .025f)
        {
            // Shift the existing values to the right (removing the value at index 0).
            for (int i = differenceBuffer.Length - 1; i > 0; i--)
            {
                differenceBuffer[i] = differenceBuffer[i - 1];
            }

            currentTrackpadValue = base._trackpadCoordinates;
            float sumCurrent = currentTrackpadValue.x + currentTrackpadValue.y + currentTrackpadValue.z;
            float sumPrevious = previousTrackpadValue.x + previousTrackpadValue.y + previousTrackpadValue.z;

            float difference = Mathf.Abs(sumCurrent - sumPrevious);

            differenceBuffer[0] = difference;
            float sumArray = SumArray(differenceBuffer);

            // Check if the current value is different from the previous value
            if (sumArray > 0)
            {
                isTrackpadPressed = true;
            }
            else
            {
                isTrackpadPressed = false;
            }

            // Update the previousValue for the next frame
            previousTrackpadValue = currentTrackpadValue;

            lastCheckTime = Time.time;
        }
    }

    private float SumArray(float[] array)
    {
        float sum = 0.0f;
        for (int i = 0; i < array.Length; i++)
        {
            sum += array[i];
        }
        return sum;
    }

    #region Trackpad averaging, normalizing & rounding
    private Vector3 TrackpadDirection()
    {
        // normalizing values based on an average of multiple consoles
        float normalizedTrackpadValuesX = NormalizeValuesWithMiddlePoint(GetRawTrackpadCoordinates().x, trackpadMinX, trackpadMaxX);
        float normalizedTrackpadValuesY = NormalizeValuesWithMiddlePoint(GetRawTrackpadCoordinates().y, trackpadMinY, trackpadMaxY);

        // round the normalized values
        float roundedNormalizedTrackpadValuesX = RoundToDecimalPlaces(normalizedTrackpadValuesX, 3);
        float roundedNormalizedTrackpadValuesY = RoundToDecimalPlaces(normalizedTrackpadValuesY, 3);

        // re map to correctly represent the users touch ( x == up / down ) ( y == right / left )
        normalizedTrackpadCoordinates = new Vector3(roundedNormalizedTrackpadValuesY, roundedNormalizedTrackpadValuesX, GetRawTrackpadCoordinates().z);

        return normalizedTrackpadCoordinates;
    }

    private float NormalizeValuesWithMiddlePoint(float rawValue, float minValue, float maxValue)
    {
        if (rawValue == 0f)
        {
            return 0.0f;
        }
        else if (rawValue < minValue)
        {
            return -1f;
        }
        else if (rawValue > maxValue)
        {
            return 1f;
        }
        else
        {
            float middleValue = (minValue + maxValue) / 2.0f;

            if (Mathf.Approximately(minValue, middleValue) || Mathf.Approximately(maxValue, middleValue))
            {
                // Avoid division by zero
                return 0.0f;
            }

            if (rawValue <= middleValue)
            {
                return (rawValue - middleValue) / (middleValue - minValue);
            }
            else
            {
                return (rawValue - middleValue) / (maxValue - middleValue);
            }
        }
    }

    private float RoundToDecimalPlaces(float value, int decimalPlaces)
    {
        float multiplier = Mathf.Pow(10f, decimalPlaces);
        return Mathf.Round(value * multiplier) / multiplier;
    }
    #endregion

    #endregion
}