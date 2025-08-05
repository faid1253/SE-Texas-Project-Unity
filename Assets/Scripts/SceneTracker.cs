using UnityEngine;

public class SceneTracker : MonoBehaviour
{
    public static SceneTracker Instance;

    private int totalItems;
    private int interactedCount;

    private QuizManager quizManager;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        // ✅ Use new API
        totalItems = Object.FindObjectsByType<InteractableItem>(FindObjectsSortMode.None).Length;

        interactedCount = 0;

        // ✅ Use new API for single object
        quizManager = Object.FindFirstObjectByType<QuizManager>();
    }

    public void ItemInteracted()
    {
        interactedCount++;
        Debug.Log($"Item Interacted! {interactedCount}/{totalItems}");

        if (interactedCount > totalItems )
        {
            Debug.Log("All items found! Starting quiz...");
            if (quizManager != null)
            {
                quizManager.StartQuiz();
            }
        }
    }
}