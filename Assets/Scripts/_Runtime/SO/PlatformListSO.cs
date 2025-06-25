using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using NUnit.Framework;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "PlatformList", menuName = "PlatformListSO")]
public class PlatformListSO : ScriptableObject
{
    [SerializeField] public List<PlatformSO> PlatformList;

}