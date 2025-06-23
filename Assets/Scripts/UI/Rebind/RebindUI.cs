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

    private void Start()
    {
        ShowBinding();
        rebindButton.onClick.AddListener(StartRebind);
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

        action.PerformInteractiveRebinding(bindingIndex)
            .WithCancelingThrough("<Keyboard>/escape")
            .OnCancel(op =>
            {
                action.Enable();
                rebindButton.interactable = true;
            })
            .OnComplete(op =>
            {
                op.Dispose();
                action.Enable();
                rebindButton.interactable = true;
                ShowBinding();
                SaveBinding();
                InputManager.RaiseBindingChanged(action, bindingIndex);  // ← кидаємо івент
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

    private void Awake()
    {
        string saved = PlayerPrefs.GetString(action.id + "_" + bindingIndex, "");
        if (!string.IsNullOrEmpty(saved))
            action.ApplyBindingOverride(bindingIndex, saved);
    }
}
