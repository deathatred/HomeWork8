using NUnit.Framework;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerSkills : MonoBehaviour
{
    [SerializeField] private Transform platformPrefab;
    private InputHandler _inputHandler; 

    public float FirstSpellTimer { get; private set; } = 0f;
    public float SecondSpellTimer { get; private set; } = 0f;
    private float _firstSpellCooldown = 0f;

    [SerializeField] private List<SpellsSO> _playerSpells;

    private List<float> _spellTimers = new List<float>();
    private void Awake()
    {
        _inputHandler = GetComponent<InputHandler>();
        foreach (var spell in _playerSpells)
        {
            _spellTimers.Add(0f);
        }
    }

    private void Update()
    {
        for (int i = 0; i < _playerSpells.Count; i++)
        {
            print($"Handling {i} spell");
            HandleSpell(i);
        }

    }
    private void HandleSpell(int index)
    {
        BaseSpell spell = _playerSpells[index].spellPrefab.GetComponent<BaseSpell>();
        float cooldown = spell.SkillCooldown;
        if (_spellTimers[index] > 0)
        {
            _spellTimers[index] -= Time.deltaTime;
            _inputHandler.SetSpellPressedFalse(index);
        }
        if (_inputHandler.IsSpellPressed(index) && _spellTimers[index] <= 0f)
        {
            print("Im here");
            spell.Cast(transform);
            _inputHandler.SetSpellPressedFalse(index);
            _spellTimers[index] = cooldown;
        }
    }
  
    public float GetFirstSkillCooldown()
    {
        return _firstSpellCooldown;
    }
    public float GetSpellCooldownTimer(int index)
    {
        return _spellTimers[index];
    }
    public int GetSpellsCount()
    {
        return _playerSpells.Count;
    }
    public SpellsSO GetPlayerSkillWithIndex(int index)
    {
        return _playerSpells[index];
    }



}
