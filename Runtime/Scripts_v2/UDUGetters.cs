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
    #endregion

    private void Start()
    {
        EventsSystemHandler.Instance.onConsoleConnect += IsConsoleConnectedFromBase;
    }

    private void Update()
    {
        GetAccelerationFromBase();
        GetTimestampFromBase();
        GetAngularVelocityFromBase();
        GetOrientationFromBase();
        GetTrackpadCoordinatesFromBase();
        GetMagneticHeadingFromBase();
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
        trackpadCoordinates = base._trackpadCoordinates;
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

}
