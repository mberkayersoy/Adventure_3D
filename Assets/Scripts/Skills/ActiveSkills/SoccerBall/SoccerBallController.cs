using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoccerBallController : ActiveSkill
{
    [SerializeField] private SoccerBallInteraction _soccerBallPrefab;
    [SerializeField] private float _speed;
    [SerializeField] private int _damage;
    [SerializeField] private int _ballCount;
    [SerializeField] private SoccerBallInteraction[] _soccerBallList;

    /// <summary>
    /// params: _speed, _damage
    /// </summary>
    public event Action<float, int> OnActivateAction;

    private new void Start()
    {
        CreateSoccerBalls();
        base.Start();
    }
    private new void Update()
    {
        base.Update();
        transform.rotation = Quaternion.Euler(0, 0, 0);
    }
    private void CreateSoccerBalls()
    {
        _soccerBallList = new SoccerBallInteraction[_ballCount];
        for (int i = 0; i < _ballCount; i++) 
        {
            SoccerBallInteraction newBall = Instantiate(_soccerBallPrefab, transform);
            _soccerBallList[i] = newBall;
            newBall.gameObject.SetActive(false);
        }
    }

    private void DestroySoccerBalls()
    {
        foreach (var item in _soccerBallList)
        {
            Destroy(item.gameObject);
        }
    }

    private void ActivateSoccerBalls()
    {
        foreach (SoccerBallInteraction ball in _soccerBallList)
        {
            ball.gameObject.SetActive(IsActive);
            ball.transform.position = transform.position;
        }
        OnActivateAction?.Invoke(_speed, _damage);
    }

    private void DeActivateSoccerBalls()
    {
        foreach (SoccerBallInteraction ball in _soccerBallList)
        {
            ball.gameObject.SetActive(IsActive);
        }
    }
    public override void Activate()
    {
        base.Activate();
        ActivateSoccerBalls();
    }

    public override void DeActivate()
    {
        base.DeActivate();
        DeActivateSoccerBalls();
    }
    public override void UpgradeSkill()
    {
        _ballCount++;
        _damage += 100;
        DestroySoccerBalls();
        CreateSoccerBalls();
    }
}
