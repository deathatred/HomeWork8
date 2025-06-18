using UnityEngine;

public class SceneManager : MonoBehaviour
{
    public static SceneManager Instance { get; private set; }
    [SerializeField] private bool _toggleColorChange;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }
    public void ChangeAllColorsInScene()
    {
        if (_toggleColorChange)
        {
            var currentScene = UnityEngine.SceneManagement.SceneManager.GetActiveScene();

            foreach (var rootObj in currentScene.GetRootGameObjects())
            {
                var sprites = rootObj.GetComponentsInChildren<SpriteRenderer>();
                foreach (var sprite in sprites)
                {
                    sprite.color = Random.ColorHSV();
                }
            }
        }
    }

}