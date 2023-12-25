using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CardDataSO", menuName = "Card Data")]
public class CardDataSO : ScriptableObject
{
    public Skill cardSkill;
    public Sprite cardSprite;
    public string cardName;
    public string cardDescription;
    public string upgradeInfo;
    public Action cardEvent;
}
