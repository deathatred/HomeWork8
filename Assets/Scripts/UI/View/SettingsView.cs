using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SettingsView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _musicSettingText;
    [SerializeField] private Button _backButton;
    [SerializeField] private Button _musicPlusButton;
    [SerializeField] private Button _musicMinusButton;
    [SerializeField] private Image _rebindBackground;

    private static int _musicIndex;
    private const string MUSIC_VOLUME_KEY = "MusicVolume";

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
        _musicPlusButton.onClick.AddListener(AddVolume);
        _musicMinusButton.onClick.AddListener(LowerVolume);
        _backButton.onClick.AddListener(BackButtonClick);
        GameEventBus.OnRebindStarted += GameEventBusOnRebindStarted;
        GameEventBus.OnRebindFinished += GameEventBusOnRebindFinished;
    }
    private void UnsubscribeFromEvents()
    {
        _musicPlusButton.onClick.RemoveListener(AddVolume);
        _musicMinusButton.onClick.RemoveListener(LowerVolume);
        _backButton?.onClick.RemoveListener(BackButtonClick);
        GameEventBus.OnRebindStarted -= GameEventBusOnRebindStarted;
        GameEventBus.OnRebindFinished -= GameEventBusOnRebindFinished;
    }
    private void SetDefaults()
    {
        if (!PlayerPrefsManager.CheckForKey(MUSIC_VOLUME_KEY))
        {
            int musicDefault = 5;
            _musicIndex = musicDefault;
            _musicSettingText.text = $"MUSIC: {_musicIndex}";

        }
        else
        {
            float saved = PlayerPrefsManager.GetFloatFromKey(MUSIC_VOLUME_KEY);
            _musicIndex = Mathf.RoundToInt(saved * 10);
            _musicSettingText.text = $"MUSIC: {_musicIndex}";
        }
    }
    private void GameEventBusOnRebindStarted()
    {
        _rebindBackground.gameObject.SetActive(true);
    }
    private void GameEventBusOnRebindFinished()
    {
        _rebindBackground.gameObject.SetActive(false);
    }
    private void AddVolume()
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
    }
    private void LowerVolume()
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
    }
    private void BackButtonClick()
    {
        GameEventBus.BackButtonClick(BackButtonContext.Settings);
    }
}
