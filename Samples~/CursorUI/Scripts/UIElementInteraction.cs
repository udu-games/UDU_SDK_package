using UDU;
using UnityEngine;

public class UIElementInteraction : MonoBehaviour
{
    private RectTransform uiElement;

    private void Start()
    {
        EventsSystemHandler.Instance.onTriggerPressTriggerButton += IsPointerOverUI;
        uiElement = GetComponent<RectTransform>();
    }

    private void IsPointerOverUI()
    {
        RaycastUtilities.ClickButtonAtTargetPosition(uiElement.position);
    }
}



