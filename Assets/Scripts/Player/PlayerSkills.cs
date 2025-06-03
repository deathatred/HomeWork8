using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerSkills : MonoBehaviour
{
    [SerializeField] private Transform platformPrefab;
    private InputHandler _inputHandler;

    public float FirstSkillCooldown { get; private set; } = 4f;
    public float FirstSkillTimer { get; private set; } = 0f;
    private void Awake()
    {
        _inputHandler = GetComponent<InputHandler>();
    }
    public void SpawnPlatformUnderPlayer()
    {

        float platformYOffset = 4.5f;
        Vector3 platfromLocation = new Vector3(transform.position.x, transform.position.y - platformYOffset, transform.position.z);
        Instantiate(platformPrefab, platfromLocation, Quaternion.identity);
    }
    private void Update()
    {

        if (FirstSkillTimer > 0)
        {
            FirstSkillTimer -= Time.deltaTime;
            _inputHandler.SetFirstSpellPressedFalse();
        }
        if (_inputHandler.FirstSpellPressed && FirstSkillTimer <= 0f)
        {
            print("Im here");
            SpawnPlatformUnderPlayer();
            _inputHandler.SetFirstSpellPressedFalse();
            FirstSkillTimer = FirstSkillCooldown;
        }
    }



}
