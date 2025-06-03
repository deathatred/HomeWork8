using UnityEngine;
using UnityEngine.UI;

public class SingleSpellUI : MonoBehaviour
{
    public static SingleSpellUI Instance;
    [SerializeField] private PlayerSkills _playerSkills;

    [SerializeField] private Image _firstSpellImage;
    [SerializeField] private Image _firstSpellCooldownImage;

    private void Awake()
    {
        
    }
    private void Update()
    {
        float firstSkillTimer = _playerSkills.FirstSkillTimer;
        float firstSkillCooldown = _playerSkills.FirstSkillCooldown;

        if (_playerSkills.FirstSkillTimer <= 0)
        {
            _firstSpellCooldownImage.gameObject.SetActive(false);
        }
        else
        {
            _firstSpellCooldownImage.gameObject.SetActive(true);
            _firstSpellCooldownImage.fillAmount = firstSkillTimer / firstSkillCooldown;
        }
    }
}
