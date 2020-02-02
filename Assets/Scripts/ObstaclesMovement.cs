using System;
using System.Collections;
using System.Collections.Generic;
using PxlSquad;
using UnityEngine;
using UnityEngine.Experimental.TerrainAPI;

public class ObstaclesMovement : MonoBehaviour
{
    public float baseSpeed;
    private float m_SpeedFactor;

    private void OnEnable()
    {
        MessagingManager<float>.RegisterObserver("LeverSpeedUpdated", UpdateObstacleSpeed);
    }

    private void OnDisable()
    {
        MessagingManager<float>.DeregisterObserver("LeverSpeedUpdated", UpdateObstacleSpeed);
    }

    private void Update()
    {
        transform.addPositionX(-baseSpeed * m_SpeedFactor * Time.deltaTime);
    }

    private void UpdateObstacleSpeed(float parameter) {
        m_SpeedFactor = parameter;
    }
}
