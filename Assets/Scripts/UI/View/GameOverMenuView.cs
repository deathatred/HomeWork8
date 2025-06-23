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
        _menuButton.onClick.AddListener(() =>
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(
     UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex);
            GameEventBus.MenuButtonClick();
        });
        _restartButton.onClick.AddListener(() =>
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(
      UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex);
            GameEventBus.RestartClick();
        });
        GameEventBus.OnDistanceChanged += GameEventBusOnDistanceChanged;
    }
    private void UnsubscribeFromEvents()
    {
        _menuButton.onClick.RemoveAllListeners();
        _restartButton?.onClick.RemoveAllListeners();
        GameEventBus.OnDistanceChanged -= GameEventBusOnDistanceChanged;
    }
    private void GameEventBusOnDistanceChanged(int obj)
    {
        SetBestDistanceText();
    }
}
