using System;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class GameEventBus : MonoBehaviour
{
    public static event Action<int> OnDistanceChanged;
    public static event Action<PowerupsSO> OnPowerupPickedup;
    public static event Action OnMenuButtonClicked;
    public static event Action OnStartGameButtonClicked;
    public static event Action<int> OnCanvasChanged;

    public static void ChangeDistance(int newDistance)
    {
        OnDistanceChanged?.Invoke(newDistance);
    }
    public static void PowerupPickedUp(PowerupsSO powerupSO)
    {
        OnPowerupPickedup?.Invoke(powerupSO);
    }
    public static void MenuButtonClicked()
    {
        OnMenuButtonClicked?.Invoke();
    }
    public static void StartGameButtonClicked()
    {
        OnStartGameButtonClicked?.Invoke();
    }
    public static void CanvasChanged(int id)
    {
        OnCanvasChanged?.Invoke(id);
    } 
}
