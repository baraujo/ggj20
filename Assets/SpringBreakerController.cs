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
                ObstaclesGenerator.obstaclesCount--;
                Destroy(gameObject);
            }
        }
    }

    private void Update()
    {
        if (transform.position.x < -20)
        {
            ObstaclesGenerator.obstaclesCount--;
            Destroy(gameObject);
        }
            
    }
}
