using System;
using System.Collections;
using System.Collections.Generic;
using PxlSquad;
using UnityEngine;

public class LeverController : MonoBehaviour
{
    public float leverSpeed = 0;
    public float actualSpeed = 0;
    public SpriteRenderer m_Renderer;
    public Sprite leftSide, rightSide;
    private bool m_WhichSide = false;
    private bool m_IsRunning = true;

    
    private void OnEnable()
    {
        MessagingManager.RegisterObserver("Victory", StopLever);
        MessagingManager.RegisterObserver("GameOver", StopLever);
    }

    private void StopLever()
    {
        m_IsRunning = false;
    }

    private void OnDisable()
    {
        MessagingManager.DeregisterObserver("Victory", StopLever);
        MessagingManager.DeregisterObserver("GameOver", StopLever);
    }
    private void Awake()
    {
        m_Renderer = GetComponentInChildren<SpriteRenderer>();
    }

    private void Update()
    {
        if (m_IsRunning)
        {
            leverSpeed -= Time.deltaTime / 2.5f;
            if (leverSpeed < 0)
            {
                leverSpeed = 0;
            }
        }
        else
        {
            leverSpeed -= Time.deltaTime * 5;
            if (leverSpeed < 0)
            {
                leverSpeed = 0;
            }
        }
        MessagingManager<float>.SendMessage("LeverSpeedUpdated", leverSpeed);
    }

    public void ChangeSides()
    {
        if (!m_IsRunning) return;
        if (m_WhichSide)
        {
            m_Renderer.sprite = leftSide;
            m_WhichSide = false;
        }
        else
        {
            m_Renderer.sprite = rightSide;
            m_WhichSide = true;    
        }
    }
    
}

