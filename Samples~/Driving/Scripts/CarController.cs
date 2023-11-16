using UnityEngine;

public class CarController : MonoBehaviour
{
    public float speed = 10; // Adjust this value to set the speed of the car

    private bool isMovingForward = false; // Boolean to control whether the car should move forward

    private Rigidbody rb;
    private float initalAngle;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();


        EventsSystemHandler.Instance.onTriggerPressTriggerButton += CarMove;
        EventsSystemHandler.Instance.onTriggerReleaseTriggerButton += CarStop;
        EventsSystemHandler.Instance.onTriggerPressSqueezeButton += GetStartingAngle;
    }

    // Update is called once per frame
    void Update()
    {
        if (isMovingForward)
        {
            MoveForward();
        }
        if (!isMovingForward)
        {
            Brake();
        }

        RotateCar();
    }

    private void RotateCar()
    {
        float angleY = Mathf.DeltaAngle(initalAngle, UDUGetters.GetMagneticHeading()) + 90;

        this.gameObject.transform.rotation = Quaternion.Euler(0f, angleY, 0f);
    }

    private void CarMove()
    {
        isMovingForward = true;
    }
    private void CarStop()
    {
        isMovingForward = false;
    }

    void MoveForward()
    {
        // Apply forward force to the car
        rb.velocity = transform.forward * speed;
    }

    void Brake()
    {
        rb.velocity = Vector3.zero;
    }


    private void GetStartingAngle()
    {
        initalAngle = UDUGetters.GetMagneticHeading();
    }
}
