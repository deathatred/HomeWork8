using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;


public class GameOverMenuView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _gameOverText;
    [SerializeField] private TextMeshProUGUI _bestDistanceText;
    [SerializeField] private Button _menuButton;
    [SerializeField] private Button _restartButton;
    private void OnEnable()
    {
        SetBestDistanceText();
        SubscribeToEvents();
    }
    private void OnDisable()
    {
        UnsubscribeFromEvents();
    }
    private void SetBestDistanceText()
    {
        _bestDistanceText.text = $"Best distance: {PlayerPrefsManager.GetRecordDistance()}";
    }
    private void SubscribeToEvents()
    {
        _menuButton.onClick.AddListener(MenuButtonClick);
        _restartButton.onClick.AddListener(RestartClick);
        GameEventBus.OnDistanceChanged += GameEventBusOnDistanceChanged;
    }
    private void UnsubscribeFromEvents()
    {
        _menuButton.onClick.RemoveListener(MenuButtonClick);
        _restartButton?.onClick.RemoveListener(RestartClick);
        GameEventBus.OnDistanceChanged -= GameEventBusOnDistanceChanged;
    }
    private void GameEventBusOnDistanceChanged(int amountOfDistance)
    {
        SetBestDistanceText();
    }
    private void MenuButtonClick()
    {
        GameEventBus.MenuButtonClick();
    }
    private void RestartClick()
    {
        GameEventBus.RestartClick();
    }
}
