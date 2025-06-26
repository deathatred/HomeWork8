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
        _platformsInfoButton.onClick.AddListener(() =>
        {
            _platformScroll.gameObject.SetActive(true);
        });
        _backButton.onClick.AddListener(() =>
        {
            print("CLICK");
            GameEventBus.BackButtonClick(BackButtonContext.Tutorial);
        });
    }
    private void UnsubscribeFromEvents()
    {
        _backButton.onClick.RemoveAllListeners();
    }

   
}
