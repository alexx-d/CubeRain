using UnityEngine;

public class Platform : MonoBehaviour
{
    private Renderer _renderer;

    private void Awake()
    {
        _renderer = GetComponent<Renderer>();
    }

    void Start()
    {
        _renderer.material.color = Random.ColorHSV();
    }
}
