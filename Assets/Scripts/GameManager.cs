using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    bool isGameOver = false;
    private void Start()
    {
        Player2DMovement.Instance.OnFinishPlatformReached += Player2DMovement_OnFinishPlatformReached;
        Water.Instance.OnPlayerTouchedWater += Instance_OnPlayedTouchedWater;
    }

    private void Instance_OnPlayedTouchedWater(object sender, System.EventArgs e)
    {
        StartCoroutine(PlayerDrowning());
    }

    private void Player2DMovement_OnFinishPlatformReached(object sender, System.EventArgs e)
    {
        EndGame();
    }

    private void EndGame()
    {
        Time.timeScale = 0f;
    }
    
    private IEnumerator PlayerDrowning()
    {
        //BlockInput
        yield return new WaitForSeconds(0.15f);
        EndGame();
    }
}
