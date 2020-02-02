using UnityEngine;
using Random = System.Random;

public class ObstaclesGenerator : MonoBehaviour
{
    public GameObject springbreakerPrefab;
    public Vector2 timeRange;

    public static int obstaclesCount = 0;
    
    private float m_TimerElapsed;
    private float m_TimerLimit;

    public void Start()
    {
        ResetTimer();
        obstaclesCount = 0;
    }

    private void ResetTimer()
    {
        m_TimerElapsed = 0; 
        m_TimerLimit = UnityEngine.Random.Range(timeRange.x, timeRange.y);
    }

    private void Update()
    {
        if (obstaclesCount == 0)
        {
            m_TimerElapsed += Time.deltaTime;
        }

        if (m_TimerElapsed >= m_TimerLimit)
        {
            SpawnObstacles();
            ResetTimer();
        }
    }

    private void SpawnObstacles()
    {
        if (UnityEngine.Random.value > 0.5f)
        {
            Instantiate(springbreakerPrefab, new Vector3(47.2f, -10.33f, 1), Quaternion.identity);
            Instantiate(springbreakerPrefab, new Vector3(37.2f, -10.33f, 1), Quaternion.identity);
            obstaclesCount = 2;
        }
        else
        {
            Instantiate(springbreakerPrefab, new Vector3(37.2f, -10.33f, 1), Quaternion.identity);
            obstaclesCount = 1;
        }
    }
}
