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
    private LeverController m_CurrentLeverController = null;
    private bool m_LeverInUse = false;

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
        if (other.CompareTag("Wheel"))
        {
            m_CurrentWheelController = other.GetComponent<WheelController>();
            if (m_CurrentWheelController.currentPlayer == null)
            {
                m_CurrentWheelController.currentPlayer = this;
            }
        }

        if (other.CompareTag("Lever"))
        {
            m_CurrentLeverController = other.GetComponent<LeverController>();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Wheel"))
        {
            if (m_CurrentWheelController != null)
            {
                m_CurrentWheelController.repairInProgress = true;
            }

            m_CurrentWheelController = null;
        }

        if (other.CompareTag("Lever"))
        {
            if (m_LeverInUse)
            {
                m_CurrentLeverController.leverSpeed--;
                m_LeverInUse = false;
            }

            m_CurrentLeverController = null;
        }
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
        else if (m_CurrentLeverController != null)
        {
            ProcessLeverAction(button.isPressed);
        }
    }

    private void ProcessLeverAction(bool buttonIsPressed)
    {
        if (buttonIsPressed)
        {
            m_CurrentLeverController.leverSpeed++;
            m_LeverInUse = true;
        }
        else
        {
            if(m_CurrentLeverController.leverSpeed > 0)
            {
                m_CurrentLeverController.leverSpeed--;
            }
            m_LeverInUse = false;
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
                m_CurrentWheelController.repairInProgress = true;
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
                m_CurrentWheelController.repairInProgress = false;
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