using UnityEngine;
using MSP_Input;
using System.Collections;
using System;
using UDU;

namespace MSP_Input.Demo
{
    public class MSP_Sword : MonoBehaviour
    {
        [SerializeField] private GameObject gameObjectToMove;
        [SerializeField] private GameObject pointer;

        Vector3 initialPos;
        Vector3 pointerWorldPosition;

        private float PivotDistance = 10f;

        #region DataManipulation
        private Quaternion _consoleCurrentQuat;
        public Quaternion gameWorldRotation = Quaternion.identity;

        private bool isTriggerButtonPressed;

        #endregion

        #region UDUConsoleInput
        [HideInInspector] public Quaternion currentOrientation;

        [HideInInspector] public Vector3 angularVelocity;

        private GameObject pivotPoint;
        private Vector3 pivotPointPosition;
        private Quaternion uduOrientation;
        private Quaternion gyroOffset;
        #endregion

        private void Awake()
        {
            pivotPoint = this.gameObject;
        }


        private void Start()
        {
            SubscribeToBLEEvents();

            // Can be called in the update method, if it's supposed to change the position
            pivotPointPosition = pivotPoint.transform.position;

            angularVelocity = Vector3.zero;
            currentOrientation = Quaternion.identity;
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
            EventsSystemHandler.Instance.onTriggerPressSqueezeButton -= ResetHeading;
        }

        private void OnConsoleTriggerButtonPress()
        {
            isTriggerButtonPressed = true;
        }

        private void OnConsoleTriggerButtonRelease()
        {
            isTriggerButtonPressed = false;
        }

        private GameObject selectedObject;


        void Update()
        {
            // Moved UDUConsoleInput and DataManipulation logic into methods for cleaner code
            HandleUDUConsoleInput();

            gameWorldRotation = ConsoleIMUToUnityRotation(_consoleCurrentQuat);

            ApplyRotationToObject(gameObjectToMove, gameWorldRotation);

            UpdatePointerDirection();
            CastRayAndUpdatePointer();

            UpdateSelectedObjectPosition();
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
            }
            else
            {
                pointer.transform.position = pivotPointPosition + (pointerWorldPosition - pivotPointPosition).normalized * raycastMaxRange;
            }
        }

        void UpdateSelectedObjectPosition()
        {
            // If an object is selected, position it a fixed distance from the pivot
            if (selectedObject != null)
            {
                Vector3 direction = (pointerWorldPosition - pivotPointPosition).normalized;
                Vector3 offsetPosition = pivotPointPosition + direction * PivotDistance;
                selectedObject.transform.position = offsetPosition;
            }
        }

        // CORE -  Data manipulation method to map Unity coordinate system to UDU IMU coordinate system
        void HandleUDUConsoleInput()
        {
            if (UDUGetters.IsConsoleConnected())
            {
                Quaternion gyroRotation = UDUGetters.GetOrientation();
                // Apply the offset to the gyroscope rotation
                Quaternion correctedRotation = gyroOffset * gyroRotation;
                _consoleCurrentQuat = correctedRotation;
            }
        }

        // CORE - Data manipulation method to map Unity coordinate system to UDU IMU coordinate system
        Quaternion ConsoleIMUToUnityRotation(Quaternion consoleCurrentQuat)
        {
            Quaternion gameWorldRotation = Quaternion.identity;
            if (consoleCurrentQuat != Quaternion.identity)
            {
                // Unity axis/orientation doesn't match the one from IMU in UDU - need to realign
                Quaternion correctiveRotation = Quaternion.Euler(90, 0, 0);
                Quaternion adjustedRotation = correctiveRotation * consoleCurrentQuat;

                // Calculate the forward and up vectors from the adjusted rotation.
                Vector3 forward = adjustedRotation * Vector3.forward;
                Vector3 up = adjustedRotation * Vector3.up;

                // Apply the rotation to the cube.
                gameWorldRotation = Quaternion.LookRotation(forward, up);
            }
            return gameWorldRotation;
        }


        // CORE -  Reset the position - align UDU orientation with Unity one
        public void SetCurrentHeadingQuaternion()
        {
            uduOrientation = UDUGetters.GetOrientation();

            // Extract the forward direction from the initial rotation
            Vector3 forward = uduOrientation * Vector3.forward;

            // Flatten the forward direction onto the XZ plane (removing tilt around Z-axis)
            forward.y = 0;
            forward.Normalize(); // Important to normalize the vector after altering it

            // Create a new quaternion looking in the flattened forward direction
            // This quaternion represents the new orientation without the Z-axis rotation
            gyroOffset = Quaternion.LookRotation(forward, Vector3.up) * Quaternion.Inverse(uduOrientation);

        }

        // Apply the world rotation to some object - this is what they would expect to be able to call
        void ApplyRotationToObject(GameObject gameObject, Quaternion rotation)
        {
            gameObject.SetActive(true);
            gameObject.transform.rotation = rotation;
        }


        #region UDUConsoleInput

        // Method responsible for realigning/resetting the heading, camera and cube
        public void ResetHeading()
        {
            if (UDUGetters.IsConsoleConnected())
            {
                //OrientationSensors.SetHeading(0f);
                SetCurrentHeadingQuaternion();
            }
        }
        #endregion

    }
}
