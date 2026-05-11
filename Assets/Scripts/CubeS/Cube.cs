using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(CubeCollisionHandler), typeof(ColorChanger))]
public class Cube : MonoBehaviour
{
    [SerializeField] private float _minLifetime = 2f;
    [SerializeField] private float _maxLifetime = 5f;

    private CubeCollisionHandler _collisionHandler;
    private ColorChanger _colorChanger;
    private Rigidbody _rigidbody;
    private bool _hasTouchedPlatform = false;

    public event Action<Cube> Expired;

    private void Awake()
    {
        _collisionHandler = GetComponent<CubeCollisionHandler>();
        _colorChanger = GetComponent<ColorChanger>();
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        _collisionHandler.PlatformTouched += OnPlatformTouched;
    }

    private void OnDisable()
    {
        _collisionHandler.PlatformTouched -= OnPlatformTouched;
    }

    public void Init()
    {
        _hasTouchedPlatform = false;
        _colorChanger.ResetColor();
        _rigidbody.velocity = Vector3.zero;
        _rigidbody.angularVelocity = Vector3.zero;
    }

    private void OnPlatformTouched(Platform platform)
    {
        if (_hasTouchedPlatform == false)
        {
            _hasTouchedPlatform = true;
            _colorChanger.SetRandomColor();

            float lifetime = UnityEngine.Random.Range(_minLifetime, _maxLifetime);
            StartCoroutine(LifeTimer(lifetime));
        }
    }

    private IEnumerator LifeTimer(float delay)
    {
        yield return new WaitForSeconds(delay);

        Expired?.Invoke(this);
    }
}
