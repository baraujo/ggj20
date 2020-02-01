using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;

namespace PxlSquad {
    public class EventManager : MonoBehaviour {
        // Private parameter event classes
        // Trigger events (no params) are just plain UnityEvents
        private class IntEvent : UnityEvent<int> { };
        private class FloatEvent : UnityEvent<float> { };
        private class StringEvent : UnityEvent<string> { };

        private Dictionary<string, UnityEventBase> m_EventDict;
        private static EventManager m_EventManager;

        public static EventManager Instance
        {
            get
            {
                if (!m_EventManager)
                {
                    m_EventManager = FindObjectOfType<EventManager>();
                    if (!m_EventManager)
                    {
                        Debug.LogError("EventManager should be present in a GameObject on the current scene");
                    }
                    else
                    {
                        m_EventManager.Init();
                    }
                }
                return m_EventManager;
            }
        }

        void Init() {
            if(m_EventDict == null)
            {
                m_EventDict = new Dictionary<string, UnityEventBase>();
            }
        }

        #region Trigger events
        private UnityEvent GetOrCreateTrigger(string eventName) {
            UnityEventBase currentEvent = null;
            if (!Instance.m_EventDict.TryGetValue(eventName, out currentEvent))
            {
                currentEvent = new UnityEvent();
                Instance.m_EventDict.Add(eventName, currentEvent);
            }
            return currentEvent as UnityEvent;
        }

        public static void RegisterTrigger(string eventName, UnityAction listener) {
            Instance.GetOrCreateTrigger(eventName).AddListener(listener);
        }

        public static void DeregisterTrigger(string eventName, UnityAction listener) {
            if (m_EventManager == null) return;
            Instance.GetOrCreateTrigger(eventName).RemoveListener(listener);
        }

        public static void Trigger(string eventName) {
            UnityEventBase currentEvent = null;
            if (Instance.m_EventDict.TryGetValue(eventName, out currentEvent))
            {
                var convertedEvent = currentEvent as UnityEvent;
                convertedEvent.Invoke();
            }
            else
            {
                Debug.Log("No one is listening to the event" + eventName);
            }
        }
        #endregion

        #region Int events
        private IntEvent GetOrCreateIntEvent(string eventName) {
            UnityEventBase currentEvent = null;
            if (!Instance.m_EventDict.TryGetValue(eventName, out currentEvent))
            {
                currentEvent = new IntEvent();
                Instance.m_EventDict.Add(eventName, currentEvent);
            }
            return currentEvent as IntEvent;
        }

        public static void RegisterIntEvent(string eventName, UnityAction<int> listener) {
            Instance.GetOrCreateIntEvent(eventName).AddListener(listener);
        }

        public static void DeregisterIntEvent(string eventName, UnityAction<int> listener) {
            if (m_EventManager == null) return;
            Instance.GetOrCreateIntEvent(eventName).RemoveListener(listener);
        }

        public static void TriggerIntEvent(string eventName, int parameter) {
            UnityEventBase currentEvent = null;
            if (Instance.m_EventDict.TryGetValue(eventName, out currentEvent))
            {
                var convertedEvent = currentEvent as IntEvent;
                convertedEvent.Invoke(parameter);
            }
            else
            {
                Debug.Log("No one is listening to the event" + eventName);
            }
        }
        #endregion

        #region Float events
        private FloatEvent GetOrCreateFloatEvent(string eventName) {
            UnityEventBase currentEvent = null;
            if (!Instance.m_EventDict.TryGetValue(eventName, out currentEvent))
            {
                currentEvent = new FloatEvent();
                Instance.m_EventDict.Add(eventName, currentEvent);
            }
            return currentEvent as FloatEvent;
        }

        public static void RegisterFloatEvent(string eventName, UnityAction<float> listener) {
            Instance.GetOrCreateFloatEvent(eventName).AddListener(listener);
        }

        public static void DeregisterFloatEvent(string eventName, UnityAction<float> listener) {
            if (m_EventManager == null) return;
            Instance.GetOrCreateFloatEvent(eventName).RemoveListener(listener);
        }

        public static void TriggerFloatEvent(string eventName, float parameter) {
            UnityEventBase currentEvent = null;
            if (Instance.m_EventDict.TryGetValue(eventName, out currentEvent))
            {
                var convertedEvent = currentEvent as FloatEvent;
                convertedEvent.Invoke(parameter);
            }
            else
            {
                Debug.Log("No one is listening to the event" + eventName);
            }
        }
        #endregion

        #region String events
        private StringEvent GetOrCreateStringEvent(string eventName) {
            UnityEventBase currentEvent = null;
            if (!Instance.m_EventDict.TryGetValue(eventName, out currentEvent))
            {
                currentEvent = new StringEvent();
                Instance.m_EventDict.Add(eventName, currentEvent);
            }
            return currentEvent as StringEvent;
        }

        public static void RegisterStringEvent(string eventName, UnityAction<string> listener) {
            Instance.GetOrCreateStringEvent(eventName).AddListener(listener);
        }

        public static void DeregisterStringEvent(string eventName, UnityAction<string> listener) {
            if (m_EventManager == null) return;
            Instance.GetOrCreateStringEvent(eventName).RemoveListener(listener);
        }

        public static void TriggerStringEvent(string eventName, string parameter) {
            UnityEventBase currentEvent = null;
            if (Instance.m_EventDict.TryGetValue(eventName, out currentEvent))
            {
                var convertedEvent = currentEvent as StringEvent;
                convertedEvent.Invoke(parameter);
            }
            else
            {
                Debug.Log("No one is listening to the event" + eventName);
            }
        }
        #endregion
    }
}