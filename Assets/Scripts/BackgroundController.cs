using System;
using System.Collections;
using System.Collections.Generic;
using PxlSquad;
using UnityEngine;

public class BackgroundController : MonoBehaviour
{
    public float displacement = 0;
    public float finalDistance = 50.1f;
    private Material m_Material;
    private float m_LeverSpeed;
    private bool m_IsRunning = true;
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
        displacement += Time.deltaTime * m_LeverSpeed;
        m_Material.SetFloat(UvOffset, displacement);

        if (displacement > finalDistance && m_IsRunning)
        {
            m_IsRunning = false;
        }
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
