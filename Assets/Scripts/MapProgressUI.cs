using UnityEngine;
using TMPro;

public class MapProgressUI : MonoBehaviour
{
    public TextMeshProUGUI progressText;

    void Update()
    {
        int visitedCount = 0;
        if (GameManager.Instance.visitedAlamo) visitedCount++;
        if (GameManager.Instance.visitedRanch) visitedCount++;
        if (GameManager.Instance.visitedStadium) visitedCount++;

        progressText.text = $"Visited: {visitedCount}/3";
    }
}