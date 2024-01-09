using DG.Tweening;
using GameSystemsCookbook;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class CardManager : MonoBehaviour
{
    [SerializeField] private CardSkillContainerSO _cardSkills;
    [SerializeField] private Transform _skillPanelUI;
    [SerializeField] private Transform _cardContent;
    [SerializeField] private Transform _currentActiveCardsContent;
    [SerializeField] private Transform _currentPassiveCardsContent;
    [SerializeField] private UICard _cardPrefab;

    [SerializeField] private List<CardData> _activeSkills = new List<CardData>();
    [SerializeField] private List<CardData> _passiveSkills = new List<CardData>();
    [SerializeField] private List<CardData> _unlockedActiveSkills = new List<CardData>(_MAXSKILLCOUNT);
    [SerializeField] private List<CardData> _unlockedPassiveSkills = new List<CardData>(_MAXSKILLCOUNT);

    [Header("Listening Events")]
    [SerializeField] private SkillEventChannelSO _skillAddedEventChannel;
    [SerializeField] private SkillEventChannelSO _skillUpgradedEventChannel;
    [SerializeField] private SkillEventChannelSO _skillChooseEventChannel;
    [SerializeField] private IntEventChannelSO _playerLeveledUp;

    private int _cardGenerationCount;
    private bool _isSkillChoosing;
    private Tween _skillPanelTween;
    private const int _MAXSKILLCOUNT = 6;
    private void Awake()
    {
        _activeSkills = _cardSkills.CopyActiveSkillList();
        _passiveSkills = _cardSkills.CopyPassiveSkillList();
    }
    private void OnEnable()
    {
        _skillAddedEventChannel.OnEventRaised += CardAdded;
        _skillUpgradedEventChannel.OnEventRaised += CardUpgraded;
        _playerLeveledUp.OnEventRaised += ShowSkillPanel;
        _skillChooseEventChannel.OnEventRaised += HideSkillPanel;
    }
    private void OnDisable()
    {
        _skillAddedEventChannel.OnEventRaised -= CardAdded;
        _skillUpgradedEventChannel.OnEventRaised -= CardUpgraded;
        _playerLeveledUp.OnEventRaised -= ShowSkillPanel;
        _skillChooseEventChannel.OnEventRaised -= HideSkillPanel;
    }

    private void HideSkillPanel(CardData arg0)
    {
        if (_cardGenerationCount > 0)
        {
            DestroyOldCards();
            _cardGenerationCount--;
            GenerateCards();
        }
        else
        {
            if (_skillPanelTween.IsActive())
            {
                _skillPanelTween.Kill(true);
            }

            Time.timeScale = 1f;
            _isSkillChoosing = false;
            _skillPanelTween = _skillPanelUI.DOScale(Vector3.zero, 0.5f).SetUpdate(true).OnComplete(() =>
            {
                _skillPanelUI.gameObject.SetActive(false);
            });

            DestroyOldCards();
        }
    }

    private void ShowSkillPanel(int arg0)
    {
        if (_skillPanelTween.IsActive())
        {
            _skillPanelTween.Kill(true);
        }

        if (_isSkillChoosing)
        {
            _cardGenerationCount++;
        }
        else
        {
            GenerateCards();
            _isSkillChoosing = true;
            _skillPanelUI.gameObject.SetActive(true);
            _skillPanelUI.localScale = Vector3.zero;
            _skillPanelTween = _skillPanelUI.DOScale(Vector3.one, 0.5f).SetUpdate(true);
        }
        Time.timeScale = 0f;
    }
    private void CardUpgraded(CardData upgradedSkill)
    {
        upgradedSkill.cardLevel++;
    }

    private void CardAdded(CardData addedSkill)
    {
        addedSkill.cardLevel = 1;

        if (addedSkill.cardSkill is ActiveSkill)
        {
            _unlockedActiveSkills.Add(addedSkill);
        }
        else
        {
            _unlockedPassiveSkills.Add(addedSkill);
        }
        UpdateCurrentSkillsUISlot();
    }

    private void GenerateCards()
    {
        List<CardData> cardsToShow = new List<CardData>();

        if (_unlockedActiveSkills.Count == _MAXSKILLCOUNT)
        {
            cardsToShow = GetRandomCards(_unlockedActiveSkills, _passiveSkills, 3);
        }
        else if (_unlockedPassiveSkills.Count == _MAXSKILLCOUNT)
        {
            cardsToShow = GetRandomCards(_activeSkills, _unlockedPassiveSkills, 3);
        }
        else if (_unlockedPassiveSkills.Count == _MAXSKILLCOUNT && _unlockedActiveSkills.Count == _MAXSKILLCOUNT)
        {
            cardsToShow = GetRandomCards(_unlockedActiveSkills, _unlockedPassiveSkills, 3);
        }
        else
        {
            cardsToShow = GetRandomCards(_activeSkills, _passiveSkills, 3);
        }

        foreach (CardData card in cardsToShow)
        {
            UICard uiCard = Instantiate(_cardPrefab, _cardContent);
            uiCard.CardData = card;
        }
    }
    private void DestroyOldCards()
    {
        foreach (Transform item in _cardContent.transform)
        {
            if (item == _cardContent) continue;

            Destroy(item.gameObject);
        }
    }

    private void UpdateCurrentSkillsUISlot()
    {
        for (int i = 0; i < _unlockedActiveSkills.Count; i++)
        {
            _currentActiveCardsContent.transform.GetChild(i + 1).GetComponent<Image>().sprite =
                _unlockedActiveSkills[i].cardSprite;
        }

        for (int i = 0; i < _unlockedPassiveSkills.Count; i++)
        {
            _currentPassiveCardsContent.transform.GetChild(i + 1).GetComponent<Image>().sprite =
                _unlockedPassiveSkills[i].cardSprite;
        }
    }
    /// <summary>
    /// Merges two lists and selects a specified number of unique random items from the combined list.
    /// </summary>
    /// <typeparam name="T">The type of items in the lists.</typeparam>
    /// <param name="list1">The first list to merge.</param>
    /// <param name="list2">The second list to merge.</param>
    /// <param name="cardCount">The number of random items to select.</param>
    /// <returns>A list containing unique random items from the merged lists.</returns>
    private List<T> GetRandomCards<T>(List<T> list1, List<T> list2, int cardCount)
    {
        List<T> mergedList = list1.Concat(list2).ToList();
        List<T> choosenCards = new List<T>();

        while (choosenCards.Count < cardCount)
        {
            T randomKart = mergedList[UnityEngine.Random.Range(0, mergedList.Count)];

            if (!choosenCards.Contains(randomKart))
            {
                choosenCards.Add(randomKart);
            }
        }

        return choosenCards;
    }
}
