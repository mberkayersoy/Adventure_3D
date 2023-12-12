using GameSystemsCookbook;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Transform _target;
    [SerializeField] private DefaultEnemyBrainSO defaultEnemySO;
    [SerializeField] private GameObjectRuntimeSetSO playerSet;
    private Rigidbody _rb;

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }
    private void OnEnable()
    {
        // There is only one player.
        if (playerSet.Items.Count > 0)
        {
            _target = playerSet.Items[0].transform;
        }
    }
    
    private void Update()
    {
        defaultEnemySO.Rotate(transform, _target);
    }

    private void FixedUpdate()
    {
        defaultEnemySO.Move(_rb, transform, _target);
    }
}
