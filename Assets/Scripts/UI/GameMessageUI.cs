using TMPro;
using TMPro.EditorUtilities;
using UnityEngine;
using UnityEngine.UI;

public class GameMessageUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _youEscapedMessage;
    [SerializeField] private TextMeshProUGUI _gameOverMessage;
    [SerializeField] private TextMeshProUGUI _gameStartMessage;
    private void OnEnable()
    {
        ExplodeAction.OnPlayerExploded += ExplodeAction_OnPlayerExploded;
    }
    private void OnDisable()
    {
        ExplodeAction.OnPlayerExploded -= ExplodeAction_OnPlayerExploded;
    }
    private void ExplodeAction_OnPlayerExploded(Vector3 obj)
    {
        _gameOverMessage.gameObject.SetActive(true);
    }

   
    private void Start()
    {
        Player2DMovement.Instance.OnFinishPlatformReached += PlayerMovement_OnFinishPlatformReached;
        PlayerTriggers.Instance.OnPlayerHit += PlayerTriggers_OnPlayerHit;
        if (Water.Instance != null)
        {
          Water.Instance.OnPlayerTouchedWater += Water_OnPlayedTouchedWater;
        }
       
    }

    private void PlayerTriggers_OnPlayerHit(object sender, System.EventArgs e)
    {
        _gameOverMessage.gameObject.SetActive(true);
    }

    private void Water_OnPlayedTouchedWater(object sender, System.EventArgs e)
    {
        _gameOverMessage.gameObject.SetActive(true);
    }

    private void PlayerMovement_OnFinishPlatformReached(object sender, System.EventArgs e)
    {
         _youEscapedMessage.gameObject.SetActive(true);
    }
}
