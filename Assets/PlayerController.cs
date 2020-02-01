using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float m_MoveSpeed;
    
    private Vector3 m_Velocity;
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
        m_NormalizedVelocity = value.ReadValue<Vector2>() * m_MoveSpeed;
        m_Velocity.x = m_NormalizedVelocity.x;
        m_Velocity.y = m_NormalizedVelocity.y;
    }

    public void Action(InputAction.CallbackContext value)
    {
        if (!value.performed) return;
        if (!(value.control is ButtonControl button)) return;
        Debug.Log(button.isPressed ? $"Pressed: {name}" : $"Released: {name}");
    }
}
