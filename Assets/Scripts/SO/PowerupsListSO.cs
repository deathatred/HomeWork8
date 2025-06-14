using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using NUnit.Framework;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "PowerupList", menuName = "PowerupListSO")]
public class PowerupsListSO : ScriptableObject
{
    [SerializeField] public List<PowerupsSO> PowerupList;
  
}