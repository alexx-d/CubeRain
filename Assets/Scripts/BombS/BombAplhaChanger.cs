using UnityEngine;

[RequireComponent (typeof(Rigidbody))]
public class BombAlphaChanger : MonoBehaviour
{
    private Renderer _renderer;
    private Color _originalColor;

    private void Awake()
    {
        _renderer = GetComponent<Renderer>();
        _originalColor = _renderer.material.color;

        SetupFadeMode();
    }

    public void ResetAlpha()
    {
        _renderer.material.color = _originalColor;
    }

    public void UpdateAlpha(float progress)
    {
        Color color = _renderer.material.color;
        color.a = Mathf.Lerp(1f, 0f, progress);
        _renderer.material.color = color;
    }

    private void SetupFadeMode()
    {
        Material material = _renderer.material;
        material.SetFloat("_Mode", 2);
        material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
        material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
        material.SetInt("_ZWrite", 0);
        material.DisableKeyword("_ALPHATEST_ON");
        material.EnableKeyword("_ALPHABLEND_ON");
        material.DisableKeyword("_ALPHAPREMULTIPLY_ON");
        material.renderQueue = 3000;
    }
}