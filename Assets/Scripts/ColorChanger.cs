using UnityEngine;

[RequireComponent(typeof(Renderer))]
public class ColorChanger : MonoBehaviour
{
    private Renderer _renderer;

    private void Awake()
    {
        _renderer = GetComponent<Renderer>();
    }

    public void SetRandomColor()
    {
        _renderer.material.color = Random.ColorHSV();
    }

    public void ResetColor()
    {
        _renderer.material.color = Color.white;
    }
}
