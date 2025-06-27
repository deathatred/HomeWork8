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
            GameEventBus.BackButtonClick(BackButtonContext.Settings);
        });
        GameEventBus.OnRebindStarted += GameEventBusOnRebindStarted;
        GameEventBus.OnRebindFinished += GameEventBusOnRebindFinished;
    }


    private void UnsubscribeFromEvents()
    {
        _musicPlusButton.onClick.RemoveAllListeners();
        _musicMinusButton.onClick.RemoveAllListeners();
        _backButton?.onClick.RemoveAllListeners();
        GameEventBus.OnRebindStarted -= GameEventBusOnRebindStarted;
        GameEventBus.OnRebindFinished -= GameEventBusOnRebindFinished;
    }
    private void SetDefaults()
    {
        if (!PlayerPrefs.HasKey("MusicVolume")) 
        {
            int musicDefault = 5;
            _musicIndex = musicDefault;
            _musicSettingText.text = $"MUSIC: {_musicIndex}";
            
        }
        else
        {
            float saved = PlayerPrefs.GetFloat("MusicVolume");
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

}
