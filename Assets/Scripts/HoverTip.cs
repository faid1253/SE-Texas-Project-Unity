using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem; // Added for the new Input System

public class HoverTip : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public string tipToShow;

    private float timeToWait = 0.5f; // Fixed typo: timeTowait -> timeToWait

    public void OnPointerEnter(PointerEventData eventData)
    {
        StopAllCoroutines(); // Fixed typo: StopALLCoroutines -> StopAllCoroutines
        StartCoroutine(StartTimer());
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        StopAllCoroutines();
        // Null-safe invocation of the action
        HoverTipManager.OnMouseLoseFocus?.Invoke(); // Fixed syntax: (/; -> ?.Invoke()
    }

    private void ShowMessage() // Fixed typo: Showlessaget -> ShowMessage, removed extra parenthesis
    {
        // Check if a mouse is currently available in the new Input System
        if (Mouse.current != null)
        {
            // Invoke the OnMouseHover action in the manager with the tip text and current mouse position
            HoverTipManager.OnMouseHover?.Invoke(tipToShow, Mouse.current.position.ReadValue());
        }
        else
        {
            // Fallback or warning if Mouse.current is null (e.g., on platforms without a mouse)
            Debug.LogWarning("Mouse.current is null. Cannot get mouse position using Input System. Consider using eventData.position if this is a UI element.");
            // As an alternative for UI elements, you could use eventData.position from OnPointerEnter/Exit
            // if you need the exact pointer position that triggered the event.
            // HoverTipManager.OnMouseHover?.Invoke(tipToShow, eventData.position); // This would require passing eventData
        }
    }

    private IEnumerator StartTimer()
    {
        // Wait for the specified time before showing the message
        yield return new WaitForSeconds(timeToWait);
        ShowMessage();
    }
}