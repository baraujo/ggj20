using System.Collections;
using System.Collections.Generic;
using PxlSquad;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    public float degressPerSecond = 360f;

    private int m_RotationFactor;
    
    private void OnEnable()
    {
        MessagingManager<int>.RegisterObserver("LeverSpeedUpdated", UpdateRotationSpeed);
    }
    
    private void OnDisable()
    {
        MessagingManager<int>.DeregisterObserver("LevelSpeedUpdated", UpdateRotationSpeed);
    }
    
    void Update()
    {
        transform.Rotate(0, 0, -degressPerSecond * Time.deltaTime * m_RotationFactor);
    }
    
    private void UpdateRotationSpeed(int parameter)
    {
        m_RotationFactor = parameter;
    }
}
