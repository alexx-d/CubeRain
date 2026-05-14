using System;

public interface ISpawnerInfo
{
    event Action InfoChanged;
    int TotalSpawned { get; }
    int CreatedCount { get; }
    int ActiveCount { get; }
}