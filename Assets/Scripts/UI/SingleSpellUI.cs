using UnityEngine;
using UnityEngine.UI;

public class SingleSpellUI : MonoBehaviour
{
    public static SingleSpellUI Instance;
    [SerializeField] private PlayerSkills _playerSkills;
    [SerializeField] private int _playerSkillsIndex;
    [SerializeField] private Image _spellImage;
    [SerializeField] private Image _spellCooldownImage;

    private void Awake()
    {

    }
    private void Update()
    {
        SpellsSO spellSO = _playerSkills.GetPlayerSkillWithIndex(_playerSkillsIndex);
        BaseSpell spell = spellSO.spellPrefab.GetComponent<BaseSpell>();
        float timer = _playerSkills.GetSpellCooldownTimer(_playerSkillsIndex);
        float cooldown = spell.SkillCooldown;

        _spellImage.sprite = spellSO.spellImage;

        if (timer <= 0f)
        {
            _spellCooldownImage.gameObject.SetActive(false);
        }
        else
        {

            _spellCooldownImage.gameObject.SetActive(true);
            _spellCooldownImage.fillAmount = timer / cooldown;
        }
    }
}
