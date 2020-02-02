using System;
using System.Collections;
using PxlSquad;
using UnityEngine;
using UnityEngine.SceneManagement;
using Debug = UnityEngine.Debug;

public class GameStateManager : MonoBehaviour
{
    public float targetDistance;
    
    // Players
    public PlayerController p1, p2;
    
    //Wheels
    public WheelController[] wheels;
    
    // UI    
    public UIController uiController;
    public BackgroundController background;
    public float m_ElapsedTime = 0;

    private bool m_IsRunning, m_GameEnded = false;

    private void Start()
    {
        background.finalDistance = targetDistance;
        uiController.ShowStart();
    }

    private void OnEnable()
    {
        MessagingManager.RegisterObserver("Victory", Victory);
        MessagingManager.RegisterObserver("GameOver", GameOver);
    }

    private void OnDisable()
    {
        MessagingManager.DeregisterObserver("Victory", Victory);
        MessagingManager.DeregisterObserver("GameOver", GameOver);
    }

    private void Update()
    {
        if (m_GameEnded) return;
        if (!m_IsRunning) return;
        Debug.Log("Running true!!");
        m_ElapsedTime += Time.deltaTime;
        TimeSpan ts = System.TimeSpan.FromSeconds(m_ElapsedTime);
        uiController.timeText.text = $"Time: {ts.TotalSeconds}";
        uiController.distanceText.text = $"Distance: {background.displacement:F}";

        var wheelCount = 0;
        for (var i = 0; i < wheels.Length; i++)
        {
            if (wheels[i].CurrentState == WheelController.WheelState.Damaged)
            {
                wheelCount++;
            }
        }
        if(wheelCount == wheels.Length)
        {
            MessagingManager.SendMessage("GameOver");
            m_IsRunning = false;
        }
        
        if (background.displacement > targetDistance)
        {
            MessagingManager.SendMessage("Victory");
            m_IsRunning = false;
            Debug.Log("Set running to false");
        }
    }

    private void GameOver()
    {
        if (!m_IsRunning) return;
        Debug.Log("Game over!");
        uiController.ShowGameOver();
        StopGame();
    }
    
    private void Victory()
    {
        if (!m_IsRunning) return;
        Debug.Log("Victory!");
        uiController.ShowVictory();
        StopGame();
    }

    public void StartGame()
    {
        if (m_IsRunning || m_GameEnded) return;
        uiController.GameStart();
        p1.isRunning = true;
        p2.isRunning = true;
        m_IsRunning = true;
        MessagingManager<string>.SendMessage("PlayMusic", "Cafofo - MUSIC - Love");
        MessagingManager<string>.SendMessage("PlayAmbience", "Cafofo - AMB - City (Distant) with wind");
    }
    
    private void StopGame()
    {
        p1.isRunning = false;
        p2.isRunning = false;
        m_IsRunning = false;
        m_GameEnded = true;
    }

    public void RestartGame()
    {
        if (!m_GameEnded) return;
        StartCoroutine(RestartGameRoutine());
    }

    private IEnumerator RestartGameRoutine()
    {
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene("Main");
    }


}
