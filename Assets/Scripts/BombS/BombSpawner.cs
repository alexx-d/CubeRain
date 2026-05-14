using UnityEngine;

public class BombSpawner : Spawner<Bomb>
{
    [SerializeField] private CubeSpawner _cubeSpawner;

    private void OnEnable()
    {
        _cubeSpawner.CubeDestroyed += SpawnBomb;
    }

    private void OnDisable()
    {
        _cubeSpawner.CubeDestroyed -= SpawnBomb;
    }

    private void SpawnBomb(Vector3 position)
    {
        Bomb bomb = Get();
        bomb.transform.position = position;

        bomb.Exploded += OnBombExploded;
        bomb.Init();
    }

    private void OnBombExploded(Bomb bomb)
    {
        bomb.Exploded -= OnBombExploded;
        Release(bomb);
    }
}