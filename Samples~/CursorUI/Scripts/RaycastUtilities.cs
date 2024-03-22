using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public static class RaycastUtilities

{
    public static void ClickButtonAtTargetPosition(Vector2 screenPos)
    {
        PointerIsOverUI(screenPos)?.GetComponent<UnityEngine.UI.Button>()?.onClick.Invoke();
    }

    static GameObject PointerIsOverUI(Vector2 screenPos)
    {
        GameObject hitObject = UIRaycast(ScreenPosToPointerData(screenPos));
        if (hitObject != null && hitObject.layer == LayerMask.NameToLayer("UI")) return hitObject;
        else return null;
    }

    static GameObject UIRaycast(PointerEventData pointerData)
    {
        var results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(pointerData, results);

        return results.Count < 1 ? null : results[0].gameObject;
    }

    static PointerEventData ScreenPosToPointerData(Vector2 screenPos) => new(EventSystem.current) { position = screenPos };
}
