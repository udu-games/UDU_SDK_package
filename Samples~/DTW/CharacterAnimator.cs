using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimator : MonoBehaviour
{
    private Animator _animator;

    [SerializeField]
    private DTWUsageExampleClass dtwUsageExampleClass; // reference to the DTW4 component

    private void Start()
    {
        _animator = GetComponent<Animator>();

        // Get a reference to the DTW4 component on the same GameObject
        dtwUsageExampleClass = GetComponent<DTWUsageExampleClass>();

        // Check if the DTW4 component exists
        if (dtwUsageExampleClass != null)
        {
            dtwUsageExampleClass.OnGestureRecognized += HandleGestureRecognized;
        }
        else
        {
            Debug.LogWarning("DTW4 component not found on this GameObject.");
        }
    }

    private void OnDestroy()
    {
        if (dtwUsageExampleClass != null)
        {
            // Unsubscribe from the gesture recognized event
            dtwUsageExampleClass.OnGestureRecognized -= HandleGestureRecognized;
        }
    }

    private void HandleGestureRecognized(string gestureName)
    {
        Debug.Log("GESTURE NAME: " + gestureName);
        ResetAllTriggers();

        // Trigger an animation based on the recognized gesture
        switch (gestureName)
        {
            //case "BACK_SLASH_GESTURE":
            case "FH_TOPSPIN":

                _animator.SetTrigger("backSlash");
                break;

            //case "STAB_GESTURE":
            case "FH_FLAT":
                _animator.SetTrigger("stab");
                break;

            //case "FORWARD_SLASH_GESTURE":
            case "LOB":
                _animator.SetTrigger("forwardSlash");
                break;

            //case "DUNK_GESTURE":
            case "BH_TOPSPIN":
                _animator.SetTrigger("dunk");
                break;

            //case "UPPERCUT_GESTURE":
            case "BH_BACKSPIN":
                _animator.SetTrigger("uppercut");
                break;

            // Add more cases as needed for other gestures and their corresponding animations

            default:
                Debug.LogWarning("Unrecognized gesture for animation: " + gestureName);
                break;
        }
    }

    public void ResetAllTriggers()
    {
        // Reset all triggers
        foreach (AnimatorControllerParameter parameter in _animator.parameters)
        {
            if (parameter.type == AnimatorControllerParameterType.Trigger)
            {
                _animator.ResetTrigger(parameter.name);
            }
        }
    }
}
