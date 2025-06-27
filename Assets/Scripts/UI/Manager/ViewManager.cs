using System.Collections.Generic;
using UnityEngine;

public class ViewManager : MonoBehaviour
{
    [SerializeField] private List<Canvas> _views = new();
    private static bool _gameWasRestarted;

    private void OnEnable()
    {
        SubscribeToEvents();
    }
    private void Start()
    {
        if (ViewManager._gameWasRestarted)
        {
            ChangeCanvas(0);
            ViewManager._gameWasRestarted = false;
        }
        else
        {
            print("HERE");
            ChangeCanvas(1);
        }
    }
    private void OnDisable()
    {
        UnsubscribeFromEvents();
    }
    public void ChangeCanvas(int id)
    {
        if (id > _views.Count)
        {
            Debug.LogError($"This canvas id {id}, does not exist");
            return;
        }
        foreach (Canvas canvas in _views)
        {
            canvas.enabled = false;
        }
        _views[id].enabled = true;
        GameEventBus.CanvasChange(id);
    }
    private void SubscribeToEvents()
    {
        GameEventBus.OnMenuButtonClicked += GameEventBusOnMenuButtonClicked;
        GameEventBus.OnStartGameButtonClicked += GameEventBusOnStartGameButtonClicked;
        GameEventBus.OnPlayerDead += GameEventBusOnPlayerDead;
        GameEventBus.OnRestartButtonClicked += GameEventBusOnRestartButtonClicked;
        GameEventBus.OnBackButtonClicked += GameEventBusOnBackButtonClicked;
        GameEventBus.OnSettingButtonClicked += GameEventBusOnSettingButtonClicked;
        GameEventBus.OnEscapePressed += GameEventBus_OnEscapePressed;
        GameEventBus.OnTutorialPressed += GameEventBus_OnTutorialPressed;
    }
    private void UnsubscribeFromEvents()
    {
        GameEventBus.OnMenuButtonClicked -= GameEventBusOnMenuButtonClicked;
        GameEventBus.OnStartGameButtonClicked -= GameEventBusOnStartGameButtonClicked;
        GameEventBus.OnPlayerDead -= GameEventBusOnPlayerDead;
        GameEventBus.OnRestartButtonClicked -= GameEventBusOnRestartButtonClicked;
        GameEventBus.OnBackButtonClicked -= GameEventBusOnBackButtonClicked;
        GameEventBus.OnSettingButtonClicked -= GameEventBusOnSettingButtonClicked;
        GameEventBus.OnEscapePressed -= GameEventBus_OnEscapePressed;
        GameEventBus.OnTutorialPressed -= GameEventBus_OnTutorialPressed;
    }
    private void GameEventBus_OnTutorialPressed()
    {
        ChangeCanvas(4);
    }
    private void GameEventBusOnSettingButtonClicked()
    {
        ChangeCanvas(3);
    }

    private void GameEventBusOnBackButtonClicked(BackButtonContext obj)
    {
        if (obj is BackButtonContext.Settings || obj is BackButtonContext.Tutorial)
        {
            ChangeCanvas(1);
        }
    }
    private void GameEventBusOnRestartButtonClicked()
    {
        ViewManager._gameWasRestarted = true;
    }

    private void GameEventBusOnPlayerDead()
    {
        ChangeCanvas(2);
    }

    private void GameEventBusOnStartGameButtonClicked()
    {
        ChangeCanvas(0);
    }
    private void GameEventBus_OnEscapePressed()
    {
        ChangeCanvas(1);
    }


    private void GameEventBusOnMenuButtonClicked()
    {
        ChangeCanvas(1);
    }
}
