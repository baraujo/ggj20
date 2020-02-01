using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// TODO:
// - Test various use cases on editor and builds Windows/Android
// - MessagingManager is a good name? Are there better ones?
// - Serialization is an issue here?

namespace PxlSquad {
    // Provides forced cleanup of all instantiated MessagingManagers when needed
    // Clients should Register/Deregister observers on their own, though
    public static class MessagingManagerIndex {
        private static List<IDictionary> m_dictList = new List<IDictionary>();

        public static void AddManager(IDictionary dict) {
            m_dictList.Add(dict);
        }

        public static void ClearAllManagers() {
            foreach(var dict in m_dictList)
            {
                dict.Clear();
            }
        }
    }

    // Observers supported
    public delegate void NoParameterFunction();
    public delegate void SingleParameterFunction<T>(T parameter);
    public delegate void DoubleParameterFunction<T1, T2>(T1 parameter1, T2 parameter2);
    // I wanna add more parameters, but at what cost? ლ(ಠ_ಠ ლ)

    // No parameter version
    public static class MessagingManager {
        private static Dictionary<string, NoParameterFunction> m_SubjectDict;

        static MessagingManager() {
            m_SubjectDict = new Dictionary<string, NoParameterFunction>();
            MessagingManagerIndex.AddManager(m_SubjectDict);
        }

        public static void RegisterObserver(string channel, NoParameterFunction observer) {
            m_SubjectDict.TryGetValue(channel, out NoParameterFunction currentSubject);
            currentSubject += observer;
            m_SubjectDict[channel] = currentSubject;
        }

        public static void DeregisterObserver(string channel, NoParameterFunction observer) {
            if (m_SubjectDict.TryGetValue(channel, out NoParameterFunction currentObserver))
            {
                currentObserver -= observer;
                m_SubjectDict[channel] = currentObserver;
            }
        }

        public static void SendMessage(string channel) {
            if (m_SubjectDict.TryGetValue(channel, out NoParameterFunction currentObserver))
            {
                currentObserver();
            }
            else
            {
                Debug.Log("No one is observing this message channel: " + channel);
            }
        }
    }

    // Single parameter version
    public static class MessagingManager<T> {
        private static Dictionary<string, SingleParameterFunction<T>> m_SubjectDict;

        static MessagingManager() {
            m_SubjectDict = new Dictionary<string, SingleParameterFunction<T>>();
            MessagingManagerIndex.AddManager(m_SubjectDict);
        }

        public static void RegisterObserver(string channel, SingleParameterFunction<T> observer) {
            m_SubjectDict.TryGetValue(channel, out SingleParameterFunction<T> currentSubject);
            currentSubject += observer;
            m_SubjectDict[channel] = currentSubject;
        }

        public static void DeregisterObserver(string channel, SingleParameterFunction<T> observer) {
            if (m_SubjectDict.TryGetValue(channel, out SingleParameterFunction<T> currentSubject)) {
                currentSubject -= observer;
                m_SubjectDict[channel] = currentSubject;
            }
        }

        public static void SendMessage(string channel, T parameter) {
            if (m_SubjectDict.TryGetValue(channel, out SingleParameterFunction <T> currentSubject)) {
                currentSubject(parameter);
            }
            else {
                Debug.LogWarning("There are no observers for channel " + channel);
            }
        }
    }

    // Double parameter version
    public static class MessagingManager<T1, T2> {
        private static Dictionary<string, DoubleParameterFunction<T1, T2>> m_SubjectDict;

        static MessagingManager() {
            m_SubjectDict = new Dictionary<string, DoubleParameterFunction<T1, T2>>();
            MessagingManagerIndex.AddManager(m_SubjectDict);
        }

        public static void RegisterObserver(string channel, DoubleParameterFunction<T1, T2> observer) {
            m_SubjectDict.TryGetValue(channel, out DoubleParameterFunction<T1, T2> currentSubject);
            currentSubject += observer;
            m_SubjectDict[channel] = currentSubject;
        }

        public static void DeregisterObserver(string channel, DoubleParameterFunction<T1, T2> observer) {
            if (m_SubjectDict.TryGetValue(channel, out DoubleParameterFunction<T1, T2> currentSubject)) {
                currentSubject -= observer;
                m_SubjectDict[channel] = currentSubject;
            }
        }

        public static void SendMessage(string channel, T1 parameter1, T2 parameter2) {
            if (m_SubjectDict.TryGetValue(channel, out DoubleParameterFunction<T1, T2> currentSubject)) {
                currentSubject(parameter1, parameter2);
            }
            else {
                Debug.LogWarning("There are no observers for channel " + channel);
            }
        }
    }
}
