using UnityEngine;

public class InteractableItem : MonoBehaviour
{
    [Tooltip("The text to display in the info panel when this item is interacted with.")]
    public string infoText;

    [Tooltip("The maximum distance from the player to interact with this item.")]
    public float interactDistance = 3f;

    private GameObject player;
    private bool playerInInteractionRange = false;

    void Start()
    {
        player = GameObject.FindWithTag("Player");
        if (player == null)
        {
            Debug.LogWarning($"InteractableItem: Player with tag 'Player' not found in scene for {gameObject.name}. Interaction will not work until player is present.");
        }
    }

    void Update()
    {
        if (player == null)
        {
            player = GameObject.FindWithTag("Player");
            if (player == null) return;
        }

        float currentDistance = Vector3.Distance(transform.position, player.transform.position);

        // --- NEW DEBUGGING: Log actual positions ---
        // Uncomment these for very detailed position logging (can be spammy)
        // Debug.Log($"[{gameObject.name}] Item Pos: {transform.position}, Player Pos: {player.transform.position}, Calculated Distance: {currentDistance:F2}");
        // --- END NEW DEBUGGING ---

        bool inRangeNow = currentDistance <= interactDistance;

        if (inRangeNow && !playerInInteractionRange)
        {
            playerInInteractionRange = true;
            Debug.Log($"[{gameObject.name}] Player entered interaction range. Distance: {currentDistance:F2}. Press 'E' to interact.");
        }
        else if (!inRangeNow && playerInInteractionRange)
        {
            playerInInteractionRange = false;
            Debug.Log($"[{gameObject.name}] Player exited interaction range. Distance: {currentDistance:F2}.");
            if (InfoPanelManager.Instance != null)
            {
                InfoPanelManager.Instance.HideInfo();
            }
        }

        if (playerInInteractionRange && Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log($"[{gameObject.name}] 'E' pressed while in range. Attempting to show info.");
            if (InfoPanelManager.Instance != null)
            {
                InfoPanelManager.Instance.ShowInfo(infoText);
            }
            else
            {
                Debug.LogError("InfoPanelManager.Instance is null! Make sure InfoPanelManager is in the scene and set up correctly.");
            }
            if (SceneTracker.Instance != null)
            {
                SceneTracker.Instance.ItemInteracted();
            }
        }
    }

    /// <summary>
    /// Draws a sphere in the editor to visualize the interactDistance.
    /// Select the GameObject with this script to see the gizmo.
    /// </summary>
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, interactDistance);

        // --- NEW DEBUGGING GIZMO: Visualize Player's position ---
        if (player != null)
        {
            Gizmos.color = Color.blue; // Blue for player's position
            Gizmos.DrawWireSphere(player.transform.position, 0.5f); // Small sphere at player's pivot
        }
        // --- END NEW DEBUGGING GIZMO ---

        if (player != null && playerInInteractionRange)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawLine(transform.position, player.transform.position);
        }
    }
}
