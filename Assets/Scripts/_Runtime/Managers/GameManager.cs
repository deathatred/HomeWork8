using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }    
    private GameObject _player;
    public bool IsPlayerDead { get; private set; }
    private void OnEnable()
    {
        ExplodeAction.OnPlayerExploded += ExplodeActionOnPlayerExploded;
        Player2DMovement.Instance.OnFinishPlatformReached += Player2DMovementOnFinishPlatformReached;
        PlayerTriggers.Instance.OnPlayerHit += PlayerTriggersOnPlayerHit;
        if (Water.Instance != null)
        {
            Water.Instance.OnPlayerTouchedWater += WaterOnPlayedTouchedWater;
        }

    }
    private void OnDisable()
    {
        ExplodeAction.OnPlayerExploded -= ExplodeActionOnPlayerExploded;
        Player2DMovement.Instance.OnFinishPlatformReached -= Player2DMovementOnFinishPlatformReached;
        PlayerTriggers.Instance.OnPlayerHit -= PlayerTriggersOnPlayerHit;
        Water.Instance.OnPlayerTouchedWater -= WaterOnPlayedTouchedWater;
    }
    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        _player = GameObject.FindWithTag("Player");

    }

    private void PlayerTriggersOnPlayerHit(object sender, System.EventArgs e)
    {
        float bleedingDuration = 0.20f;
        StartCoroutine(PlayerDying(bleedingDuration));
    }

    private void WaterOnPlayedTouchedWater(object sender, System.EventArgs e)
    {
        float drowningDuration = 0.17f;
        StartCoroutine(PlayerDying(drowningDuration));
    }

    private void Player2DMovementOnFinishPlatformReached(object sender, System.EventArgs e)
    {
        EndGame();
    }
    private void ExplodeActionOnPlayerExploded(Vector3 obj)
    {
        float explodingDuration = 1.2f;
        StartCoroutine(PlayerDying(explodingDuration));
    }

    private void EndGame()
    {
        Time.timeScale = 0f;
    }

    private IEnumerator PlayerDying(float duration)
    {
        //BlockInput 
        IsPlayerDead = true;
        InputHandler playerInput = _player.GetComponent<InputHandler>();
        if (playerInput != null)
        {
            playerInput.enabled = false;
        }
        yield return new WaitForSeconds(duration);
        EndGame();
    }
}
