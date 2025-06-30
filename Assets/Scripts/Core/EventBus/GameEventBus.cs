using System;
using System.Security.Cryptography.X509Certificates;
using UnityEditor;
using UnityEngine;

public static class GameEventBus
{
    //UI Events 
    public static event Action OnMenuButtonClicked;
    public static event Action OnStartGameButtonClicked;
    public static event Action OnRestartButtonClicked;
    public static event Action OnMusicPlusButtonClicked;
    public static event Action OnMusicMinusButtonClicked;
    public static event Action<BackButtonContext> OnBackButtonClicked;
    public static event Action OnSettingButtonClicked;
    public static event Action<int> OnCanvasChanged;
    public static event Action OnRebindStarted;
    public static event Action OnRebindFinished;
    public static event Action OnTutorialPressed;
    public static event Action OnPlatformInfoPressed;
    //In Game Events
    public static event Action OnPlayerDead;
    public static event Action<int> OnDistanceChanged;
    public static event Action<PowerupsSO> OnPowerupPickedup;
    public static event Action OnEscapePressed;


    public static void ChangeDistance(int newDistance)
    {
        OnDistanceChanged?.Invoke(newDistance);
    }
    public static void PowerupPickedUp(PowerupsSO powerupSO)
    {
        OnPowerupPickedup?.Invoke(powerupSO);
    }
    public static void MenuButtonClick()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(
    UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex);
        OnMenuButtonClicked?.Invoke();
    }
    public static void StartGameButtonClick()
    {
        OnStartGameButtonClicked?.Invoke();
    }
    public static void CanvasChange(int id)
    {
        OnCanvasChanged?.Invoke(id);
    }
    public static void PlayerDead()
    {
        OnPlayerDead?.Invoke();
    }
    public static void RestartClick()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(
    UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex);
        OnRestartButtonClicked?.Invoke();
    }
    public static void MusicPlusButtonClick()
    {
        OnMusicPlusButtonClicked?.Invoke();
    }
    public static void MusicMinusButtonClick()
    {
        OnMusicMinusButtonClicked?.Invoke();
    }
    public static void BackButtonClick(BackButtonContext context)
    {
        OnBackButtonClicked?.Invoke(context);
    }
    public static void SettingButtonClick()
    {
        OnSettingButtonClicked?.Invoke();
    }
    public static void StartRebind()
    {
        OnRebindStarted?.Invoke();
    }
    public static void FinishRebind()
    {
        OnRebindFinished?.Invoke();
    }
    public static void PressEscape()
    {
        OnEscapePressed?.Invoke();
    }
    public static void TutorialMenuClick()
    {
        OnTutorialPressed?.Invoke();
    }
    public static void PlatformInfoClick()
    {
        OnPlatformInfoPressed?.Invoke();
    }
}
