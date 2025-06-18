using NUnit.Framework;
using UnityEngine;

public class ExplodingPlatform : Platform
{
    [SerializeField] private ExplodeAction _explodeAction;
    public override void TakeDamage(int amount)
    {
        _explodeAction.Execute();
      
    }
}
