using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }    
    [SerializeField] private GameObject _player;
    private bool isGameOver = false;
    public bool IsPlayerDead { get; private set; }
    private void OnEnable()
    {
        ExplodeAction.OnPlayerExploded += ExplodeAction_OnPlayerExploded;
    }
    private void OnDisable()
    {
        ExplodeAction.OnPlayerExploded -= ExplodeAction_OnPlayerExploded;
    }
    private void Awake()
    {
        Instance = this;
    }
    private void ExplodeAction_OnPlayerExploded(Vector3 obj)
    {
        float explodingDuration = 1.2f;
        StartCoroutine(PlayerDying(explodingDuration));
    }

    private void Start()
    {
        _player = GameObject.FindWithTag("Player");
        Player2DMovement.Instance.OnFinishPlatformReached += Player2DMovement_OnFinishPlatformReached;
        if (Water.Instance != null)
        {
            Water.Instance.OnPlayerTouchedWater += Instance_OnPlayedTouchedWater;
        }
    }

    private void Instance_OnPlayedTouchedWater(object sender, System.EventArgs e)
    {
        float drowningDuration = 0.17f;
        StartCoroutine(PlayerDying(drowningDuration));
    }

    private void Player2DMovement_OnFinishPlatformReached(object sender, System.EventArgs e)
    {
        EndGame();
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
        playerInput.enabled = false;
        yield return new WaitForSeconds(duration);
        EndGame();
    }
}
