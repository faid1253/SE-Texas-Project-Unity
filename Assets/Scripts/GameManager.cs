using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public bool visitedAlamo;
    public bool visitedStadium;
    public bool visitedRanch;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // ✅ Persists across scenes
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void MarkVisited(string location)
    {
        Debug.Log("MarkVisited called for " + location);

        if (location == "TheAlamoScene") visitedAlamo = true;
        if (location == "TheStadiumScene") visitedStadium = true;
        if (location == "TheRanchScene") visitedRanch = true;
    }
}