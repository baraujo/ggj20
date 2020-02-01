using System;
using System.Collections;
using System.Collections.Generic;
using PxlSquad;
using UnityEngine;

public class LeverController : MonoBehaviour
{
    public int leverSpeed = 0;
    private int m_LastLeverSpeed = 0;

    private void Update()
    {
        if (leverSpeed != m_LastLeverSpeed)
        {
            Debug.Log($"New lever speed: {leverSpeed}");
            m_LastLeverSpeed = leverSpeed;
            MessagingManager<int>.SendMessage("LeverSpeedUpdated", leverSpeed);
        }
    }
}
