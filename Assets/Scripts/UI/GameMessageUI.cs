using TMPro;
using TMPro.EditorUtilities;
using UnityEngine;
using UnityEngine.UI;

public class GameMessageUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _youWonMessage;
    [SerializeField] private TextMeshProUGUI _gameOverMessage;
    [SerializeField] private TextMeshProUGUI _gameStartMessage;
    private void Start()
    {
        Player2DMovement.Instance.OnFinishPlatformReached += PlayerMovement_OnFinishPlatformReached;
    }

    private void PlayerMovement_OnFinishPlatformReached(object sender, System.EventArgs e)
    {
         
    }
}
