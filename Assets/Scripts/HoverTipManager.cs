using System; // Required for Action delegate
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HoverTipManager : MonoBehaviour
{
    public TextMeshProUGUI tipText;
    public RectTransform tipWindow;

    // Static actions for other scripts to subscribe to
    public static Action<string, Vector2> OnMouseHover;
    public static Action OnMouseLoseFocus;

    private void OnEnable()
    {
        // Subscribe to the actions when the object is enabled
        OnMouseHover += ShowTip;
        OnMouseLoseFocus += HideTip;
    }

    private void OnDisable()
    {
        // Unsubscribe from the actions when the object is disabled
        OnMouseHover -= ShowTip;
        OnMouseLoseFocus -= HideTip;
    }

    private void Start()
    {
        // Initially hide the tip window
        HideTip();
    }

    private void ShowTip(string tip, Vector2 mousePos)
    {
        tipText.text = tip;
        // Adjust tipWindow size based on text content, with a max width of 200
        tipWindow.sizeDelta = new Vector2(tipText.preferredWidth > 200 ? 200 : tipText.preferredWidth, tipText.preferredHeight);

        tipWindow.gameObject.SetActive(true);

        // --- POSITIONING FIX ---
        // Instead of multiplying by the tip's width, add a small fixed offset.
        // 10f is an example; you can adjust this value to control the gap.
        float offsetX = 10f; // Small offset to the right of the mouse cursor
        float offsetY = 10f; // Small offset above/below the mouse cursor (optional, adjust as needed)

        // Position the tip window relative to the mouse position
        // This places the tip's pivot (usually center or top-left) at mousePos + offset
        tipWindow.transform.position = new Vector2(mousePos.x + offsetX, mousePos.y + offsetY);

        // Optional: Ensure the tooltip stays within screen bounds
        // This is more complex and depends on your canvas setup (Screen Space - Overlay, Camera, World)
        // For Screen Space - Overlay, you might check Camera.main.pixelWidth/Height
        // Example (simplified, may need refinement for your specific canvas):
        Vector3[] corners = new Vector3[4];
        tipWindow.GetWorldCorners(corners);
        float tipWidth = corners[2].x - corners[0].x;
        float tipHeight = corners[1].y - corners[0].y;

        Vector2 currentPos = tipWindow.transform.position;
        float screenWidth = Screen.width;
        float screenHeight = Screen.height;

        // Prevent going off right edge
        if (currentPos.x + tipWidth / 2 > screenWidth)
        {
            currentPos.x = screenWidth - tipWidth / 2;
        }
        // Prevent going off left edge
        if (currentPos.x - tipWidth / 2 < 0)
        {
            currentPos.x = tipWidth / 2;
        }
        // Prevent going off top edge
        if (currentPos.y + tipHeight / 2 > screenHeight)
        {
            currentPos.y = screenHeight - tipHeight / 2;
        }
        // Prevent going off bottom edge
        if (currentPos.y - tipHeight / 2 < 0)
        {
            currentPos.y = tipHeight / 2;
        }
        tipWindow.transform.position = currentPos;
    }

    private void HideTip()
    {
        tipText.text = default;
        tipWindow.gameObject.SetActive(false);
    }
}