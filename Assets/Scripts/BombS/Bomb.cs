using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(BombExploder), typeof(BombAlphaChanger))]
public class Bomb : MonoBehaviour
{
    [SerializeField] private float _minLifetime = 2f;
    [SerializeField] private float _maxLifetime = 5f;

    private Rigidbody _rigidbody;
    private BombExploder _exploder;
    private BombAlphaChanger _alphaChanger;

    public event Action<Bomb> Exploded;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _exploder = GetComponent<BombExploder>();
        _alphaChanger = GetComponent<BombAlphaChanger>();
    }

    public void Init()
    {
        _rigidbody.velocity = Vector3.zero;
        _rigidbody.angularVelocity = Vector3.zero;
        _alphaChanger.ResetAlpha();

        float lifetime = UnityEngine.Random.Range(_minLifetime, _maxLifetime);
        StartCoroutine(LifeTimer(lifetime));
    }

    private IEnumerator LifeTimer(float duration)
    {
        float elapsed = 0;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            _alphaChanger.UpdateAlpha(elapsed / duration);
            yield return null;
        }

        _exploder.Explode();
        Exploded?.Invoke(this);
    }
}
