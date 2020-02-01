using System;
using System.Collections;
using System.Collections.Generic;
using PxlSquad;
using UnityEngine;

public class BackgroundController : MonoBehaviour
{
    private Material m_Material;
    private static readonly int ScrollSpeed = Shader.PropertyToID("_ScrollSpeed");

    private void OnEnable()
    {
        MessagingManager<int>.RegisterObserver("LeverSpeedUpdated", UpdateBackgroundSpeed);
    }

    private void OnDisable()
    {
        MessagingManager<int>.DeregisterObserver("LevelSpeedUpdated", UpdateBackgroundSpeed);
    }


    private void Awake()
    {
        m_Material = GetComponentInChildren<SpriteRenderer>().sharedMaterial;
    }
    
    private void UpdateBackgroundSpeed(int parameter)
    {
        m_Material.SetFloat(ScrollSpeed, parameter / 2f);   
    }
}
