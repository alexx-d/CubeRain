using UnityEngine;
using System.Collections.Generic;

public class BombExploder : MonoBehaviour
{
    [SerializeField] private float _explosionForce = 500f;
    [SerializeField] private float _explosionRadius = 5f;
    [SerializeField] private ParticleSystem _explosionEffect;

    private const int MaxTargets = 50;
    private readonly Collider[] _overlapResults = new Collider[MaxTargets];

    public void Explode()
    {
        SpawnEffect(transform.position);

        int count = Physics.OverlapSphereNonAlloc(
            transform.position,
            _explosionRadius,
            _overlapResults
        );

        for (int i = 0; i < count; i++)
        {
            if (_overlapResults[i].TryGetComponent(out Rigidbody rigidbody))
            {
                rigidbody.AddExplosionForce(_explosionForce, transform.position, _explosionRadius);
            }
        }
    }

    private void SpawnEffect(Vector3 position)
    {
        Instantiate(_explosionEffect, position, Quaternion.identity);
    }
}