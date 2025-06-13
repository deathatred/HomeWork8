using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ExplodeAction : ActionBase
{
    [SerializeField] private ParticleSystem _explodeEffectParticles;
    [SerializeField] private List<SpriteRenderer> _targetRenderersList;
    [SerializeField] private float _explosionRadius = 3f;
    public static event Action<Vector3> OnPlayerExploded; 
    protected override void ExecuteInternal()
    {
        Explode();
    }
    private void Explode()
    {
        StartCoroutine(StartExploding());
    }
    private IEnumerator StartExploding()
    {
        int flashCount = 3;
        float flashDuration = 0.2f;

        Color originalColor = _targetRenderersList[0].color;

        for (int i = 0; i < flashCount; i++)
        {
            for (int y = 0; y < _targetRenderersList.Count; y++)
            {
                _targetRenderersList[y].color = Color.white;
            }
            yield return new WaitForSeconds(flashDuration);
            for (int y = 0; y < _targetRenderersList.Count; y++)
            {
                _targetRenderersList[y].color = originalColor;
            }
            yield return new WaitForSeconds(flashDuration);
        }
        if (_explodeEffectParticles != null)
        {
            ParticleSystem spawnedParticles = Instantiate(_explodeEffectParticles, transform.position, Quaternion.identity);
            spawnedParticles.Play();
        }
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, _explosionRadius);
        foreach (var hit in hits)
        {
            if (hit.CompareTag("Player"))
            {
                OnPlayerExploded?.Invoke(transform.position);
            }
        }
    }

}
