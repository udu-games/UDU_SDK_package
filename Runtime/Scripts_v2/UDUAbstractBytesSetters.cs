using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UDUAbstractBytesSetters : MonoBehaviour
{

    #region Field variables
    #region Private fields
    private List<byte> motionStream = new List<byte>();
    private List<byte> buttonStream = new List<byte>();
    private List<byte> trackpadStream = new List<byte>();

    protected bool firstTrackpadPress = true;
    protected bool trackpadPressed = false;
    protected bool trackpadReleased = false;

    private float prevTrackpadX, prevTrackpadY, prevTrackpadZ;
    #endregion

    #region Fields : IMU
    protected long _timestamp;
    protected Quaternion _orientation = new Quaternion();
    protected Vector3 _acceleration = new Vector3();
    protected Vector3 _linearAcceleration = new Vector3();
    protected Vector3 _angularVelocity = new Vector3();
    protected float _magneticHeading;
    protected Vector3 _trackpadCoordinates = new Vector3();
    #endregion

    #endregion

    #region Fields : Buttons
    protected bool squeezePressed = false;
    protected bool triggerPressed = false;

    protected bool triggerReleased = false;
    protected bool squeezeReleased = false;

    protected bool firstSqueezePress = true;
    protected bool firstTriggerPress = true;
    #endregion

    #region Setting stream bytes
    public void SetIMUBytes(byte[] arr)
    {
        List<byte> s = new List<byte>();
        foreach (byte b in arr)
        {
            s.Add(b);
        }
        motionStream = s;
        if (motionStream.Count >= 26)
        {
            SetIMUData();
        }
    }

    public void SetButtonBytes(byte[] arr)
    {
        List<byte> s = new List<byte>();
        foreach (byte b in arr)
        {
            s.Add(b);
        }
        buttonStream = s;
        SetButtonPressed();
    }

    public void SetTrackpadBytes(byte[] arr)
    {
        List<byte> s = new List<byte>();
        foreach (byte b in arr)
        {
            s.Add(b);
        }
        trackpadStream = s;
        SetTrackpadCoordinates();
    }

    #endregion

    #region Setting console data
    private void SetIMUData()
    {
        SetTimestamp();
        SetAcceleration();
        SetLinearAcceleration();
        SetAngularVelocity();
        SetOrientation();
        SetMagneticHeading();
    }

    protected void SetTimestamp()
    {
        byte[] timestampBytes = new byte[8] { 0x00, 0x00, 0x00, 0x00, motionStream[0], motionStream[1], motionStream[2], motionStream[3] };
        _timestamp = System.BitConverter.ToInt64(timestampBytes, 0);
    }

    protected void SetAcceleration()
    {
        byte[] axBytes = new byte[2] { motionStream[4], motionStream[5] };
        byte[] ayBytes = new byte[2] { motionStream[6], motionStream[7] };
        byte[] azBytes = new byte[2] { motionStream[8], motionStream[9] };

        _acceleration.x = System.BitConverter.ToInt16(axBytes, 0);
        _acceleration.y = System.BitConverter.ToInt16(ayBytes, 0);
        _acceleration.z = System.BitConverter.ToInt16(azBytes, 0);
    }

    protected void SetLinearAcceleration()
    {
        byte[] laxBytes = new byte[2] { motionStream[26], motionStream[27] };
        byte[] layBytes = new byte[2] { motionStream[28], motionStream[29] };
        byte[] lazBytes = new byte[2] { motionStream[30], motionStream[31] };

        _linearAcceleration.x = System.BitConverter.ToInt16(laxBytes, 0);
        _linearAcceleration.y = System.BitConverter.ToInt16(layBytes, 0);
        _linearAcceleration.z = System.BitConverter.ToInt16(lazBytes, 0);
    }

    protected void SetAngularVelocity()
    {
        byte[] avxBytes = new byte[2] { motionStream[10], motionStream[11] };
        byte[] avyBytes = new byte[2] { motionStream[12], motionStream[13] };
        byte[] avzBytes = new byte[2] { motionStream[14], motionStream[15] };

        _angularVelocity.x = System.BitConverter.ToInt16(avxBytes, 0) / 16384.0f;
        _angularVelocity.y = System.BitConverter.ToInt16(avyBytes, 0) / 16384.0f;
        _angularVelocity.z = System.BitConverter.ToInt16(avzBytes, 0) / 16384.0f;
    }

    protected void SetOrientation()
    {
        byte[] qxBytes = new byte[2] { motionStream[16], motionStream[17] };
        byte[] qyBytes = new byte[2] { motionStream[18], motionStream[19] };
        byte[] qzBytes = new byte[2] { motionStream[20], motionStream[21] };
        byte[] qwBytes = new byte[2] { motionStream[22], motionStream[23] };

        _orientation.x = System.BitConverter.ToInt16(qxBytes, 0) / 16384.0f;
        _orientation.y = System.BitConverter.ToInt16(qyBytes, 0) / 16384.0f;
        _orientation.z = System.BitConverter.ToInt16(qzBytes, 0) / 16384.0f;
        _orientation.w = System.BitConverter.ToInt16(qwBytes, 0) / 16384.0f;
    }

    protected void SetMagneticHeading()
    {
        byte[] magneticHeading = new byte[2] { motionStream[24], motionStream[25] };

        _magneticHeading = System.BitConverter.ToInt16(magneticHeading, 0);
    }

    protected void SetButtonPressed()
    {
        if (buttonStream.Count > 0)
        {
            int buttonpress = (int)buttonStream[0];
            squeezePressed = buttonpress == 1 || buttonpress == 3;
            triggerPressed = buttonpress == 2 || buttonpress == 3;
        }

        if (!triggerPressed) firstTriggerPress = true;
        if (!squeezePressed) firstSqueezePress = true;

        if (squeezePressed && firstSqueezePress)
        {
            EventsSystemHandler.Instance.TriggerPressSqueezeButton();
            Debug.Log("Squeeze");
            firstSqueezePress = false;
            squeezeReleased = true;
        }
        if (triggerPressed && firstTriggerPress)
        {
            EventsSystemHandler.Instance.TriggerPressTriggerButton();
            Debug.Log("Trigger");
            firstTriggerPress = false;
            triggerReleased = true;
        }
        if (!triggerPressed && triggerReleased)
        {
            EventsSystemHandler.Instance.TriggerReleaseTriggerButton();
            Debug.Log("Trigger released");
            triggerReleased = false;
        }
        if (!squeezePressed && squeezeReleased)
        {
            EventsSystemHandler.Instance.TriggerReleaseSqueezeButton();
            Debug.Log("Squeeze released");
            squeezeReleased = false;
        }
    }

    protected void SetTrackpadCoordinates()
    {
        byte[] trackpadxCoordinates = new byte[2] { trackpadStream[0], trackpadStream[1] };
        byte[] trackpadyCoordinates = new byte[2] { trackpadStream[2], trackpadStream[3] };
        byte[] trackpadzCoordinates = new byte[2] { trackpadStream[4], trackpadStream[5] };

        _trackpadCoordinates.x = System.BitConverter.ToInt16(trackpadxCoordinates, 0);
        _trackpadCoordinates.y = System.BitConverter.ToInt16(trackpadyCoordinates, 0);
        _trackpadCoordinates.z = System.BitConverter.ToInt16(trackpadzCoordinates, 0);
    }

    #endregion

}
