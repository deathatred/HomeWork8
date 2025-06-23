using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using TMPro;

public class RebindUI : MonoBehaviour
{
    [SerializeField] private int bindingIndex = 0;
    [SerializeField] private TextMeshProUGUI bindText;
    [SerializeField] private Button rebindButton;
    [SerializeField] private InputActionReference actionRef;

    private InputAction action => InputManager.Actions.FindAction(actionRef.name);

    private void Awake()
    {
        string saved = PlayerPrefs.GetString(action.id + "_" + bindingIndex, "");
        if (!string.IsNullOrEmpty(saved))
            action.ApplyBindingOverride(bindingIndex, saved);
    }
    private void Start()
    {
        RebindRegistry.Register(this);
        ShowBinding();
        rebindButton.onClick.AddListener(StartRebind);
    }
    private void OnDestroy()
    {
        RebindRegistry.Unregister(this);
    }

    private void ShowBinding()
    {
        var b = action.bindings[bindingIndex];
        bindText.text = InputControlPath.ToHumanReadableString(
            b.effectivePath, InputControlPath.HumanReadableStringOptions.OmitDevice);
    }

    private void StartRebind()
    {
        rebindButton.interactable = false;
        action.Disable();
        GameEventBus.StartRebind();
        action.PerformInteractiveRebinding(bindingIndex)
            .WithCancelingThrough("<Keyboard>/escape")
            .OnCancel(op =>
            {
                action.Enable();
                rebindButton.interactable = true;
                GameEventBus.FinishRebind();
            })
            .OnComplete(op =>
            {
                op.Dispose();
                action.Enable();
                rebindButton.interactable = true;
                ShowBinding();
                SaveBinding();
                InputManager.RaiseBindingChanged(action, bindingIndex);
                GameEventBus.FinishRebind();
                OnRebindComplete(action, bindingIndex);
                InputManager.RaiseBindingChanged(action, bindingIndex);
                GameEventBus.FinishRebind();
            })
            .Start();
    }

    private void SaveBinding()
    {
        PlayerPrefs.SetString(
            action.id + "_" + bindingIndex,
            action.bindings[bindingIndex].overridePath);
        PlayerPrefs.Save();
    }

    private void OnRebindComplete(InputAction action, int bindingIndex)
    {
        var newBindingPath = action.bindings[bindingIndex].effectivePath;
        foreach (var otherAction in InputManager.Actions)
        {
            if (otherAction == action)
                continue;

            for (int i = 0; i < otherAction.bindings.Count; i++)
            {
                var binding = otherAction.bindings[i];
                if (binding.effectivePath == newBindingPath)
                {
                    if (!string.IsNullOrEmpty(binding.overridePath))
                    {
                        otherAction.RemoveBindingOverride(i);
                        foreach (var ui in RebindRegistry.AllRebinds)
                        {
                            if (ui.actionRef.action.name == otherAction.name && ui.bindingIndex == i)
                            {
                                print("KYS");
                                ui.RefreshBindingDisplay();
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

}
