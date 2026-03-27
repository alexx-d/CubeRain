using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeSpawner : Spawner<Cube>
{
    [SerializeField] private float _spawnRadius = 5f;
    [SerializeField] private bool _spawnInSphere = false;
    [SerializeField] private float _repeatRate = 1f;

    private void Start()
    {
        InvokeRepeating(nameof(GetRandomly), 0.0f, _repeatRate);
    }

    public Cube GetRandomly()
    {
        Cube cube = Get();

        if (cube != null)
        {
            Vector3 randomOffset;

            if (_spawnInSphere)
            {
                randomOffset = Random.insideUnitSphere * _spawnRadius;
            }
            else
            {
                Vector2 circle = Random.insideUnitCircle * _spawnRadius;
                randomOffset = new Vector3(circle.x, 0, circle.y);
            }

            cube.transform.SetPositionAndRotation(transform.position + randomOffset, Random.rotation);

            cube.Init();
            cube.Expired += OnCubeExpired;
        }

        return cube;
    }

    private void OnCubeExpired(Cube cube)
    {
        cube.Expired -= OnCubeExpired;

        Release(cube);
    }
}
