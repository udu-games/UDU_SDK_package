using MSP_Input;
using System.Diagnostics;
using UnityEngine;
using Debug = UnityEngine.Debug;

// Class responsible for translating controller data into 2D space data
public class DataManipulation : MonoBehaviour
{
    private Quaternion _consoleCurrentQuat;
    private HeadingPitchRoll deviceHPR;
    private float smoothFactor = 1f;
    private Quaternion gyroRot = Quaternion.identity;
    private const float sqrthalf = 0.707106781186548f;
    private float _headingOffset = -180f;
    private FloatMinMax pitchOffset = new FloatMinMax(0f, -70f, 70f, -90f, 90f);
    public Quaternion gameWorldRotation = Quaternion.identity;
   
    // Cached values
    private AnimationCurve devicePitchAdjustmentCurve;

    private void Start()
    {
        pitchOffset.ValidateData();
        // Cache the animation curve
        devicePitchAdjustmentCurve = new AnimationCurve(new Keyframe(-90f, 0f), new Keyframe(pitchOffset.value, -pitchOffset.value), new Keyframe(90f, 0f));
    }

    void Update()
    {

        Quaternion newRotation;
        Quaternion gyroQuat = new Quaternion(0.0f, sqrthalf, -sqrthalf, 0.0f) * _consoleCurrentQuat;
        deviceHPR = HeadingPitchRoll.FromQuaternionTopDownView(gyroRot, true);
        newRotation = gyroQuat;
        // Modulo for heading offset
        _headingOffset = (_headingOffset + 180) % 360 - 180;
        Vector3 rotAxis = Vector3.Cross(Vector3.up, newRotation * Vector3.forward);
        newRotation = Quaternion.AngleAxis(devicePitchAdjustmentCurve.Evaluate(deviceHPR.pitch), rotAxis) * newRotation;
        newRotation = Quaternion.AngleAxis(_headingOffset, Vector3.up) * newRotation;
        // GAMEWORLD ROTATION & HPR: set final rotation and heading, pitch & roll
        gameWorldRotation = Quaternion.Slerp(gameWorldRotation, newRotation, smoothFactor);
    }

}
    // DONT DELETE BELOW - in case different screen orientation is needed, use:
   
  /*    switch (baseOrientation)
        {
            case BaseOrientation.LandscapeLeft:
                gyroRot = new Quaternion(0.5f, 0.5f, -0.5f, 0.5f) * _consoleCurrentQuat * new Quaternion(0f, 0f, 1f, 0f);
                gyroHPR = HeadingPitchRoll.FromQuaternion(gyroRot, true);
                break;
            case BaseOrientation.Portrait:
                gyroRot = new Quaternion(0.0f, sqrthalf, -sqrthalf, 0.0f) * _consoleCurrentQuat * new Quaternion(0f, 0f, 1f, 0f);
                gyroHPR = HeadingPitchRoll.FromQuaternion(gyroRot, true);
                break;
            case BaseOrientation.LandscapeRight:
                gyroRot = new Quaternion(-0.5f, 0.5f, -0.5f, -0.5f) * _consoleCurrentQuat * new Quaternion(0f, 0f, 1f, 0f);
                gyroHPR = HeadingPitchRoll.FromQuaternion(gyroRot, true);
                break;
            case BaseOrientation.LandscapeLeft_FaceUp:
                gyroRot = new Quaternion(0.5f, 0.5f, -0.5f, 0.5f) * _consoleCurrentQuat * new Quaternion(0f, 0f, 1f, 0f);
                gyroHPR = HeadingPitchRoll.FromQuaternionTopDownView(gyroRot, true);
                break;
            case BaseOrientation.Portrait_FaceUp:
                gyroRot = new Quaternion(0.0f, sqrthalf, -sqrthalf, 0.0f) * _consoleCurrentQuat*//* * new Quaternion(0f, 0f, 1f, 0f)*//*;
                gyroHPR = HeadingPitchRoll.FromQuaternionTopDownView(gyroRot, true);
                break;
            case BaseOrientation.LandscapeRight_FaceUp:
                gyroRot = new Quaternion(-0.5f, 0.5f, -0.5f, -0.5f) * _consoleCurrentQuat* new Quaternion(0f, 0f, 1f, 0f);
                gyroHPR = HeadingPitchRoll.FromQuaternionTopDownView(gyroRot, true);
                break;
            default:
                gyroRot = new Quaternion(0.0f, sqrthalf, -sqrthalf, 0.0f) * _consoleCurrentQuat* new Quaternion(0f, 0f, 1f, 0f);
                gyroHPR = HeadingPitchRoll.FromQuaternion(gyroRot, true);
                break;
        }*/



