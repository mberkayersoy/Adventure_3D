using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CardDataSO", menuName = "Card Data")]
public class CardDataSO : ScriptableObject
{
    // UI
    public Skill cardSkill;
    public string cardName;
    public Sprite cardSprite;
    public Sprite cardBackground;
    [Range(0, 6)]
    public int cardLevel;
    public string[] cardInfos;

    //public void IncreaseSkillLevel()
    //{
    //    cardLevel++;
    //    cardSkill.UpgradeSkill();
    //}
}
