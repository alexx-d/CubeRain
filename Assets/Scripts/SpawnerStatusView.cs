using UnityEngine;
using TMPro;

public class SpawnerStatusView : MonoBehaviour
{
    [SerializeField] private MonoBehaviour _spawnerObject;
    [SerializeField] private TMP_Text _textField;
    [SerializeField] private string _label;

    private ISpawnerInfo _spawner;

    private void Awake()
    {
        _spawner = _spawnerObject as ISpawnerInfo;
    }

    private void OnEnable()
    {
        if (_spawner == null)
        {
            return;
        }

        _spawner.InfoChanged += UpdateText;
        UpdateText();
    }

    private void OnDisable()
    {
        if (_spawner is not null)
        {
            _spawner.InfoChanged -= UpdateText;
        }
    }

    private void UpdateText()
    {
        _textField.text = $"{_label}:\n" +
                         $"Всего: {_spawner.TotalSpawned}\n" +
                         $"В пуле: {_spawner.CreatedCount}\n" +
                         $"Активно: {_spawner.ActiveCount}";
    }
}