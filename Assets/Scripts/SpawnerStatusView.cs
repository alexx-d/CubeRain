using UnityEngine;
using TMPro;

public class SpawnerStatusView : MonoBehaviour
{
    [SerializeField] private CubeSpawner _cubeSpawner;
    [SerializeField] private BombSpawner _bombSpawner;

    [SerializeField] private TMP_Text _cubeInfoText;
    [SerializeField] private TMP_Text _bombInfoText;

    private void OnEnable()
    {
        _cubeSpawner.InfoChanged += UpdateCubeStats;
        _bombSpawner.InfoChanged += UpdateBombStats;

        UpdateCubeStats();
        UpdateBombStats();
    }

    private void OnDisable()
    {
        _cubeSpawner.InfoChanged -= UpdateCubeStats;
        _bombSpawner.InfoChanged -= UpdateBombStats;
    }

    private void UpdateCubeStats() =>
        DisplayStats(_cubeSpawner, _cubeInfoText, "Кубы");

    private void UpdateBombStats() =>
        DisplayStats(_bombSpawner, _bombInfoText, "Бомбы");

    private void DisplayStats<T>(Spawner<T> spawner, TMP_Text textField, string label) where T : MonoBehaviour
    {
        if (spawner == null || textField == null)
        {
            return;
        }

        textField.text = $"{label}:\n" +
                         $"Всего заспавнено: {spawner.TotalSpawned}\n" +
                         $"Создано в пуле: {spawner.CreatedCount}\n" +
                         $"Активно на сцене: {spawner.ActiveCount}";
    }
}