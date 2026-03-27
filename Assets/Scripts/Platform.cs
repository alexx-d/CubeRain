using UnityEngine;

[RequireComponent(typeof(ColorChanger))]
public class Platform : MonoBehaviour
{
    private ColorChanger _colorChanger;

    private void Awake()
    {
        _colorChanger = GetComponent<ColorChanger>();
    }

    public void Start()
    {
        _colorChanger.SetRandomColor();
    }
}
