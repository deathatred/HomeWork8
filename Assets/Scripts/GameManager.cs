using UnityEngine;

public class GameManager : MonoBehaviour
{
    bool isGameOver = false;


    private void EndGame()
    {
        Time.timeScale = 0f;
    }
}
