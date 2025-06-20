using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MenuView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _gameNameText;
    [SerializeField] private Button _startGameButton;
    [SerializeField] private TextMeshProUGUI _startGameTitle;
    [SerializeField] private Button _settingsButton;
    [SerializeField] private Button _exitButtom;

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
        
            GameEventBus.StartGameButtonClicked();
        });
        GameEventBus.OnStartGameButtonClicked += GameEventBusOnStartGameButtonClicked;
        GameEventBus.OnPlayerDead += GameEventBusOnPlayerDead;
    }

    private void UnsubscribeFromEvents()
    {
        _startGameButton.onClick.RemoveAllListeners();
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
