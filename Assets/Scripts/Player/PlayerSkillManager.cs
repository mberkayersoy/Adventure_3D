using System.Collections.Generic;
using GameSystemsCookbook;
using UnityEngine;
using UnityEngine.Rendering;

public class PlayerSkillManager : MonoBehaviour
{
    [SerializeField] private ActiveSkill _playerDefaultWeapon;
    [SerializeField] private Transform _playerSkillRoot;

    [Header("Listen to Event Channels")]
    [SerializeField] private SkillEventChannelSO _skillChoose;

    [Header("Broadcast")]
    [SerializeField] private SkillEventChannelSO _skillAdded;
    [SerializeField] private SkillEventChannelSO _skillUpgraded;

    private SerializedDictionary<System.Type, CardData> _activeSkillsDictionary = new SerializedDictionary<System.Type, CardData>();
    private SerializedDictionary<System.Type, CardData> _passiveSkillsDictionary = new SerializedDictionary<System.Type, CardData>();
    private SerializedDictionary<System.Type, Skill> _currentSkillsDictionary = new SerializedDictionary<System.Type, Skill>();

    private void OnEnable()
    {
        _skillChoose.OnEventRaised += AddSkill;
    }

    private void OnDisable()
    {
        _skillChoose.OnEventRaised -= AddSkill;
    }

    public void AddSkill(CardData newSkill)
    {
        if (newSkill.cardSkill is ActiveSkill)
        {
            AddActiveSkill(newSkill);
        }
        else
        {
            AddPassiveSkill(newSkill);
        }
    }

    private void AddActiveSkill(CardData newActiveSkill)
    {
        if (_activeSkillsDictionary.ContainsKey(newActiveSkill.cardSkill.GetType()))
        {
            UpgradeSkill(newActiveSkill, _activeSkillsDictionary, _skillUpgraded);
        }
        else
        {
            Skill newSkill = Instantiate(newActiveSkill.cardSkill, _playerSkillRoot);
            _currentSkillsDictionary.Add(newActiveSkill.cardSkill.GetType(), newSkill);
            _activeSkillsDictionary.Add(newActiveSkill.cardSkill.GetType(), newActiveSkill);
            _skillAdded.RaiseEvent(newActiveSkill);
        }
    }

    private void AddPassiveSkill(CardData newPassiveSkill)
    {
        if (_passiveSkillsDictionary.ContainsKey(newPassiveSkill.cardSkill.GetType()))
        {
            UpgradeSkill(newPassiveSkill, _passiveSkillsDictionary, _skillUpgraded);
        }
        else
        {
            Skill newSkill = Instantiate(newPassiveSkill.cardSkill, _playerSkillRoot);
            _currentSkillsDictionary.Add(newPassiveSkill.cardSkill.GetType(), newSkill);
            _passiveSkillsDictionary.Add(newPassiveSkill.cardSkill.GetType(), newPassiveSkill);
            _skillAdded.RaiseEvent(newPassiveSkill);
        }
    }

    private void UpgradeSkill(CardData upgradeSkill, Dictionary<System.Type, CardData> skillDictionary, SkillEventChannelSO upgradeEvent)
    {
        _currentSkillsDictionary[upgradeSkill.cardSkill.GetType()].UpgradeSkill();
        upgradeEvent.RaiseEvent(upgradeSkill);
    }
}