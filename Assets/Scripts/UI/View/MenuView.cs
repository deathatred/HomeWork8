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
        _startGameButton.onClick.AddListener(() =>
        {
            GameEventBus.StartGameButtonClick();
        });
        _settingsButton.onClick.AddListener(() =>
        {
            GameEventBus.SettingButtonClick();
        });
        _exitButton.onClick.AddListener(() =>
        {
            print("quit");
            Application.Quit();
        });
        GameEventBus.OnStartGameButtonClicked += GameEventBusOnStartGameButtonClicked;
        GameEventBus.OnPlayerDead += GameEventBusOnPlayerDead;
    }
    private void UnsubscribeFromEvents()
    {
        _startGameButton.onClick.RemoveAllListeners();
        _settingsButton.onClick.RemoveAllListeners(); 
        GameEventBus.OnStartGameButtonClicked -= GameEventBusOnStartGameButtonClicked;
        GameEventBus.OnPlayerDead -= GameEventBusOnPlayerDead;
        _exitButton.onClick.RemoveAllListeners();
    }
    private void GameEventBusOnStartGameButtonClicked()
    {
        _startGameTitle.text = "Continue";
    }

    private void GameEventBusOnPlayerDead()
    {
        _startGameTitle.text = "Start";
    }
}
