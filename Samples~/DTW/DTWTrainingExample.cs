using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DTWTrainingExample : MonoBehaviour
{
    [SerializeField]
    public bool considerAcceleration = true;
    [SerializeField]
    public bool considerAngularVelocity = true;
    [SerializeField]
    public bool considerYaw = true;
    [SerializeField]
    public bool considerPitch = true;
    [SerializeField]
    public bool considerRoll = true;
    [SerializeField]
    private string LogPrefix = "DTW Training Results";

    private Quaternion consoleOrientation;
    private Vector3 consoleAcceleration;
    private Vector3 consoleAngularVelocity;

    // Data save here while the trigger button is pressed
    private List<Vector3> currentAcceleration = new List<Vector3>();
    private List<Vector3> currentAngularVelocity = new List<Vector3>();
    private List<Quaternion> currentOrientation = new List<Quaternion>();

    private bool hasProcessedTrainingData = true;

    DTWTraining dtwTraining = DTWTraining.Instance;

    // Gesture recording flags
    private bool isTriggerButtonPressed = false;

    // Control gestures are used during training to find closest distances to the repeatedly trained gesture
    // yet farthest from the control gestures
    private ControlGestures controlGestures;
    private const double CONTROL_SCORE_WEIGHT = 0.5;

    // Start is called before the first frame update
    void Start()
    {
        DTW4 dtw = DTW4.Instance;
        controlGestures = ControlGestures.Instance;
        dtw.Initialize(considerAcceleration, considerAngularVelocity, considerYaw, considerPitch, considerRoll, LogPrefix);
        dtwTraining.Initialize(dtw, controlGestures, CONTROL_SCORE_WEIGHT, LogPrefix);

        SubscribeToBLEEvents();
    }

    // Update is called once per frame
    void Update()
    {
        if (isTriggerButtonPressed)
        {
            ListenToGestureData();
        }
        else if (!hasProcessedTrainingData)
        {
            // After executing this, expect a log with the best gesture data
            dtwTraining.ProcessTrainingData(currentAcceleration, currentAngularVelocity, currentOrientation);
            hasProcessedTrainingData = true;
        }
    }

        private void ListenToGestureData()
        {
            consoleOrientation = UDUGetters.GetOrientation();
            consoleAcceleration = UDUGetters.GetAcceleration().normalized;
            consoleAngularVelocity = UDUGetters.GetAngularVelocity();
            currentAcceleration.Add(consoleAcceleration);
            currentOrientation.Add(consoleOrientation);
            currentAngularVelocity.Add(consoleAngularVelocity);
            hasProcessedTrainingData = false;
        }

    // Triggered by the reset button on screen
    public void ClearAll()
    {
        dtwTraining.ClearAll();
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

    private void OnConsoleTriggerButtonPress()
    {
        isTriggerButtonPressed = true;
    }

    private void OnConsoleTriggerButtonRelease()
    {
        isTriggerButtonPressed = false;
    }
}
