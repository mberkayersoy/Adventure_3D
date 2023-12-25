using UnityEngine;
using TMPro;
using UnityEngine.UI;
using GameSystemsCookbook;
using DG.Tweening;

public class UIExperienceDisplay : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _levelText;
    [SerializeField] private Image _fillImage;

    [Header("Listen to Event Channels")]
    [SerializeField] private IntEventChannelSO _playerLeveledUp;
    [SerializeField] private ExperienceDataEventChannelSO _playerExperienceData;

    private void OnEnable()
    {
        _playerLeveledUp.OnEventRaised += UpdateLevel;
        _playerExperienceData.OnEventRaised += UpdateExperienceDisplay;

        // Set Default values
        _fillImage.fillAmount = 0;
    }

    private void OnDisable()
    {
        _playerLeveledUp.OnEventRaised -= UpdateLevel;
        _playerExperienceData.OnEventRaised -= UpdateExperienceDisplay;
    }
    private void UpdateLevel(int level)
    {
        _levelText.text = level.ToString();
        _fillImage.fillAmount = 0; // Reset fillAmount;
    }
    private void UpdateExperienceDisplay(ExperienceData experienceData)
    {
        float targetFillAmount = (float)experienceData.currentXP /
            (experienceData.currentLevel * experienceData.experienceMultiplier);

        
        DOTween.To(() => _fillImage.fillAmount, x => _fillImage.fillAmount = x, targetFillAmount, 0.5f)
            .SetEase(Ease.OutQuad);
    }
}
