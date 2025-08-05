using UnityEngine;
using UnityEngine.SceneManagement;

public class HomeManager : MonoBehaviour
{
    public void LoadMap()
    {
        SceneManager.LoadScene("TheMapScene"); // Change to your map scene name
    }
}