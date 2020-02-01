using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

public class PlayerController : MonoBehaviour
{
    // Movement data
    public float moveSpeed;
    private Vector3 m_Velocity;
    private Vector3 m_VelocityRef;
    private Vector2 m_NormalizedVelocity;
    private Rigidbody2D m_Rigidbody;
    
    // Collision data
    private WheelController m_CurrentWheelController = null;
    private WallController m_CurrentWallController = null;

    public bool isRunning;
        
    private void Awake()
    {
        m_Rigidbody = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        if (!isRunning) return;
        //var newPosition = Vector3.SmoothDamp(transform.position, transform.position + m_Velocity * Time.fixedDeltaTime, ref m_VelocityRef, 0.05f);
        m_Rigidbody.MovePosition(transform.position + m_Velocity * Time.fixedDeltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Wheel")) return;
        
        Debug.Log($"{name} Entered wheel collider {other.name}");
        m_CurrentWheelController = other.GetComponent<WheelController>();
        if (m_CurrentWheelController.currentPlayer == null)
        {
            m_CurrentWheelController.currentPlayer = this;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (!other.CompareTag("Wheel")) return;
        
        Debug.Log($"{name} left wheel collider {other.name}");
        // TODO: Stop action on wheel if any
        m_CurrentWheelController = null;
    }

    public void Move(InputAction.CallbackContext value)
    {
        if (!isRunning) return;
        m_NormalizedVelocity = value.ReadValue<Vector2>() * moveSpeed;
        m_Velocity.x = m_NormalizedVelocity.x;
        m_Velocity.y = m_NormalizedVelocity.y;
    }

    public void Action(InputAction.CallbackContext value)
    {
        if (!isRunning) return;
        if (!value.performed) return;
        if (!(value.control is ButtonControl button)) return;

        if (m_CurrentWheelController != null)
        {
            ProcessWheelAction(button.isPressed);
        }
        else if (m_CurrentWallController != null)
        {
            ProcessWallAction(button.isPressed);
        }
    }

    private void ProcessWheelAction(bool buttonIsPressed)
    {
        if (buttonIsPressed)
        {
            if (m_CurrentWheelController.currentPlayer == null)
            {
                Debug.Log("m_CurrentWheelController.currentPlayer is null on press Action, this shouldn't happen");
            }
            else if(m_CurrentWheelController.currentPlayer == this)
            {
                Debug.Log($"{name} started using action on wheel collider {m_CurrentWheelController.name}");  
                // TODO: perform wheel action
            }
            else
            {
                Debug.Log($"Cannot use action, Player {m_CurrentWheelController.currentPlayer.name} is inside wheel collider {m_CurrentWheelController.name}");
            }
        }
        else
        {
            if (m_CurrentWheelController.currentPlayer == null)
            {
                Debug.Log("m_CurrentWheelController.currentPlayer is null on release Action, this shouldn't happen");
            }
            else if(m_CurrentWheelController.currentPlayer == this)
            {
                Debug.Log($"{name} stopped using action on wheel collider {m_CurrentWheelController.name}");
                // TODO: stop wheel action
            }
            else
            {
                Debug.Log($"Cannot stop action, Player {m_CurrentWheelController.currentPlayer.name} is inside wheel collider {m_CurrentWheelController.name}");
            }
        }
    }
    private void ProcessWallAction(bool buttonIsPressed)
    {
        throw new NotImplementedException();
    }
}