using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SettingsView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _musicSettingText;
    [SerializeField] private Button _backButton;
    [SerializeField] private Button _musicPlusButton;
    [SerializeField] private Button _musicMinusButton;

    private int _musicIndex;
    private float _musicDefault = 0.5f;

    private void OnEnable()
    {
        SubscribeToEvents();
        SetDefaults();
    }
    private void OnDisable()
    {
        UnsubscribeFromEvents();
    }
    private void SubscribeToEvents()
    {
        _musicPlusButton.onClick.AddListener(() =>
        {
            GameEventBus.MusicPlusButtonClick();
            int musicMaxValue = 10;
            if (_musicIndex == musicMaxValue)
            {
                return;
            }
            else
            {
                _musicSettingText.text = $"MUSIC: {++_musicIndex}";
            }

        });
        _musicMinusButton.onClick.AddListener(() =>
        {
            GameEventBus.MusicMinusButtonClick();
            int musicMinValue = 0;
            if (_musicIndex == musicMinValue)
            {
                return;
            }
            else
            {
                _musicSettingText.text = $"MUSIC: {--_musicIndex}";
            }
        });
        _backButton.onClick.AddListener(() =>
        {
            GameEventBus.BackButtonClick();
        });
    }



    private void UnsubscribeFromEvents()
    {
        _musicPlusButton.onClick.RemoveAllListeners();
        _musicMinusButton.onClick.RemoveAllListeners();
        _backButton?.onClick.RemoveAllListeners();
    }
    private void SetDefaults()
    {
        int defaultMusicVolume = 5;
        _musicSettingText.text = $"MUSIC: {defaultMusicVolume}";
        _musicIndex = defaultMusicVolume;
    }
}
