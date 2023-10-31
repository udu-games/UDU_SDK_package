using UnityEngine;
using MSP_Input;
using System.Collections;
using TMPro;

namespace MSP_Input.Demo
{
    public class MSP_Sword : MonoBehaviour
    {
        private DataManipulation dataManipulation;
        private GameObject cube;
        //====================================================================

        Vector3 initialPos;
        Vector3 cubeWorldPosition;

        Vector3 lastPosition = Vector3.zero;
        private bool isCoroutineRunning = false;
        [SerializeField]
        public int PivotDistance = 5;

        [SerializeField]
        public TextMeshProUGUI infoTxt;
        

        private void Start()
        {
            dataManipulation = FindObjectOfType<DataManipulation>();
            cube = GameObject.FindWithTag("Cube");
        }

        //====================================================================


        long elapsedTime = 0;
        void Update()
        {
            Debug.Log("~~~~~~~~~ LATENCY: Update method entered. Timestamp: " + Time.time + " TimeSinceIdle: " + GlobalTimer.stopwatch.ElapsedMilliseconds);
            float distanceTraveled = Vector3.Distance(cube.transform.position, lastPosition);
            if (distanceTraveled > 0.05f)
            {
                lastPosition = cubeWorldPosition;
                Debug.Log("◘◘◘◘◘◘◘◘◘◘◘◘↓↓↓↓↓↓↓↓↓↓↓↓ LATENCY: THE CUBE HAS MOVED. Timestamp: " + Time.time + " TimeSinceIdle: " + GlobalTimer.stopwatch.ElapsedMilliseconds);
                elapsedTime = GlobalTimer.stopwatch.ElapsedMilliseconds;
            }
            infoTxt.SetText($"Position: {cube.transform.position.ToString()}, Elapsed time: {elapsedTime}, FPS: {Time.deltaTime}");

            Debug.Log("LATENCY: Update method, getting new rotation. Timestamp: " + Time.time + " TimeSinceIdle: " + GlobalTimer.stopwatch.ElapsedMilliseconds);
            Quaternion newRotation = dataManipulation.gameWorldRotation;
            Debug.Log("LATENCY: Update method, updating cube's rotation. Timestamp: " + Time.time + " TimeSinceIdle: " + GlobalTimer.stopwatch.ElapsedMilliseconds);
            transform.rotation = newRotation;
            Debug.Log("LATENCY: Update method, determining position relative to the pivot point. Timestamp: " + Time.time + " TimeSinceIdle: " + GlobalTimer.stopwatch.ElapsedMilliseconds);
            Vector3 positionRelativeToPivotPoint = new Vector3(0, PivotDistance, 0);
            Debug.Log("LATENCY: Update method, calculating tip position. Timestamp: " + Time.time + " TimeSinceIdle: " + GlobalTimer.stopwatch.ElapsedMilliseconds);
            Vector3 rotatedTipPosition = newRotation * positionRelativeToPivotPoint;
            Debug.Log("LATENCY: Update method, Updating cube's world position. Timestamp: " + Time.time + " TimeSinceIdle: " + GlobalTimer.stopwatch.ElapsedMilliseconds);
            // Update the cube's world position based on the sword's world position
            cubeWorldPosition = transform.position + rotatedTipPosition;
            Debug.Log("LATENCY: Update method, setting z to PivotDistance. Timestamp: " + Time.time + " TimeSinceIdle: " + GlobalTimer.stopwatch.ElapsedMilliseconds);
            cubeWorldPosition.z = PivotDistance;
            Debug.Log("LATENCY: Update method, updating cube's transform position: "+ cubeWorldPosition.ToString()+ " Current position is: "+ cube.transform.position.ToString() + ". Timestamp: " + Time.time + " TimeSinceIdle: " + GlobalTimer.stopwatch.ElapsedMilliseconds);
            cube.transform.position = cubeWorldPosition;
            Debug.Log("LATENCY: Update method, starting coroutine. Timestamp: " + Time.time + " TimeSinceIdle: " + GlobalTimer.stopwatch.ElapsedMilliseconds);
            if (UDUGetters.GetAcceleration().magnitude > 2000 && !isCoroutineRunning)
            {
                StartCoroutine(CalculateDirection());
            }
        }




        IEnumerator CalculateDirection()
        {
            isCoroutineRunning = true; // Set flag to true
            initialPos = cubeWorldPosition;  // Store the initial position
            yield return new WaitForSeconds(0.1f);  // Wait for 100ms

            // Now, calculate the direction vector and angle
            Vector3 directionVector = cubeWorldPosition - initialPos;
            float angleInRadians = Mathf.Atan2(directionVector.y, directionVector.x);
            float angleInDegrees = angleInRadians * Mathf.Rad2Deg;

            infoTxt.SetText($"Angle: {angleInDegrees}");
            Debug.Log("Ondrej: Angle in Degrees: " + angleInDegrees);
            isCoroutineRunning = false; // Reset flag
        }

        //====================================================================

    }
}