using UnityEngine;
using System;

public class EventsSystemHandler : Singleton<EventsSystemHandler>
{
    // GENERAL CONSOLE //
    public event Action onEventEnded; 
    public void EventEnded()
    {
        if (onEventEnded != null)
        {
            onEventEnded();
        }
    }

    public event Action onTriggerPressTriggerButton;
    public void TriggerPressTriggerButton()
    {
        if (onTriggerPressTriggerButton != null)
        {
            onTriggerPressTriggerButton();
        }
    }

    public event Action onTriggerPressSqueezeButton;
    public void TriggerPressSqueezeButton()
    {
        if (onTriggerPressSqueezeButton != null)
        {
            onTriggerPressSqueezeButton();
        }
    }

    public event Action<bool, Vector3> onTriggerPressTrackpadButton;
    public void TriggerPressTrackpadButton(bool firstPress, Vector3 trackpadCoordinates)
    {
        if (onTriggerPressTrackpadButton != null)
        {
            onTriggerPressTrackpadButton(firstPress, trackpadCoordinates);
        }
    }

    public event Action onTriggerReleaseTriggerButton;
    public void TriggerReleaseTriggerButton()
    {
        if (onTriggerReleaseTriggerButton != null)
        {
            onTriggerReleaseTriggerButton();
        }
    }

    public event Action onTriggerReleaseSqueezeButton;
    public void TriggerReleaseSqueezeButton()
    {
        if (onTriggerReleaseSqueezeButton != null)
        {
            onTriggerReleaseSqueezeButton();
        }
    }

    public event Action onTriggerReleaseTrackpadButton;
    public void TriggerReleaseTrackpadButton()
    {
        if (onTriggerReleaseTrackpadButton != null)
        {
            onTriggerReleaseTrackpadButton();
        }
    }

    public event Action<bool> onConsoleConnect;
    public void ConsoleConnect(bool isConnected)
    {
        if (onConsoleConnect != null)
        {
            onConsoleConnect(isConnected);
        }
    }

    public event Action<string> onGestureRecognized;
    public void GestureRecognized(string recognizedGestureName)
    {
        if (onGestureRecognized != null)
        {
            onGestureRecognized(recognizedGestureName);
        }
    }
}