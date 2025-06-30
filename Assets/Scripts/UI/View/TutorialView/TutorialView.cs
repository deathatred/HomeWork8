using System;
using UnityEngine;
using UnityEngine.UI;

public class TutorialView : MonoBehaviour
{
    [SerializeField] private Button _platformsInfoButton;
    [SerializeField] private Button _enemiesInfoButton;
    [SerializeField] private Button _powerupsInfoButton;
    [SerializeField] private Button _backButton;
    [SerializeField] private RectTransform _platformScroll;
    [SerializeField] private RectTransform _enemyScroll;
    private void OnEnable()
    {
        SubscribeToEvents();
    }
    private void OnDisable()
    {
        UnsubscribeFromEvents();
    }

    private void SubscribeToEvents()
    {
        _platformsInfoButton.onClick.AddListener(ShowPlatformScroll);
        _enemiesInfoButton.onClick.AddListener(ShowEnemyPlatformScroll);
        _backButton.onClick.AddListener(BackButtonPress);
    }
    private void UnsubscribeFromEvents()
    {
        _platformsInfoButton.onClick.RemoveListener(ShowPlatformScroll);
        _enemiesInfoButton.onClick.RemoveListener(ShowEnemyPlatformScroll);
        _backButton.onClick.RemoveListener(BackButtonPress);
    }
    private void ShowPlatformScroll()
    {
        _platformScroll.gameObject.SetActive(true);
    }
    private void ShowEnemyPlatformScroll()
    {
        _enemyScroll.gameObject.SetActive(true);
    }
    private void BackButtonPress()
    {
        GameEventBus.BackButtonClick(BackButtonContext.Tutorial);
    }

}
