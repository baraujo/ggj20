using System;
using UnityEngine;

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
    public GameObject intactSprite, damagedSprite, wrenchSprite;
    public float repairRate;
    public bool repairInProgress;
    
    [Range(0f, 100f)]
    public float damagePercentage;
    
    public WheelState m_CurrentState;

    public WheelState CurrentState
    {
        get => m_CurrentState;
        set { m_CurrentState = value; UpdateState(); }
    }

    private SpriteRenderer m_IntactSpriteRenderer;
    private static readonly int Fill = Shader.PropertyToID("_Fill");

    private void Awake()
    {
        m_IntactSpriteRenderer = intactSprite.GetComponent<SpriteRenderer>();
    }
   
    private void Start()
    {
        UpdateState(); 
    }

    private void Update()
    {
        if (repairInProgress)
        {
            PerformRepair(Time.deltaTime);
        }
        UpdateIntactSpriteFill(damagePercentage / 100f);
    }
    
    private void UpdateIntactSpriteFill(float value)
    {
        m_IntactSpriteRenderer.sharedMaterial.SetFloat(Fill, 1 - value);
    }
    
    [ContextMenu("Update sprite")]
    public void UpdateState()
    {
        damagePercentage = m_CurrentState == WheelState.Intact ? 0 : 100;
        if (m_CurrentState == WheelState.Damaged)
        {
            wrenchSprite.SetActive(true);
        }
        else
        {
            wrenchSprite.SetActive(false);
        }
        
    }

    private void PerformRepair(float repairAmount)
    {
        if (damagePercentage >= 10)
        {
            damagePercentage -= repairRate * repairAmount;
        }
        else if (m_CurrentState == WheelState.Damaged)
        {
            m_CurrentState = WheelState.Intact;
            UpdateState();
        }
    }

    public void TakeDamage(){
        if(m_CurrentState == WheelState.Intact){
            m_CurrentState = WheelState.Damaged;
            UpdateState();
        }
    }
}
