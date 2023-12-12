using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoccerBallController : Skill
{
    [SerializeField] private SoccerBallInteraction soccerBallPrefab;
    [SerializeField] private float speed;
    [SerializeField] private int damage;
    [SerializeField] private int bounceCount;
    [SerializeField] private int ballCount;
    [SerializeField] private SoccerBallInteraction[] soccerBallList;

    public event Action<float, int, int> OnActivateAction;


    private new void Start()
    {
        CreateSoccerBalls();
        base.Start();
    }
    private void CreateSoccerBalls()
    {
        soccerBallList = new SoccerBallInteraction[ballCount];
        for (int i = 0; i < ballCount; i++) 
        {
            SoccerBallInteraction newBall = Instantiate(soccerBallPrefab, transform);
            soccerBallList[i] = newBall;
            newBall.gameObject.SetActive(false);
        }
    }

    private void DestroySoccerBalls()
    {
        foreach (var item in soccerBallList)
        {
            Destroy(item.gameObject);
        }
    }

    private void ActivateSoccerBalls()
    {
        foreach (SoccerBallInteraction ball in soccerBallList)
        {
            ball.gameObject.SetActive(IsActive);
            ball.transform.position = transform.position;
        }
        OnActivateAction?.Invoke(speed, damage, bounceCount);
    }

    private void DeActivateSoccerBalls()
    {
        foreach (SoccerBallInteraction ball in soccerBallList)
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
        throw new System.NotImplementedException();
    }
}
