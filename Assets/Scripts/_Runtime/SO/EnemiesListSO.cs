using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using NUnit.Framework;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "EnemiesList", menuName = "EnemiesListSO")]
public class EnemiesListSO : ScriptableObject
{
    [SerializeField] public List<EnemySO> EnemiesList;

}
