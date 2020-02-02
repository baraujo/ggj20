using System;
using System.Collections;
using System.Collections.Generic;
using PxlSquad;
using UnityEngine;

public class LeverController : MonoBehaviour
{
    public float leverSpeed = 0;
    public float actualSpeed = 0;
    private float m_LastLeverSpeed = 0;

    private void Update()
    {
        if (Math.Abs(leverSpeed - m_LastLeverSpeed) > 0.01f)
        {
            Debug.Log($"New lever speed: {leverSpeed}");
            m_LastLeverSpeed = leverSpeed;
        }
        
        actualSpeed = Mathf.Lerp(actualSpeed, leverSpeed, 0.01f);
        MessagingManager<float>.SendMessage("LeverSpeedUpdated", actualSpeed);
    }
}
