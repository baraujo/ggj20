using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public GameObject startCanvas, gameOverCanvas, victoryCanvas, gameCanvas;
    public Text timeText, distanceText;
    
    public void ShowStart()
    {
        startCanvas.SetActive(true);
    }
    
    public void ShowGameOver()
    {
        gameOverCanvas.SetActive(true);
    }
    
    public void ShowVictory()
    {
        victoryCanvas.SetActive(true);
    }

    public void GameStart()
    {
        startCanvas.SetActive(false);
        gameOverCanvas.SetActive(false);
        victoryCanvas.SetActive(false);
        gameCanvas.SetActive(true);
    }
}
