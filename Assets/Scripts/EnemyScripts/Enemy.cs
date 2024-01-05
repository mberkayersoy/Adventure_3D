using GameSystemsCookbook;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private DefaultEnemyBrainSO defaultEnemySO;
    [SerializeField] private GameObjectRuntimeSetSO playerSet;
    private Transform _target;
    [SerializeField] private float _maxDistanceToPlayer;
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
        CheckDistanceToPlayer();
    }

    private void FixedUpdate()
    {
        defaultEnemySO.Move(_rb, transform, _target);
    }

    // If the distance between this.gameobject and the target exceeds a certain value, return to the pool.
    private void CheckDistanceToPlayer()
    {
        if (_target == null) return;
        float distanceToPlayer = Vector3.Distance(transform.position, _target.position);

        if (distanceToPlayer >= _maxDistanceToPlayer)
        {
            Lean.Pool.LeanPool.Despawn(gameObject);
        }
    }
}
