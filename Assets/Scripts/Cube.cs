using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Cube : MonoBehaviour
{
    [SerializeField] private float _minLifetime = 2f;
    [SerializeField] private float _maxLifetime = 5f;

    private Spawner<Cube> _spawner;
    private Rigidbody _rb;
    private Renderer _renderer;
    private bool _hasTouchedPlatform = false;

    private void Awake()
    {
        _renderer = GetComponent<Renderer>();
        _rb = GetComponent<Rigidbody>();
    }

    public void Init(Spawner<Cube> spawner)
    {
        _spawner = spawner;
        _hasTouchedPlatform = false;
        _renderer.material.color = Color.white;

        _rb.velocity = Vector3.zero;
        _rb.angularVelocity = Vector3.zero;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (_hasTouchedPlatform == false && collision.gameObject.TryGetComponent(out Platform platform))
        {
            _hasTouchedPlatform = true;

            _renderer.material.color = Random.ColorHSV();

            float lifetime = Random.Range(_minLifetime, _maxLifetime);
            StartCoroutine(LifeTimer(lifetime));
        }
    }

    private IEnumerator LifeTimer(float delay)
    {
        yield return new WaitForSeconds(delay);

        if (_spawner != null)
        {
            _spawner.Release(this);
        }
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }
}
