using UnityEngine;
using TMPro; // Required for TextMeshProUGUI
using UnityEngine.UI; // Required for Button

public class InfoPanelManager : MonoBehaviour
{
    // --- Singleton Pattern ---
    // This ensures only one instance of the manager exists and is easily accessible.
    public static InfoPanelManager Instance { get; private set; }

    [Header("UI References")]
    [Tooltip("Drag your InfoPanel GameObject (the parent panel) here from the Hierarchy.")]
    public GameObject infoPanelGameObject;

    [Tooltip("Drag the TextMeshProUGUI component from inside your InfoPanel here.")]
    public TextMeshProUGUI infoTextComponent;

    [Tooltip("Optional: Drag the Close Button from inside your InfoPanel here.")]
    public Button closeButton;

    void Awake()
    {
        // --- Robust Singleton Initialization ---
        if (Instance == null)
        {
            Instance = this;
            // Optional: If this manager should persist across scene loads, uncomment the line below.
            // DontDestroyOnLoad(gameObject);
        }
        else if (Instance != this)
        {
            // If another instance already exists, destroy this one.
            Debug.LogWarning($"InfoPanelManager: Duplicate instance found on '{gameObject.name}'. Destroying this duplicate.");
            Destroy(gameObject);
            return; // Stop further execution for this duplicate.
        }
        // --- End Robust Singleton Initialization ---


        // --- ENHANCED DEBUGGING: Verify UI References at Awake ---
        Debug.Log("--- InfoPanelManager Awake Debugging ---");
        Debug.Log($"InfoPanelManager instance running Awake: {gameObject.name}"); // Log which instance is running Awake

        if (infoPanelGameObject == null)
        {
            Debug.LogError("Awake: 'Info Panel GameObject' is NOT assigned in the Inspector! UI will not show.");
        }
        else
        {
            Debug.Log($"Awake: 'Info Panel GameObject' assigned: {infoPanelGameObject.name}");
        }

        if (infoTextComponent == null)
        {
            Debug.LogError("Awake: 'Info Text Component' is NOT assigned in the Inspector! Text will not show.");
        }
        else
        {
            Debug.Log($"Awake: 'Info Text Component' assigned: {infoTextComponent.name}");
        }

        if (closeButton == null)
        {
            Debug.LogWarning("Awake: 'Close Button' is NOT assigned in the Inspector. Close functionality will not work.");
        }
        else
        {
            Debug.Log($"Awake: 'Close Button' assigned: {closeButton.name}");
        }
        Debug.Log("--------------------------------------");
        // --- END ENHANCED DEBUGGING ---

        // Ensure the panel is initially hidden when the game starts.
        if (infoPanelGameObject != null)
        {
            infoPanelGameObject.SetActive(false);
        }

        // Add a listener to the close button if it's assigned.
        if (closeButton != null)
        {
            closeButton.onClick.AddListener(HideInfo);
        }
    }

    /// <summary>
    /// Displays the info panel with the given text.
    /// </summary>
    /// <param name="text">The text to show in the info panel.</param>
    public void ShowInfo(string text)
    {
        // --- ENHANCED DEBUGGING: Verify UI References at ShowInfo call ---
        Debug.Log("--- InfoPanelManager ShowInfo Debugging ---");
        Debug.Log($"ShowInfo called on instance: {gameObject.name}"); // Log which instance is handling the call

        if (infoPanelGameObject == null)
        {
            Debug.LogError("ShowInfo: 'Info Panel GameObject' is NULL at runtime! UI will not show.");
        }
        else
        {
            Debug.Log($"ShowInfo: 'Info Panel GameObject' is assigned ({infoPanelGameObject.name}).");
        }

        if (infoTextComponent == null)
        {
            Debug.LogError("ShowInfo: 'Info Text Component' is NULL at runtime! Text will not show.");
        }
        else
        {
            Debug.Log($"ShowInfo: 'Info Text Component' is assigned ({infoTextComponent.name}).");
        }
        Debug.Log("--------------------------------------");
        // --- END ENHANCED DEBUGGING ---

        // This check is still important for runtime calls, but Awake check helps initial setup.
        if (infoPanelGameObject == null || infoTextComponent == null)
        {
            Debug.LogError("InfoPanelManager: UI references (Info Panel GameObject or Text Component) are missing at runtime! Please assign them in the Inspector.");
            return; // Exit if references are null to prevent NullReferenceException
        }

        infoTextComponent.text = text;
        infoPanelGameObject.SetActive(true);
        Debug.Log($"Info Panel activated with text: '{text}'");
    }

    /// <summary>
    /// Hides the info panel.
    /// </summary>
    public void HideInfo()
    {
        if (infoPanelGameObject != null)
        {
            infoPanelGameObject.SetActive(false);
            Debug.Log("Info Panel hidden.");
        }
        else
        {
            Debug.LogWarning("HideInfo: 'Info Panel GameObject' is null. Cannot hide panel.");
        }
    }

    void OnDestroy()
    {
        // Log if this instance is being destroyed, which could explain null references if it's the 'Instance'.
        Debug.LogWarning($"InfoPanelManager: Instance '{gameObject.name}' is being destroyed.");
        if (Instance == this)
        {
            Instance = null; // Clear the static reference if this was the active instance.
        }

        if (closeButton != null)
        {
            closeButton.onClick.RemoveListener(HideInfo);
        }
    }
}
