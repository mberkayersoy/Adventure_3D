using GameSystemsCookbook;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class LevelObjectsSO : DescriptionSO
{
}

[System.Serializable]
public struct PoolableData
{
    public GameObject poolableObject;
    [Range(0f, 1f)]
    public float chanceRate;
}
