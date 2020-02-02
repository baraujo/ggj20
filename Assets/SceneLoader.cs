using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public string nextScene;

    public void GoToNextScene()
    {
        SceneManager.LoadScene(nextScene);
    }
}
