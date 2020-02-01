using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ObstacleDetector : MonoBehaviour
{
    public UnityEvent OnObstacleCollision;
    
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("Obstacle")){
            OnObstacleCollision.Invoke();
        }
    }
}
