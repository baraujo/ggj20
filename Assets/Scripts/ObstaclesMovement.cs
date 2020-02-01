using System;
using System.Collections;
using System.Collections.Generic;
using PxlSquad;
using UnityEngine;
using UnityEngine.Experimental.TerrainAPI;

public class ObstaclesMovement : MonoBehaviour
{
    public float baseSpeed;
    private int m_SpeedFactor;

    private void OnEnable()
    {
        MessagingManager<int>.RegisterObserver("LeverSpeedUpdated", UpdateObstacleSpeed);
    }

    private void OnDisable()
    {
        MessagingManager<int>.DeregisterObserver("LeverSpeedUpdated", UpdateObstacleSpeed);
    }

    private void Update()
    {
        transform.addPositionX(-baseSpeed * m_SpeedFactor * Time.deltaTime);
    }

    private void UpdateObstacleSpeed(int parameter)
    {
        m_SpeedFactor = parameter;
    }
}
