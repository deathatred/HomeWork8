using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.ShaderGraph.Internal.KeywordDependentCollection;

public class GameView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _distanceToFinishText;
    [SerializeField] private TextMeshProUGUI _powerupsCollectedText;
    [SerializeField] private Button _menuButton;



    private void OnEnable()
    {
        SubscribeToEvents();
    }
    private void Start()
    {
        ShowDefaults();
    }
    private void OnDisable()
    {
        UnsubscribeFromEvents();
    }


 
    private void SubscribeToEvents()
    {
        GameEventBus.OnDistanceChanged += GameEventBusOnDistanceChanged;
        GameEventBus.OnPowerupPickedup += GameEventBus_OnPowerupPickedup;
        _menuButton.onClick.AddListener(() =>
        {
            GameEventBus.MenuButtonClick();
        });
    }


    private void UnsubscribeFromEvents()
    {
        GameEventBus.OnDistanceChanged -= GameEventBusOnDistanceChanged;
        GameEventBus.OnPowerupPickedup -= GameEventBus_OnPowerupPickedup;
        _menuButton?.onClick.RemoveAllListeners();
    }
    private void GameEventBus_OnPowerupPickedup(PowerupsSO obj)
    {
        CharacterModel.PowerupsCollected += 1;
        _powerupsCollectedText.text = $"Powerups Collected: {CharacterModel.PowerupsCollected}";
    }

    private void GameEventBusOnDistanceChanged(int distance)
    {
        _distanceToFinishText.text = $"Distance: {distance}";
    }
    private void ShowDefaults()
    {
        _distanceToFinishText.text = $"Distance: {0}";
        _powerupsCollectedText.text = $"Powerups Collected: {0}";
    }
}
