using NUnit.Framework.Internal.Builders;
using System.Collections;
using System.Collections.Generic;
using TMPro.EditorUtilities;
using UnityEngine;

public class PlayerEffects : MonoBehaviour
{
    //MAKE EVENTS HERE!!!
    [SerializeField] private TrailRenderer _trail;
    [SerializeField] private ParticleSystem _dashParticles;

    private void Start()
    {
        DisableTrail();
    }
    public void DisableTrail()
    {
        if (_trail.isVisible)
        {
            StartCoroutine(FadeTrail());
        }
        if (_dashParticles != null && _dashParticles.isPlaying)
        {
            _dashParticles.Stop();
        }
    } 

    private IEnumerator FadeTrail()
    {
        yield return new WaitForSeconds(_trail.time);

        _trail.enabled = false;
    }
    public void EnableTrail()
    {
        _trail.enabled = true;
        if (_dashParticles != null && !_dashParticles.isPlaying)
        {
            _dashParticles.Play();
        }
    }
}
