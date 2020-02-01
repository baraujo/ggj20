using UnityEngine;
using UnityEngine.SceneManagement;


public class AsyncLoader : MonoBehaviour
{
    public string sceneReference;
    private void Start()
    {
        SceneManager.LoadSceneAsync(sceneReference);
    }
}
