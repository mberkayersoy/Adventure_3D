using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Card : MonoBehaviour
{
    [SerializeField] private Button button;
    [SerializeField] private TextMeshProUGUI cardName;
    [SerializeField] private Image cardImage;
    [SerializeField] private TextMeshProUGUI cardInfo;
    [SerializeField] private TextMeshProUGUI cardLevelText;
    [SerializeField] private Image cardBackground;
    [Header("Broadcast")]
    private Skill _cardSkill;
    [SerializeField] private SkillChooseEventChannelSO _skillChoosen;

    private void OnEnable()
    {
        button.onClick.AddListener(SkillChoosen);
    }

    public void SkillChoosen()
    {
        _skillChoosen.RaiseEvent(_cardSkill);
    }

    private void OnDisable()
    {
        button.onClick.RemoveListener(SkillChoosen);
    }
    // To do: Broadcast onClick event that card skills.
}
