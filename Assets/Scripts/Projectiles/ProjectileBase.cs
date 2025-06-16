using System;
using UnityEngine;

public abstract class ProjectileBase : MonoBehaviour
{
    protected abstract int Damage { get; set; }
    public abstract void Setup(Vector3 target);
 
}
