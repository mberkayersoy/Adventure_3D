using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using Unity.VisualScripting;

public class UICard : MonoBehaviour
{
    [Header("Broadcast")]
    [SerializeField] private SkillEventChannelSO _skillChoosen;

    [Header("Display Variables")]
    [SerializeField] private Button button;
    [SerializeField] private TextMeshProUGUI cardName;
    [SerializeField] private Image cardImage;
    [SerializeField] private TextMeshProUGUI cardInfo;
    [SerializeField] private Image cardBackground;
    [SerializeField] private Image[] cardLevelStarts;

    private Tween fadeTween;
    private CardData _cardData;
    public CardData CardData { get => _cardData; set => _cardData = value; }

    private void Start()
    {
        button.onClick.AddListener(SkillChoosen);
        DisplayCardData();
    }
    public void SkillChoosen()
    {
        Debug.Log("SkillChoosen(): " + _cardData.cardSkill.name);
        _skillChoosen.RaiseEvent(_cardData);
    }

    private void DisplayCardData()
    {
        cardName.text = _cardData.cardName;
        cardImage.sprite = _cardData.cardSprite;
        cardInfo.text = _cardData.cardInfos[_cardData.cardLevel];
        cardBackground.sprite = _cardData.cardBackground;
        SetCardStars(_cardData.cardLevel);
    }

    private void SetCardStars(int level)
    {
        // Tween kontrolü ve iptali
        //if (fadeTween != null && fadeTween.IsActive())
        //{
        //    fadeTween.Kill();
        //}

        // Level 0 ise
        if (level == 0)
        {
            if (cardLevelStarts[level] != null)
            {
                fadeTween = cardLevelStarts[level].DOFade(0.4f, 1f)
                    .SetEase(Ease.Linear)
                    .SetLoops(-1, LoopType.Yoyo);
            }

            for (int i = level + 1; i < cardLevelStarts.Length; i++)
            {
                if (i < cardLevelStarts.Length)
                {
                    cardLevelStarts[i].color = new Color(1, 1, 1, 0.4f); // Transparent 
                }
            }
        }
        // Level 0'dan büyükse
        else
        {
            for (int i = level; i < cardLevelStarts.Length; i++)
            {
                if (i == level)
                {
                    if (level + 1 < cardLevelStarts.Length && cardLevelStarts[level + 1] != null)
                    {
                        Debug.Log("Fade");
                        fadeTween = cardLevelStarts[level].DOFade(0.4f, 1f)
                            .SetEase(Ease.Linear)
                            .SetLoops(-1, LoopType.Yoyo);
                    }
                }
                else
                {
                    cardLevelStarts[i].color = new Color(1, 1, 1, 0.4f); // Transparent 
                }
            }
        }
    }


    private void OnDestroy()
    {
        button.onClick.RemoveListener(SkillChoosen);
        fadeTween?.Kill();
    }
}
