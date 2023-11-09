using UnityEngine;
using MSP_Input;
using System.Collections;
using System;

namespace MSP_Input.Demo
{
    public class MSP_Sword : MonoBehaviour
    {
        [SerializeField]
        private GameObject pointer;

        Vector3 initialPos;
        Vector3 pointerWorldPosition;

        private bool isCoroutineRunning = false;

        private float PivotDistance = 10f;

        #region DataManipulation
        private Quaternion _consoleCurrentQuat;
        private HeadingPitchRoll deviceHPR;
        private float smoothFactor = 1f;
        private Quaternion gyroRot = Quaternion.identity;
        private const float sqrthalf = 0.707106781186548f;
        private float _headingOffset = -180f;
        private FloatMinMax pitchOffset = new FloatMinMax(0f, -70f, 70f, -90f, 90f);
        public Quaternion gameWorldRotation = Quaternion.identity;
        private LineRenderer lineRenderer;

        private bool isTriggerButtonPressed = false;


        // Cached values
        private AnimationCurve devicePitchAdjustmentCurve;
        #endregion

        #region UDUConsoleInput
        [HideInInspector, SerializeField] public static float QUATERNION_SCALE_FACTOR = 16384.0f;
        [HideInInspector, SerializeField] public float quatX, quatY, quatZ, quatW;

        [HideInInspector, SerializeField] public short gyrX, gyrY, gyrZ;
        [HideInInspector, SerializeField] public short accX, accY, accZ;

        [HideInInspector, SerializeField] public Quaternion currentOrientation;

        //[HideInInspector, SerializeField] public OrientationSensors orientationSensors;

        [HideInInspector, SerializeField] public Vector3 angularVelocity;

        private GameObject pivotPoint;
        private Vector3 pivotPointPosition;
        private Quaternion initialRotation;
        private Quaternion gyroOffset;
        #endregion

        private void Awake()
        {
            pivotPoint = this.gameObject;
        }


        private void Start()
        {
            lineRenderer = GetComponent<LineRenderer>();
            // Can be called in the update method, if it's supposed to change the position
            pivotPointPosition = pivotPoint.transform.position;

            #region DataManipulation
            pitchOffset.ValidateData();
            // Cache the animation curve
            devicePitchAdjustmentCurve = new AnimationCurve(new Keyframe(-90f, 0f), new Keyframe(pitchOffset.value, -pitchOffset.value), new Keyframe(90f, 0f));
            #endregion

            SubscribeToBLEEvents();

            #region UDUConsoleInput
            angularVelocity = Vector3.zero;
            currentOrientation = Quaternion.identity;
            #endregion
        }

        private void OnDestroy()
        {
            UnsubscribeFromBLEEvents();
        }

        //====================================================================
        private void SubscribeToBLEEvents()
        {
            EventsSystemHandler.Instance.onTriggerPressTriggerButton += OnConsoleTriggerButtonPress;
            EventsSystemHandler.Instance.onTriggerReleaseTriggerButton += OnConsoleTriggerButtonRelease;
            EventsSystemHandler.Instance.onTriggerPressSqueezeButton += ResetHeading;
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


        float distanceThreshold = 0.05f;
        private GameObject selectedObject;
        private const float maxRayDistance = 1000.0f;


        void Update()
        {
            // Moved UDUConsoleInput and DataManipulation logic into methods for cleaner code
            HandleUDUConsoleInput();
            HandleDataManipulation();
            UpdatePointerDirection();
            CastRayAndUpdatePointer();
            UpdateSelectedObjectPosition();

            // Method used to determine swipe direction for fruitninja-like game, or "real time angle recognition"
            // Shouldn't be part of this scene as the pointer is no longer set to a fixed distance in space, but rather is dependent on raycast hit
            // meaning the angle between starting position and ending position of the pointer may differ a lot, resulting in wrong angle calculation
            CheckForAccelerationInput();
        }

        void UpdatePointerDirection()
        {
            // Calculate the direction from the pivot to the pointer
            Vector3 directionToPointer = gameWorldRotation * Vector3.up;
            pointerWorldPosition = pivotPointPosition + directionToPointer;
        }

        void CastRayAndUpdatePointer()
        {
            // Cast a ray from pivotPointPosition in the calculated direction
            RaycastHit hit;
            float raycastMaxRange = 50f;
            if (Physics.Raycast(pivotPointPosition, pointerWorldPosition - pivotPointPosition, out hit, raycastMaxRange))
            {
                pointer.transform.position = hit.point; // Update pointer position to hit point
                if (hit.collider.CompareTag("Interactable"))
                {
                    selectedObject = isTriggerButtonPressed ? hit.collider.gameObject : null;
                }
                pointer.transform.position = hit.point;
            }
            else
            {
                //pointer.transform.position = pointerWorldPosition; // Maintain last pointer position
                pointer.transform.position = pivotPointPosition + (pointerWorldPosition - pivotPointPosition).normalized * raycastMaxRange;
                Debug.Log("WorldPosition: " + pointerWorldPosition);
            }
            DrawRaycast(pivotPointPosition, pointer.transform.position); // Visualize the raycast
        }

        void UpdateSelectedObjectPosition()
        {
            // If an object is selected, position it a fixed distance from the pivot
            if (selectedObject != null)
            {
                //Vector3 offsetPosition = pivotPointPosition + (pointerWorldPosition - pivotPointPosition).normalized * PivotDistance;
                Vector3 direction = (pointerWorldPosition - pivotPointPosition).normalized;
                Vector3 offsetPosition = pivotPointPosition + direction * PivotDistance;
                selectedObject.transform.position = offsetPosition;
            }
        }

        void DrawRaycast(Vector3 start, Vector3 end)
        {
            lineRenderer.SetPosition(0, start);
            lineRenderer.SetPosition(1, end);
        }

        void CheckForAccelerationInput()
        {
            // Trigger direction calculation if acceleration is above a threshold
            if (UDUGetters.GetAcceleration().magnitude > 2000 && !isCoroutineRunning)
            {
                StartCoroutine(CalculateDirection());
            }
        }

        void HandleUDUConsoleInput()
        {
            if (UDUGetters.IsConsoleConnected())
            {
                Quaternion gyroRotation = UDUGetters.GetOrientation();
                // Apply the offset to the gyroscope rotation
                Quaternion correctedRotation = gyroOffset * gyroRotation;
                // Remove Z-axis rotation
                Vector3 eulerRotation = correctedRotation.eulerAngles;
                eulerRotation.y = 0;
                _consoleCurrentQuat = Quaternion.Euler(eulerRotation);
            }
        }

        void HandleDataManipulation()
        {
            Quaternion gyroQuat = new Quaternion(0.0f, sqrthalf, -sqrthalf, 0.0f) * _consoleCurrentQuat;
            deviceHPR = HeadingPitchRoll.FromQuaternionTopDownView(gyroRot, true);
            Quaternion newRotation = gyroQuat;
            // Modulo for heading offset
            _headingOffset = (_headingOffset + 180) % 360 - 180;
            Vector3 rotAxis = Vector3.Cross(Vector3.up, newRotation * Vector3.forward);
            newRotation = Quaternion.AngleAxis(devicePitchAdjustmentCurve.Evaluate(deviceHPR.pitch), rotAxis) * newRotation;
            newRotation = Quaternion.AngleAxis(_headingOffset, Vector3.up) * newRotation;
            // GAMEWORLD ROTATION & HPR: set final rotation and heading, pitch & roll
            gameWorldRotation = Quaternion.Slerp(gameWorldRotation, newRotation, smoothFactor);
        }




        #region UDUConsoleInput
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
        #endregion

        IEnumerator CalculateDirection()
        {
            isCoroutineRunning = true; // Set flag to true
            initialPos = pointerWorldPosition;  // Store the initial position
            yield return new WaitForSeconds(0.1f);  // Wait for 100ms

            // Now, calculate the direction vector and angle
            Vector3 directionVector = pointerWorldPosition - initialPos;
            float angleInRadians = Mathf.Atan2(directionVector.y, directionVector.x);
            float angleInDegrees = angleInRadians * Mathf.Rad2Deg;

            Debug.Log("Ondrej: Angle in Degrees: " + angleInDegrees);
            isCoroutineRunning = false; // Reset flag
        }



    }
}