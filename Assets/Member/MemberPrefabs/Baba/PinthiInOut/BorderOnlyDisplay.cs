using UnityEngine;
using UnityEngine.UI;
[RequireComponent(typeof(SpriteRenderer))]
public class BorderOnlyDisplay : MonoBehaviour
{
    private Image spriteRenderer;

    void Start()
    {
        spriteRenderer = GetComponent<Image>();
        // 透明化する
        spriteRenderer.color = new Color(1f, 1f, 1f, 0.3f);
    }

    void Update()
    {

    }
}