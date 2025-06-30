using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MenuView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _gameNameText;
    [SerializeField] private Button _startGameButton;
    [SerializeField] private TextMeshProUGUI _startGameTitle;
    [SerializeField] private Button _settingsButton;
    [SerializeField] private Button _exitButton;
    [SerializeField] private Button _tutorialButton;

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
        _startGameButton.onClick.AddListener(StartButtonClick);
        _settingsButton.onClick.AddListener(SettingsButtonClick);
        _exitButton.onClick.AddListener(ExitButtonClick);
        _tutorialButton.onClick.AddListener(TutorialButtonClick);
        GameEventBus.OnStartGameButtonClicked += GameEventBusOnStartGameButtonClicked;
        GameEventBus.OnPlayerDead += GameEventBusOnPlayerDead;
    }
    private void UnsubscribeFromEvents()
    {
        _startGameButton.onClick.RemoveListener(StartButtonClick);
        _settingsButton.onClick.RemoveListener(SettingsButtonClick);
        _exitButton.onClick.RemoveListener(ExitButtonClick);
        _tutorialButton.onClick.RemoveListener(TutorialButtonClick);
        GameEventBus.OnStartGameButtonClicked -= GameEventBusOnStartGameButtonClicked;
        GameEventBus.OnPlayerDead -= GameEventBusOnPlayerDead;   
    }
    private void GameEventBusOnStartGameButtonClicked()
    {
        _startGameTitle.text = "Continue";
    }

    private void GameEventBusOnPlayerDead()
    {
        _startGameTitle.text = "Start";
    }
    private void StartButtonClick()
    {
        GameEventBus.StartGameButtonClick();
    }
    private void SettingsButtonClick()
    {
        GameEventBus.SettingButtonClick();
    }
    private void ExitButtonClick()
    {
        print("quit and add event here");
    }
    private void TutorialButtonClick()
    {
        GameEventBus.TutorialMenuClick();
    }
}
