using GameSystemsCookbook;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReturnPoolObject : MonoBehaviour
{
    [Header("Broadcast the Event Channel")]
    [SerializeField] private GameObjectEventChannelSO _returnObjectPool;
    private void OnDisable()
    {
        _returnObjectPool.RaiseEvent(gameObject);
    }
}
