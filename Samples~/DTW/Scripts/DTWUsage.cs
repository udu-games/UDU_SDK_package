using System;
//using DTWot;
using System.Collections.Generic;
using UDU;
using UnityEngine;
using UnityEngine.UI;

public class DTWUsage : MonoBehaviour
{

    #region Gestures declaration
    #region Triangle Gesture
    public static readonly List<Vector3> TRIANGLE_ACCELERATION = new List<Vector3>()
    {
        new Vector3(-0.16f, 0.46f, 0.87f),
        new Vector3(-0.24f, 0.49f, 0.84f),
        new Vector3(-0.24f, 0.49f, 0.84f),
        new Vector3(-0.24f, 0.49f, 0.84f),
        new Vector3(-0.48f, 0.65f, 0.60f),
        new Vector3(-0.48f, 0.81f, 0.33f),
        new Vector3(-0.48f, 0.81f, 0.33f),
        new Vector3(-0.28f, 0.89f, -0.35f),
        new Vector3(0.02f, 0.49f, -0.87f),
        new Vector3(0.34f, 0.01f, -0.94f),
        new Vector3(0.34f, -0.13f, -0.93f),
        new Vector3(0.46f, -0.20f, -0.87f),
        new Vector3(0.59f, -0.17f, -0.79f),
        new Vector3(0.68f, -0.06f, -0.73f),
        new Vector3(0.77f, 0.08f, -0.63f),
        new Vector3(0.77f, 0.10f, -0.63f),
        new Vector3(0.33f, 0.91f, -0.23f),
        new Vector3(0.33f, 0.91f, -0.23f),
        new Vector3(-0.41f, 0.74f, 0.53f),
        new Vector3(-0.45f, 0.55f, 0.71f),
        new Vector3(-0.62f, 0.45f, 0.65f),
        new Vector3(-0.62f, 0.45f, 0.65f),
        new Vector3(-0.88f, 0.20f, 0.44f),
        new Vector3(-0.99f, 0.11f, 0.09f),
        new Vector3(-0.88f, 0.45f, -0.13f),
        new Vector3(-0.88f, 0.45f, -0.13f),
        new Vector3(-0.39f, 0.90f, -0.20f),
        new Vector3(-0.24f, 0.95f, -0.17f),
        new Vector3(0.54f, 0.74f, 0.40f),
        new Vector3(0.73f, 0.58f, 0.38f),
        new Vector3(0.74f, 0.45f, 0.50f),
    };

    public static readonly List<Vector3> TRIANGLE_ANGULAR_VELOCITY = new List<Vector3>()
    {
        new Vector3(0.03f, 0.04f, 0.03f),
        new Vector3(-0.04f, 0.05f, 0.03f),
        new Vector3(-0.04f, 0.05f, 0.03f),
        new Vector3(-0.04f, 0.05f, 0.03f),
        new Vector3(-0.31f, 0.00f, -0.01f),
        new Vector3(-0.39f, -0.06f, -0.06f),
        new Vector3(-0.39f, -0.06f, -0.06f),
        new Vector3(-0.38f, -0.11f, -0.09f),
        new Vector3(-0.31f, -0.14f, -0.07f),
        new Vector3(-0.24f, -0.13f, -0.04f),
        new Vector3(-0.18f, -0.11f, -0.01f),
        new Vector3(-0.10f, -0.07f, 0.01f),
        new Vector3(0.01f, -0.10f, 0.08f),
        new Vector3(0.04f, -0.10f, 0.09f),
        new Vector3(0.09f, -0.08f, 0.13f),
        new Vector3(0.10f, -0.07f, 0.15f),
        new Vector3(0.28f, -0.01f, 0.27f),
        new Vector3(0.28f, -0.01f, 0.27f),
        new Vector3(0.34f, 0.09f, 0.28f),
        new Vector3(0.24f, 0.01f, 0.23f),
        new Vector3(0.12f, 0.06f, 0.14f),
        new Vector3(0.12f, 0.06f, 0.14f),
        new Vector3(0.05f, 0.05f, 0.06f),
        new Vector3(0.04f, -0.03f, -0.05f),
        new Vector3(0.06f, -0.16f, -0.24f),
        new Vector3(0.06f, -0.16f, -0.24f),
        new Vector3(0.09f, -0.20f, -0.35f),
        new Vector3(0.10f, -0.20f, -0.37f),
        new Vector3(0.13f, -0.13f, -0.28f),
        new Vector3(0.06f, -0.02f, -0.23f),
        new Vector3(0.04f, -0.03f, -0.10f),
    };
    #endregion
    #region Knight Moves newly made
    #region Stab
    public static readonly List<Vector3> STAB_ACCELERATION = new List<Vector3>()
    {
        new Vector3(0.15f, -0.18f, 0.97f),
        new Vector3(0.18f, -0.25f, 0.95f),
        new Vector3(0.08f, -0.29f, 0.95f),
        new Vector3(0.14f, -0.73f, 0.66f),
        new Vector3(0.17f, -0.73f, 0.66f),
        new Vector3(0.03f, -0.59f, 0.81f),
        new Vector3(-0.39f, -0.77f, 0.51f),
        new Vector3(-0.37f, 0.92f, 0.13f),
        new Vector3(-0.25f, 0.97f, 0.02f),
        new Vector3(0.13f, 0.87f, 0.48f),
        new Vector3(0.45f, 0.55f, 0.70f),
    };
    public static readonly List<Vector3> STAB_ANGULAR_VELOCITY = new List<Vector3>()
    {
        new Vector3(-0.06f, 0.02f, 0.03f),
        new Vector3(-0.07f, 0.03f, 0.02f),
        new Vector3(-0.09f, 0.04f, 0.05f),
        new Vector3(-0.16f, 0.09f, 0.02f),
        new Vector3(-0.10f, 0.01f, 0.05f),
        new Vector3(-0.08f, 0.05f, 0.04f),
        new Vector3(-0.11f, 0.10f, -0.06f),
        new Vector3(0.12f, 0.03f, -0.14f),
        new Vector3(0.11f, 0.04f, -0.18f),
        new Vector3(0.22f, 0.00f, -0.13f),
        new Vector3(0.06f, -0.02f, -0.02f),
    };
    #endregion
    #region Back Slash
    public static readonly List<Vector3> BACK_SLASH_ACCELERATION = new List<Vector3>()
    {
        new Vector3(-0.51f, 0.45f, -0.73f),
        new Vector3(-0.52f, 0.67f, -0.53f),
        new Vector3(-0.52f, 0.67f, -0.53f),
        new Vector3(-0.27f, 0.78f, -0.57f),
        new Vector3(-0.14f, 0.80f, -0.59f),
        new Vector3(-0.02f, 0.81f, -0.58f),
        new Vector3(0.19f, 0.95f, -0.24f),
        new Vector3(-0.05f, 0.72f, 0.69f),
    };
    public static readonly List<Vector3> BACK_SLASH_ANGULAR_VELOCITY = new List<Vector3>()
    {
        new Vector3(0.16f, 0.04f, 0.03f),
        new Vector3(0.23f, 0.04f, 0.05f),
        new Vector3(0.23f, 0.04f, 0.05f),
        new Vector3(0.36f, 0.00f, 0.04f),
        new Vector3(0.54f, -0.05f, 0.05f),
        new Vector3(0.71f, -0.03f, 0.04f),
        new Vector3(1.14f, -0.16f, 0.12f),
        new Vector3(0.85f, 0.04f, 0.14f),
    };
    #endregion
    #region Forward Slash
    public static readonly List<Vector3> FORWARD_SLASH_ACCELERATION = new List<Vector3>()
    {
        new Vector3(0.63f, 0.16f, -0.76f),
        new Vector3(0.56f, 0.33f, -0.76f),
        new Vector3(0.56f, 0.40f, -0.73f),
        new Vector3(0.54f, 0.59f, -0.60f),
        new Vector3(0.44f, 0.71f, -0.54f),
        new Vector3(0.26f, 0.84f, -0.47f),
        new Vector3(0.26f, 0.84f, -0.47f),
        new Vector3(0.28f, 0.96f, 0.00f),
        new Vector3(0.35f, 0.80f, 0.48f),
        new Vector3(0.27f, 0.77f, 0.58f),
        new Vector3(0.27f, 0.77f, 0.58f),
        new Vector3(0.27f, 0.77f, 0.58f),
    };
    public static readonly List<Vector3> FORWARD_SLASH_ANGULAR_VELOCITY = new List<Vector3>()
    {
        new Vector3(0.07f, -0.06f, -0.09f),
        new Vector3(0.14f, -0.09f, -0.10f),
        new Vector3(0.17f, -0.10f, -0.10f),
        new Vector3(0.27f, -0.08f, -0.07f),
        new Vector3(0.54f, -0.04f, 0.04f),
        new Vector3(1.02f, 0.08f, 0.24f),
        new Vector3(1.02f, 0.08f, 0.24f),
        new Vector3(1.19f, -0.03f, 0.28f),
        new Vector3(0.84f, 0.03f, 0.31f),
        new Vector3(0.78f, 0.01f, 0.34f),
        new Vector3(0.78f, 0.01f, 0.34f),
        new Vector3(0.78f, 0.01f, 0.34f),
    };
    #endregion
    #region Dunk
    public static readonly List<Vector3> DUNK_ACCELERATION = new List<Vector3>()
    {
        new Vector3(0.13f, -0.59f, -0.79f),
        new Vector3(0.13f, -0.06f, -0.99f),
        new Vector3(0.13f, -0.06f, -0.99f),
        new Vector3(0.22f, 0.37f, -0.90f),
        new Vector3(0.21f, 0.55f, -0.81f),
        new Vector3(0.09f, 0.78f, -0.62f),
        new Vector3(0.09f, 0.78f, -0.62f),
        new Vector3(-0.15f, 0.90f, 0.40f),
    };

    public static readonly List<Vector3> DUNK_ANGULAR_VELOCITY = new List<Vector3>()
    {
        new Vector3(0.12f, 0.00f, -0.01f),
        new Vector3(0.18f, -0.01f, 0.00f),
        new Vector3(0.18f, -0.01f, 0.00f),
        new Vector3(0.24f, -0.01f, 0.01f),
        new Vector3(0.43f, 0.02f, 0.07f),
        new Vector3(0.74f, 0.04f, 0.16f),
        new Vector3(0.74f, 0.04f, 0.16f),
        new Vector3(0.86f, 0.24f, 0.25f),
    };
    #endregion
    #region Uppercut
    public static readonly List<Vector3> UPPERCUT_ACCELERATION = new List<Vector3>
    {
        new Vector3(-0.14f, 0.64f, 0.75f),
        new Vector3(-0.15f, 0.66f, 0.74f),
        new Vector3(-0.10f, 0.78f, 0.62f),
        new Vector3(0.01f, 0.93f, 0.36f),
        new Vector3(0.00f, 0.99f, 0.14f),
        new Vector3(0.00f, 0.99f, 0.11f),
        new Vector3(0.04f, 0.97f, -0.22f),
        new Vector3(0.07f, 0.69f, -0.72f),
        new Vector3(0.02f, 0.20f, -0.98f),
        new Vector3(0.09f, 0.04f, -1.00f),
        new Vector3(0.13f, -0.22f, -0.97f),
    };
    public static readonly List<Vector3> UPPERCUT_ANGULAR_VELOCITY = new List<Vector3>
    {
        new Vector3(-0.07f, -0.02f, 0.03f),
        new Vector3(-0.17f, -0.01f, -0.01f),
        new Vector3(-0.31f, -0.04f, -0.03f),
        new Vector3(-0.46f, -0.03f, -0.03f),
        new Vector3(-0.54f, 0.01f, -0.03f),
        new Vector3(-0.55f, 0.02f, -0.04f),
        new Vector3(-0.59f, 0.03f, -0.07f),
        new Vector3(-0.51f, 0.09f, -0.07f),
        new Vector3(-0.37f, -0.01f, -0.07f),
        new Vector3(-0.34f, -0.04f, -0.06f),
        new Vector3(-0.25f, -0.06f, -0.04f),
    };
    #endregion
    #endregion

    #endregion

    // Gesture recording flags
    private bool isTriggerButtonPressed = false;
    private bool hasLoggedData = true;

    private List<Vector3> accelerations = new List<Vector3>();
    private List<Vector3> angularVelocities = new List<Vector3>();
    private List<Quaternion> orientations = new List<Quaternion>();

    [HideInInspector] public Text gestureFoudName;
    [HideInInspector] public bool considerAcceleration = true;
    [HideInInspector] public bool considerAngularVelocity = true;
    [HideInInspector] public bool considerYaw = false;
    [HideInInspector] public bool considerPitch = false;
    [HideInInspector] public bool considerRoll = false;
    [HideInInspector] public string LogPrefix = "DTW Recognition Results";

    private static DTW4 dtw = DTW4.Instance;

    // Constants
    private readonly float RecognitionThreshold = 15;

   private void Start()
    {
        dtw.Initialize(considerAcceleration, considerAngularVelocity, considerYaw, considerPitch, considerRoll, LogPrefix);
        SubscribeToBLEEvents();

        AddGesture("TRIANGLE", TRIANGLE_ACCELERATION, TRIANGLE_ANGULAR_VELOCITY);
        AddGesture("BACK_SLASH", BACK_SLASH_ACCELERATION, BACK_SLASH_ANGULAR_VELOCITY);
        AddGesture("STAB", STAB_ACCELERATION, STAB_ANGULAR_VELOCITY);
        AddGesture("FORWARD_SLASH", FORWARD_SLASH_ACCELERATION, FORWARD_SLASH_ANGULAR_VELOCITY);
        AddGesture("DUNK", DUNK_ACCELERATION, DUNK_ANGULAR_VELOCITY);
        AddGesture("UPPERCUT", UPPERCUT_ACCELERATION, UPPERCUT_ANGULAR_VELOCITY);
    }

    private void Update()
    {
        HandleGestureData();
    }

    private void OnDestroy()
    {
        UnsubscribeFromBLEEvents();
    }

    public void HandleGestureData()
    {
        if (isTriggerButtonPressed)
        {
            ListenToGestureData();
        }
        else if (!hasLoggedData)
        {
            RecognitionResult result = dtw.RecognizeGesture(orientations, accelerations, angularVelocities, RecognitionThreshold);
            double distance;
            result.distancesToOthers.TryGetValue(result.gestureName, out distance);

            if (gestureFoudName != null)
            {
                string distanceDecimals = distance.ToString("F2");
                Debug.Log("Gesture found: " + result.gestureName + "\nMatch:" + distanceDecimals);
                gestureFoudName.text = "Gesture found: " + result.gestureName  + "\nMatch:" + distanceDecimals;
            }

            dtw.LogGestureData(LogPrefix, accelerations, orientations, angularVelocities);

            EventsSystemHandler.Instance.GestureRecognized(result.gestureName);

            ClearGestureData();
            hasLoggedData = true;
        }
    }

    private void ListenToGestureData()
    {
        accelerations.Add(UDUGetters.GetAcceleration().normalized);
        orientations.Add(UDUGetters.GetOrientation());
        angularVelocities.Add(UDUGetters.GetAngularVelocity());
        hasLoggedData = false;
    }

    private void ClearGestureData()
    {
        accelerations.Clear();
        orientations.Clear();
        angularVelocities.Clear();
    }

    private void OnConsoleTriggerButtonPress()
    {
        isTriggerButtonPressed = true;
    }

    private void OnConsoleTriggerButtonRelease()
    {
        isTriggerButtonPressed = false;
    }

    private void SubscribeToBLEEvents()
    {
        EventsSystemHandler.Instance.onTriggerPressTriggerButton += OnConsoleTriggerButtonPress;
        EventsSystemHandler.Instance.onTriggerReleaseTriggerButton += OnConsoleTriggerButtonRelease;
    }

    private void UnsubscribeFromBLEEvents()
    {
        EventsSystemHandler.Instance.onTriggerPressTriggerButton -= OnConsoleTriggerButtonPress;
        EventsSystemHandler.Instance.onTriggerReleaseTriggerButton -= OnConsoleTriggerButtonRelease;
    }
    /// <summary>
    /// Creates a custom gesture with the specified name, acceleration, and angular velocity data.
    /// </summary>
    /// <param name="gestureName">The desired name for the gesture.</param>
    /// <param name="gestureAcceleration">List of acceleration data for the gesture.</param>
    /// <param name="gestureAngularVelocity">List of angular velocity data for the gesture.</param>

    public static void AddGesture(string gestureName, List<Vector3> gestureAcceleration, List<Vector3> gestureAngularVelocity)
    {
        // Now you can add the gesture with the generated parameters
        CreateGesture(gestureName, gestureAcceleration, gestureAngularVelocity);
    }
    private static void CreateGesture(string name, List<Vector3> acceleration, List<Vector3> angularVelocity)
    {
        GestureData gesture = new GestureData(name);
        gesture.Acceleration = acceleration;
        gesture.AngularVelocity = angularVelocity;
        dtw.AddGesture(gesture);
    }

    #region Work in progress - SDK gesture at runtime
    // For future SDK update dynamic gesture from gesture name input at runtime
    //public static void AddGesture(string gestureName)
    //{
    //    // Assuming your acceleration and angular velocity parameters are named in a specific pattern
    //    string accelerationParamName = $"{gestureName}_ACCELERATION";
    //    string angularVelocityParamName = $"{gestureName}_ANGULAR_VELOCITY";

    //    // Use reflection to get the values from the specified parameter names
    //    List<Vector3> acceleration = GetFieldValue<List<Vector3>>(accelerationParamName);
    //    List<Vector3> angularVelocity = GetFieldValue<List<Vector3>>(angularVelocityParamName);

    //    // Now you can add the gesture with the generated parameters
    //    CreateGesture(gestureName, acceleration, angularVelocity);
    //}


    //// Helper method to get field value using reflection
    //private static T GetFieldValue<T>(string fieldName, T defaultValue = default)
    //{
    //    var field = typeof(DTWUsage).GetField(fieldName);
    //    if (field != null)
    //    {
    //        return (T)field.GetValue(null);
    //    }
    //    return defaultValue;
    //}
    #endregion
}
