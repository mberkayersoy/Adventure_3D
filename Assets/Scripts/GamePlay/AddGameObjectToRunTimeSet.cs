using GameSystemsCookbook;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddGameObjectToRunTimeSet : MonoBehaviour
{
    [SerializeField] private GameObjectRuntimeSetSO m_RunTimeSet;

    private void OnEnable()
    {
        if (m_RunTimeSet != null)
            m_RunTimeSet.Add(gameObject);
    }

    private void OnDisable()
    {
        if (m_RunTimeSet != null)
            m_RunTimeSet.Remove(gameObject);
    }
}
