
using UnityEngine;

/// <summary>
/// Parameterized Singleton class
/// </summary>
/// <typeparam name="T"></typeparam>
public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T m_Instance;

    private void Awake()
    {
        if (m_Instance == null)
        {
            m_Instance = this as T;
        }
    }

    public static T Instance
    {
        get
        {
            if (m_Instance == null)
            {
                return FindObjectOfType<T>();
            }
            else
            {
                return m_Instance;
            }
        }
    }
} 
