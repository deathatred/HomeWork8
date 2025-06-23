using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using TMPro;

public class RebindUI : MonoBehaviour
{
    [SerializeField] private int _bindingIndex = 0;
    [SerializeField] private TextMeshProUGUI _bindText;
    [SerializeField] private Button _rebindButton;
    [SerializeField] private InputActionReference _actionRef;

    private InputAction action => InputManager.Actions.FindAction(_actionRef.name);

    private void Awake()
    {
        string saved = PlayerPrefs.GetString(action.id + "_" + _bindingIndex, "");
        if (!string.IsNullOrEmpty(saved))
            action.ApplyBindingOverride(_bindingIndex, saved);
    }
    private void OnEnable()
    {
        SubscribeToEvents();
    }
    private void Start()
    {
        RebindRegistry.Register(this);
        ShowBinding();

    }
    private void OnDisable()
    {
        UnsubscribeFromEvents();
    }
    private void OnDestroy()
    {
        RebindRegistry.Unregister(this);
    }

    private void ShowBinding()
    {
        var b = action.bindings[_bindingIndex];
        _bindText.text = InputControlPath.ToHumanReadableString(
            b.effectivePath, InputControlPath.HumanReadableStringOptions.OmitDevice);
    }


    private void StartRebind()
    {
        _rebindButton.interactable = false;
        action.Disable();
        GameEventBus.StartRebind();
        action.PerformInteractiveRebinding(_bindingIndex)
            .WithCancelingThrough("<Keyboard>/escape")
            .OnCancel(op =>
            {
                action.Enable();
                _rebindButton.interactable = true;
                GameEventBus.FinishRebind();
            })
            .OnComplete(op =>
            {
                op.Dispose();
                action.Enable();
                _rebindButton.interactable = true;
                ShowBinding();
                SaveBinding();
                InputManager.RaiseBindingChanged(action, _bindingIndex);
                GameEventBus.FinishRebind();
                OnRebindComplete(action, _bindingIndex);
                InputManager.RaiseBindingChanged(action, _bindingIndex);
                GameEventBus.FinishRebind();
            })
            .Start();
    }

    private void SaveBinding()
    {
        PlayerPrefs.SetString(
            action.id + "_" + _bindingIndex,
            action.bindings[_bindingIndex].overridePath);
        PlayerPrefs.Save();
    }

    private void OnRebindComplete(InputAction action, int bindingIndex)
    {
        var newBindingPath = action.bindings[bindingIndex].effectivePath;
        foreach (var otherAction in InputManager.Actions)
        {
            if (otherAction == action)
            {
                continue;
            }            
            for (int i = 0; i < otherAction.bindings.Count; i++)
            {
                var binding = otherAction.bindings[i];
                if (binding.effectivePath == newBindingPath)
                {
                    if (!string.IsNullOrEmpty(binding.overridePath))
                    {
                        otherAction.ApplyBindingOverride(i, "");
                        foreach (var ui in RebindRegistry.AllRebinds)
                        {
                            if (ui._actionRef.action.name == otherAction.name && ui._bindingIndex == i)
                            {
                                ui.Clear();
                            }
                        }
                    }
                }
            }
        }
    }

    public void RefreshBindingDisplay()
    {
        SaveBinding();
        ShowBinding();
    }
    public void Clear()
    {
        _bindText.text = string.Empty;
    }
    private void SubscribeToEvents()
    {
        _rebindButton.onClick.AddListener(StartRebind);
    }
    private void UnsubscribeFromEvents()
    {
        _rebindButton.onClick.RemoveAllListeners();
    }

}
