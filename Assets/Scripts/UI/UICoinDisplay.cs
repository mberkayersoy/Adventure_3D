using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using GameSystemsCookbook;
using System;
using DG.Tweening;

public class UICoinDisplay : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _pickedUpCoinDisplay;
    [SerializeField] private IntEventChannelSO _pickedUpCoin;
    private int _currentCoin;
    private void OnEnable()
    {
        _pickedUpCoin.OnEventRaised += UpdatePickedUpCoinDisplay;
    }
    private void OnDisable()
    {
        _pickedUpCoin.OnEventRaised -= UpdatePickedUpCoinDisplay;
    }
    private void UpdatePickedUpCoinDisplay(int coin)
    {
        _currentCoin += coin;

        _pickedUpCoinDisplay.text = _currentCoin.ToString();

        _pickedUpCoinDisplay.transform.localScale = Vector3.one;

        _pickedUpCoinDisplay.transform.DOScale(new Vector3(1.2f, 1.2f, 1.2f), 0.3f)
            .SetEase(Ease.OutQuad)
            .OnComplete(() => _pickedUpCoinDisplay.transform.localScale = Vector3.one);
    }
}
