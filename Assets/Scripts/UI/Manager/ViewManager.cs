using System.Collections.Generic;
using UnityEngine;

public class ViewManager : MonoBehaviour
{
    [SerializeField] private List<Canvas> _views;

    private void OnEnable()
    {
        SubscribeToEvents();
    }
    private void Start()
    {
        ChangeCanvas(0);
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
    }
    private void SubscribeToEvents()
    {
        GameEventBus.OnMenuButtonClicked += GameEventBusOnMenuButtonClicked;
    }
    private void UnsubscribeFromEvents()
    {
        GameEventBus.OnMenuButtonClicked -= GameEventBusOnMenuButtonClicked;
    }

    private void GameEventBusOnMenuButtonClicked()
    {
        ChangeCanvas(1);
    }
}
