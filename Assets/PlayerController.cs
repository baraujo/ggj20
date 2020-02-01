using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Prime31;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float m_Speed;
    
    public Vector3 m_Velocity;
    private Vector3 m_VelocityRef;
    private Vector2 m_NormalizedVelocity;
    private Rigidbody2D m_Rigidbody;

    private void Awake()
    {
        m_Rigidbody = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        //var newPosition = Vector3.SmoothDamp(transform.position, transform.position + m_Velocity * Time.fixedDeltaTime, ref m_VelocityRef, 0.05f);
            m_Rigidbody.MovePosition(transform.position + m_Velocity * Time.fixedDeltaTime);
    }

    private void OnTriggerExitEvent(Collider2D obj)
    {
    }

    private void OnTriggerEnterEvent(Collider2D obj)
    {
    }

    public void Move(InputAction.CallbackContext value)
    {
        m_NormalizedVelocity = value.ReadValue<Vector2>() * m_Speed;
        m_Velocity.x = m_NormalizedVelocity.x;
        m_Velocity.y = m_NormalizedVelocity.y;
    }

    public void Action(InputAction.CallbackContext value)
    {
        Debug.Log($"Action: {name}");
    }
}
