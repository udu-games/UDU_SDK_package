using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CharacterAnimator : MonoBehaviour
{
    private Animator _animator;

    private void Start()
    {
        _animator = GetComponent<Animator>();

        EventsSystemHandler.Instance.onGestureRecognized += HandleGestureRecognized;
    }

    private void OnDestroy()
    {
        EventsSystemHandler.Instance.onGestureRecognized -= HandleGestureRecognized;
    }

    private void HandleGestureRecognized(string gestureName)
    {
        Debug.Log("GESTURE NAME: " + gestureName);
        ResetAllAnimationTriggers();

        // Trigger an animation based on the recognized gesture
        switch (gestureName)
        {
            case "BACK_SLASH":

                _animator.SetTrigger("backSlash");
                break;

            case "STAB":
                _animator.SetTrigger("stab");
                break;

            case "FORWARD_SLASH":
                _animator.SetTrigger("forwardSlash");
                break;

            case "DUNK":
                _animator.SetTrigger("dunk");
                break;

            case "UPPERCUT":
                _animator.SetTrigger("uppercut");
                break;

            // Add more cases as needed for other gestures and their corresponding animations

            default:
                Debug.LogWarning("Unrecognized gesture for animation: " + gestureName);
                break;
        }
    }

    public void ResetAllAnimationTriggers()
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
