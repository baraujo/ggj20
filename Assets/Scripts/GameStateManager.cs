using System.Collections;
using System.Collections.Generic;
using PxlSquad;
using UnityEngine;

public class GameStateManager : MonoBehaviour
{
    // Players
    public PlayerController p1, p2;
    
    // UI    
    public Canvas gameStartCanvas;

    private bool m_IsRunning;
    
    public void StartGame()
    {
        if (m_IsRunning) return;
        
        gameStartCanvas.gameObject.SetActive(false);
        p1.isRunning = true;
        p2.isRunning = true;
        m_IsRunning = true;
        MessagingManager<string>.SendMessage("PlayMusic", "Background");
    }
}
