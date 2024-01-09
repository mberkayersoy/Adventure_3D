using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MineInteraction : MonoBehaviour
{
    [SerializeField] private float _explosionRadius = 1f;
    [SerializeField] private int _damage;
    [SerializeField] private float _countdownTimer = 3f;
    [SerializeField] private ParticleSystem _explosionVFX;
    [SerializeField] private Transform _mineModel;
    [SerializeField] private Transform _timerCanvas;
    [SerializeField] private Image _remaningTimeImage;
    private float _remainingTime;
    private bool _isExploded = false;
    void Start()
    {
        _remainingTime = _countdownTimer;
    }
    private void OnDisable()
    {
        ResetMine();
    }
    public void SetMineDatas(int damage, float explosionRadius)
    {
        _damage = damage;
        _explosionRadius = explosionRadius;
    }

    void Update()
    {
        if (!_isExploded)
        {
            _remainingTime -= Time.deltaTime;
            _remaningTimeImage.fillAmount = _remainingTime / _countdownTimer;

            if (_remainingTime <= 0)
            {
                Explode();
            }
        }
    }

    private void Explode()
    {
        _isExploded = true;
        _mineModel.gameObject.SetActive(false);
        _timerCanvas.gameObject.SetActive(false);
        _explosionVFX.Play();
        GetComponent<Collider>().enabled = false;
        Collider[] colliders = Physics.OverlapSphere(transform.position, _explosionRadius);
        foreach (Collider collider in colliders)
        {
            if (collider.TryGetComponent(out IDamageable enemy))
            {
                enemy.TakeDamage(_damage);
            }
        }
    }

    private void ResetMine()
    {
        _isExploded = false;
        _mineModel.gameObject.SetActive(true);
        _timerCanvas.gameObject.SetActive(true);
        _remainingTime = _countdownTimer;
        GetComponent<Collider>().enabled = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out Enemy enemy))
        {
            Explode();
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, _explosionRadius);
    }
}
