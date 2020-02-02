using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public string nextScene;

    private bool m_IsLoading = false;

    public void GoToNextScene()
    {
        if(m_IsLoading) return;
        m_IsLoading = true;
        SceneManager.LoadScene(nextScene);
    }
}
