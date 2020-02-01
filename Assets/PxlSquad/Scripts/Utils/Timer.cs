using UnityEngine;
using UnityEngine.Events;

namespace PxlSquad {
    public class Timer : MonoBehaviour {

        public float m_TimeLimit;
        public float m_CurrentTime;
        public float m_NormalizedTime;

        public bool m_IsRunning;
        public UnityEvent OnTimeout;

        public void Start() {
            if(m_TimeLimit == 0)
            {
                Debug.LogError("Timer component on object " + name + " with time limit zero!");
            }
        }

        public void StartTimer() {
            m_CurrentTime = 0;
            m_IsRunning = true;
        }

        public void StopTimer() {
            m_IsRunning = false;
        }

        void Update() {
            if (!m_IsRunning) return;
            m_CurrentTime += Time.deltaTime;
            m_NormalizedTime = m_CurrentTime / m_TimeLimit;

            if (m_CurrentTime > m_TimeLimit)
            {
                OnTimeout.Invoke();
                m_IsRunning = false;
            }
        }
    }
}
