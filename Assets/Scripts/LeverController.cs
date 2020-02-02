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
    public Sprite leftSide, RightSide;
    
    private float m_LastLeverSpeed = 0;

    private bool m_WhichSide = false;


    private void Awake()
    {
        m_Renderer = GetComponentInChildren<SpriteRenderer>();
    }

    private void Update()
    {
        MessagingManager<float>.SendMessage("LeverSpeedUpdated", leverSpeed);
        leverSpeed -= Time.deltaTime / 2.5f;
        if (leverSpeed < 0)
        {
            leverSpeed = 0;
        }
    }

    public void ChangeSides()
    {
        if (m_WhichSide)
        {
            m_Renderer.sprite = leftSide;
            m_WhichSide = false;
        }
        else
        {
            m_Renderer.sprite = RightSide;
            m_WhichSide = true;    
        }
    }
    
}

