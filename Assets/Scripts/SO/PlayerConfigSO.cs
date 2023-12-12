using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerConfigSO", menuName = "Player/Create Player PlayerConfig SO")]
public class PlayerConfigSO : ScriptableObject
{
    public float maxSpeed;
    public float smoothRotation;
    public float jumpForce;
    public float speedChangeRate;
    public float jumpTimeout;
}
