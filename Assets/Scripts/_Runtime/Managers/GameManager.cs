using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    private GameObject _player;
    public bool IsPlayerDead { get; private set; }
    private void OnEnable()
    {
        SubscribeToEvents();

    }
    private void OnDisable()
    {
        UnsubscribeFromEvents();
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
    private void SubscribeToEvents()
    {
        ExplodeAction.OnPlayerExploded += ExplodeActionOnPlayerExploded;
        Player2DMovement.Instance.OnFinishPlatformReached += Player2DMovementOnFinishPlatformReached;
        PlayerTriggers.Instance.OnPlayerHit += PlayerTriggersOnPlayerHit;
        if (Water.Instance != null)
        {
            Water.Instance.OnPlayerTouchedWater += WaterOnPlayedTouchedWater;
        }
        GameEventBus.OnCanvasChanged += GameEventBusOnCanvasChanged;
        UnityEngine.SceneManagement.SceneManager.sceneLoaded += OnSceneLoaded;
    }
    private void UnsubscribeFromEvents()
    {
        ExplodeAction.OnPlayerExploded -= ExplodeActionOnPlayerExploded;
        Player2DMovement.Instance.OnFinishPlatformReached -= Player2DMovementOnFinishPlatformReached;
        PlayerTriggers.Instance.OnPlayerHit -= PlayerTriggersOnPlayerHit;
        if (Water.Instance != null)
        {
            Water.Instance.OnPlayerTouchedWater -= WaterOnPlayedTouchedWater;
        }
        GameEventBus.OnCanvasChanged -= GameEventBusOnCanvasChanged;
        UnityEngine.SceneManagement.SceneManager.sceneLoaded -= OnSceneLoaded;
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
        GameEventBus.PlayerDead();
    }

    private void GameEventBusOnCanvasChanged(int id)
    {
        switch (id)
        {
            case 0:
                Time.timeScale = 1; break;
            case 1:
                Time.timeScale = 0; break;
            case 2:
                Time.timeScale = 0; break;
        }
    }
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        CharacterModel.ResetStats();
    }

}
