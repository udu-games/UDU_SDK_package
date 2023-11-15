using UnityEngine;

[RequireComponent (typeof(Rigidbody))]
public class CarController : MonoBehaviour
{
    [SerializeField]private float forceMagnitude; // Adjust this value to control the force applied.
    private Vector2 trackpadDirection;     // The desired direction to move the ball.
    private Vector2 trackpadMiddleCoordinates = new Vector2(950f, 600f);     // Coordinate of the trackpad middle, this can different depending on the device

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
            Vector3 movement = new Vector3(TrackpadDirection().x, 0f, -TrackpadDirection().y).normalized * forceMagnitude;
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

    private Vector2 TrackpadDirection()
    {
        // Determine direction using the middle coordinates of the trackpad and the current coordinate of the touched area
        trackpadDirection = new Vector2(UDUGetters.GetTrackpadCoordinates().x, UDUGetters.GetTrackpadCoordinates().y) - trackpadMiddleCoordinates;
        return trackpadDirection;
    }
}
