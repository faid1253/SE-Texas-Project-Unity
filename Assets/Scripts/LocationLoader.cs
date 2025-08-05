using UnityEngine;
using UnityEngine.SceneManagement;

public class LocationLoader : MonoBehaviour
{
    public void LoadLocationScene(string scene)
    {
        SceneManager.LoadScene(scene);
    }
}