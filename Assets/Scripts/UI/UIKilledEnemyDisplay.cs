using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using GameSystemsCookbook;
using DG.Tweening;

public class UIKilledEnemyDisplay : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _killedEnemyText;
    [SerializeField] private VoidEventChannelSO _enemyDead;
    private int _deadEnemyCount;
    private void OnEnable()
    {
        _enemyDead.OnEventRaised += UpdateDeadEnemyDisplay;
    }

    private void UpdateDeadEnemyDisplay()
    {
        _deadEnemyCount++;
        _killedEnemyText.text = _deadEnemyCount.ToString();

        // Adding a scale animation using DOTween
        _killedEnemyText.transform.localScale = Vector3.one;

        _killedEnemyText.transform.DOScale(new Vector3(1.2f, 1.2f, 1.2f), 0.3f)
            .SetEase(Ease.OutQuad) 
            .OnComplete(() => _killedEnemyText.transform.localScale = Vector3.one);
    }

    private void OnDisable()
    {
        _enemyDead.OnEventRaised -= UpdateDeadEnemyDisplay;
    }
}
