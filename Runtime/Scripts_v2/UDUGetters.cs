using UnityEngine;

public class UDUGetters : UDUAbstractBytesSetters
{
    #region Private static console data
    private static long timestamp;
    private static bool isConnected;
    private static Vector3 acceleration;
    private static Vector3 angularVelocity;
    private static Quaternion orientation;
    private static Vector3 trackpadCoordinates;
    private static float magneticHeading;

    private Vector3 previousTrackpadValue;
    private Vector3 currentTrackpadValue;
    private float lastCheckTime;
    public static bool isTrackpadPressed;
    private float[] differenceBuffer = new float[7];
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
    }

    #region Getting static console data
    public static long GetTimestamp()
    {
        return timestamp;
    }

    public static Vector3 GetAcceleration()
    {
        return acceleration;
    }

    public static Vector3 GetAngularVelocity()
    {
        return angularVelocity;
    }

    public static Quaternion GetOrientation()
    {
        return orientation;
    }

    public static Vector3 GetTrackpadCoordinates()
    {
        return trackpadCoordinates;
    }

    public static float GetMagneticHeading()
    {
        return magneticHeading;
    }

    public static bool IsConsoleConnected()
    {
        return isConnected;
    }

    #endregion

    #region Getting console data from base
    private void GetTimestampFromBase()
    {
        timestamp = base._timestamp;
    }

    public void GetAccelerationFromBase()
    {
        acceleration = base._acceleration;
    }

    public void GetAngularVelocityFromBase()
    {
        angularVelocity = base._angularVelocity;
    }

    public void GetOrientationFromBase()
    {
        orientation = base._orientation;
    }

    public void GetTrackpadCoordinatesFromBase()
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

    public void GetMagneticHeadingFromBase()
    {
        magneticHeading = base._magneticHeading;
    }

    public void IsConsoleConnectedFromBase(bool isConsoleConnected)
    {
        isConnected = isConsoleConnected;
    }

    #endregion

    #region Trackpad data management
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
    #endregion

}

