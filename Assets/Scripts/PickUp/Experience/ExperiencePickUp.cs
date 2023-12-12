using GameSystemsCookbook;
using Lean.Pool;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExperiencePickUp : MonoBehaviour
{
    [SerializeField] private int _experienceAmount;

    [Header("Broadcast on Event Channels")]
    [SerializeField] private IntEventChannelSO _playerGainedExperience;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out PlayerXPHandler player))
        {
            _playerGainedExperience.RaiseEvent(_experienceAmount);
            LeanPool.Despawn(this.gameObject);
        }
    }


}
