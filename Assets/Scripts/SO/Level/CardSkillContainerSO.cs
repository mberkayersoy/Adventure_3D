using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;

[CreateAssetMenu(fileName = "SkillContainerSO", menuName = "Level/Card Skill Container")]
public class CardSkillContainerSO : LevelObjectsSO
{
    public CardDataSO[] activeSkillList;
    public CardDataSO[] passiveSkillList;

    public List<CardData> CopyActiveSkillList()
    {
        List<CardData> activeCards = new List<CardData>();
        foreach (CardDataSO cardDataSO in activeSkillList)
        {
            CardData newCard = new CardData(cardDataSO.cardSkill, cardDataSO.cardName, cardDataSO.cardSprite,
                cardDataSO.cardBackground, cardDataSO.cardLevel, cardDataSO.cardInfos);

            activeCards.Add(newCard);
        }

        return activeCards;
    }

    public List<CardData> CopyPassiveSkillList()
    {
        List<CardData> passiveCards = new List<CardData>();
        foreach (CardDataSO cardDataSO in passiveSkillList)
        {
            CardData newCard = new CardData(cardDataSO.cardSkill, cardDataSO.cardName, cardDataSO.cardSprite,
                cardDataSO.cardBackground, cardDataSO.cardLevel, cardDataSO.cardInfos);

            passiveCards.Add(newCard);
        }

        return passiveCards;
    }
}
