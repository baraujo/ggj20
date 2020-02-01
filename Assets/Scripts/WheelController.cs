using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class WheelController : MonoBehaviour
{
    [Serializable]
    public enum WheelState
    {
        Intact,
        Damaged
    }
    
    [HideInInspector]
    public PlayerController currentPlayer;
    public Sprite intactSprite, damagedSprite;
    public float repairRate;
    public bool repairInProgress;
    
    [SerializeField] private WheelState m_CurrentState;
    [SerializeField] private float m_damagePercentage;
    
    public WheelState CurrentState
    {
        get => m_CurrentState;
        set { m_CurrentState = value; UpdateState(); }
    }

    private SpriteRenderer m_Renderer;

    private void Start()
    {
        UpdateState(); 
    }

    private void Awake()
    {
        m_Renderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (repairInProgress)
        {
            PerformRepair(Time.deltaTime);
        }
    }

    [ContextMenu("Update sprite")]
    public void UpdateState()
    {
        if (m_CurrentState == WheelState.Intact)
        {
            m_Renderer.sprite = intactSprite;
            m_damagePercentage = 0;
        }
        else
        {
            m_Renderer.sprite = damagedSprite;
            m_damagePercentage = 100;
        }
    }

    private void PerformRepair(float repairAmount)
    {
        if (m_damagePercentage >= 0)
        {
            m_damagePercentage -= repairRate * repairAmount;
        }
        else if (m_CurrentState == WheelState.Damaged)
        {
            m_CurrentState = WheelState.Intact;
            UpdateState();
        }
    }
}
