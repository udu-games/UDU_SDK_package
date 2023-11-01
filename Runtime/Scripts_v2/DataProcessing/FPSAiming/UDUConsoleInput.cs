using MSP_Input;
using TMPro;
using UnityEngine;

// Class responsible for collecting the console data
public class UDUConsoleInput : MonoBehaviour
{
    // private fields // 
    //public AbstractDataStream uduConsoleDatastream;
    [HideInInspector, SerializeField] public static float QUATERNION_SCALE_FACTOR = 16384.0f;
    [HideInInspector, SerializeField] public float quatX, quatY, quatZ, quatW;

    [HideInInspector, SerializeField] public short gyrX, gyrY, gyrZ;
    [HideInInspector, SerializeField] public short accX, accY, accZ;

    [HideInInspector, SerializeField] public Quaternion currentOrientation;

    //[HideInInspector, SerializeField] public OrientationSensors orientationSensors;
    private DataManipulation dataManipulation;
    [HideInInspector, SerializeField] public Vector3 angularVelocity;

   

    private Quaternion initialRotation;
    private Quaternion gyroOffset;
    //
    private void Start()
    {
        //orientationSensors = FindObjectOfType<OrientationSensors>();
        dataManipulation = FindObjectOfType<DataManipulation>();
        angularVelocity = Vector3.zero;
        currentOrientation = Quaternion.identity;

        EventsSystemHandler.Instance.onTriggerPressSqueezeButton += ResetHeading;
    }

    void Update()
    {
       if (UDUGetters.IsConsoleConnected())
        {
            Quaternion gyroRotation = UDUGetters.GetOrientation();
            // Apply the offset to the gyroscope rotation
            Quaternion correctedRotation = gyroOffset * gyroRotation;
            // Remove Z-axis rotation
            Vector3 eulerRotation = correctedRotation.eulerAngles;
            eulerRotation.y = 0;
            Quaternion newRotation = Quaternion.Euler(eulerRotation);

            dataManipulation.SetConsoleCurrentQuat(newRotation);
        }
    }

    // Called on squeeze button pressed
    public void ResetHeading()
    {
        if (UDUGetters.IsConsoleConnected())
        {
            //OrientationSensors.SetHeading(0f);
            SetCurrentHeadingQuaternion();
        }
    }

    public void SetCurrentHeadingQuaternion()
    {
        initialRotation = UDUGetters.GetOrientation();

        Vector3 initialEulerRotation = initialRotation.eulerAngles;

        initialEulerRotation.z = 0;
        gyroOffset = Quaternion.Euler(initialEulerRotation) * Quaternion.Inverse(initialRotation);
    }
}