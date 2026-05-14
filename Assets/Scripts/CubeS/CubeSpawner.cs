using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

public class CubeSpawner : Spawner<Cube>
{
    [SerializeField] private float _spawnRadius = 5f;
    [SerializeField] private bool _spawnInSphere = false;
    [SerializeField] private float _repeatRate = 1f;

    public event Action<Vector3> CubeDestroyed;

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
                    randomOffset = UnityEngine.Random.insideUnitSphere * _spawnRadius;
                }
                else
                {
                    Vector2 circle = UnityEngine.Random.insideUnitCircle * _spawnRadius;
                    randomOffset = new Vector3(circle.x, 0, circle.y);
                }

                cube.transform.SetPositionAndRotation(transform.position + randomOffset, UnityEngine.Random.rotation);

                cube.Init();
                cube.Expired += OnCubeExpired;
            }

            yield return wait;
        }
    }

    private void OnCubeExpired(Cube cube)
    {
        cube.Expired -= OnCubeExpired;
        Vector3 position = cube.transform.position;
        Release(cube);

        CubeDestroyed?.Invoke(position);
    }
}
