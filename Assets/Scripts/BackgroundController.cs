using System;
using System.Collections;
using System.Collections.Generic;
using PxlSquad;
using UnityEngine;

public class BackgroundController : MonoBehaviour
{
    private Material m_Material;
    [SerializeField]
    private float m_Displacement = 0;
    private float m_LeverSpeed;
    private static readonly int UvOffset = Shader.PropertyToID("_UVOffset");

    private void OnEnable()
    {
        MessagingManager<float>.RegisterObserver("LeverSpeedUpdated", UpdateBackgroundSpeed);
    }

    private void OnDisable()
    {
        MessagingManager<float>.DeregisterObserver("LevelSpeedUpdated", UpdateBackgroundSpeed);
    }

    private void Update()
    {
        m_Displacement += Time.deltaTime * m_LeverSpeed;
        m_Material.SetFloat(UvOffset, m_Displacement);
    }

    private void Awake()
    {
        m_Material = GetComponentInChildren<SpriteRenderer>().sharedMaterial;
    }
    
    private void UpdateBackgroundSpeed(float parameter)
    {
        m_LeverSpeed = parameter;
    }
}
