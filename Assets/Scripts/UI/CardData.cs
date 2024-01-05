using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class CardData
{
    public Skill cardSkill;
    public string cardName;
    public Sprite cardSprite;
    public Sprite cardBackground;
    [Range(0, 6)]
    public int cardLevel;
    public string[] cardInfos;

    public CardData(Skill cardSkill, string cardName, Sprite cardSprite, Sprite cardBackground, int cardLevel, string[] cardInfos)
    {
        this.cardSkill = cardSkill ?? throw new ArgumentNullException(nameof(cardSkill));
        this.cardName = cardName ?? throw new ArgumentNullException(nameof(cardName));
        this.cardSprite = cardSprite ?? throw new ArgumentNullException(nameof(cardSprite));
        this.cardBackground = cardBackground ?? throw new ArgumentNullException(nameof(cardBackground));
        this.cardLevel = cardLevel;
        this.cardInfos = cardInfos ?? throw new ArgumentNullException(nameof(cardInfos));
    }

    public void IncreaseSkillLevel()
    {
        cardLevel++;
        cardSkill.UpgradeSkill();
    }
}
