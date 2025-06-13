using System.Collections;
using UnityEngine;

public class ExplodeAction : ActionBase
{
    [SerializeField] private ParticleSystem _explodeEffectParticles;
    [SerializeField] private SpriteRenderer _targetRenderer;
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

        Color originalColor = _targetRenderer.color;

        for (int i = 0; i < flashCount; i++)
        {
            _targetRenderer.color = Color.white;
            yield return new WaitForSeconds(flashDuration);
            _targetRenderer.color = originalColor;
            yield return new WaitForSeconds(flashDuration);
        }
        if (_explodeEffectParticles != null)
        {
            _explodeEffectParticles.Play();
        }
        Destroy(gameObject);
    }
}
