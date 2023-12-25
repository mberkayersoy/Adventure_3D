using GameSystemsCookbook;
using Lean.Pool;
using UnityEngine;

public class ExperiencePickUp : MonoBehaviour
{
    [SerializeField] private int _experienceAmount;

    [Header("Broadcast on Event Channels")]
    [SerializeField] private IntEventChannelSO _playerGainedExperience;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out PlayerScoresHandler player))
        {
            _playerGainedExperience.RaiseEvent(_experienceAmount);
            LeanPool.Despawn(this.gameObject);
        }
    }
}
