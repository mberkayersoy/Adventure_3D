using GameSystemsCookbook;
using Lean.Pool;
using UnityEngine;

public class CoinPickUp : MonoBehaviour
{
    [SerializeField] private int _coinAmount;

    [Header("Broadcast on Event Channels")]
    [SerializeField] private IntEventChannelSO _playerGainedCoin;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out PlayerScoresHandler player))
        {
            _playerGainedCoin.RaiseEvent(_coinAmount);
            LeanPool.Despawn(this.gameObject);
        }
    }
}
