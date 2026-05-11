using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeSpawner : Spawner<Cube>
{
    [SerializeField] private float _spawnRadius = 5f;
    [SerializeField] private bool _spawnInSphere = false;
    [SerializeField] private float _repeatRate = 1f;

    [SerializeField] private BombSpawner _spawner;

    private void Start()
    {
        StartCoroutine(SpawnRoutine());
    }

    private IEnumerator SpawnRoutine()
    {
        var wait = new WaitForSeconds(_repeatRate);

        while (enabled)
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

            yield return wait;
        }
    }

    private void OnCubeExpired(Cube cube)
    {
        cube.Expired -= OnCubeExpired;

        Release(cube);

        Bomb bomb = _spawner.Get();
        bomb.transform.SetPositionAndRotation(cube.transform.position, Quaternion.identity);

        bomb.Exploded += OnBombExploded;
        bomb.Init();
    }

    private void OnBombExploded(Bomb bomb)
    {
        bomb.Exploded -= OnBombExploded;

        _spawner.Release(bomb);
    }
}
