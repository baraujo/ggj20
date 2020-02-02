using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class SpringBreakerController : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Wheel"))
        {
            var wheel = other.GetComponentInParent<WheelController>();
            if (wheel.m_CurrentState == WheelController.WheelState.Intact)
            {
                Destroy(gameObject);
            }
        }
    }
}
