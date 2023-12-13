using UDU;
using UnityEngine;

[RequireComponent (typeof(Rigidbody))]
public class BallController : MonoBehaviour
{
    [SerializeField]private float forceMagnitude; // Adjust this value to control the force applied.
    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        // Checking that we are connected to the UDU console
        if (!UDUGetters.IsConsoleConnected()) return;

        if (UDUGetters.IsTrackpadPressed())
        {
            // Calculate the new position based on the desired direction.
            Vector3 movement = new Vector3(UDUGetters.GetTrackpadCoordinates().y, 0f, -UDUGetters.GetTrackpadCoordinates().x).normalized * forceMagnitude;
            Vector3 newPosition = rb.position + movement * Time.fixedDeltaTime;

            // Use MovePosition to set the new position of the Rigidbody.
            rb.MovePosition(newPosition);
        }
        if (!UDUGetters.IsTrackpadPressed())
        {
            // If isTrackpadPressed is false, cancel the velocity to stop the object.
            rb.velocity = Vector3.zero;
        }
    }
}