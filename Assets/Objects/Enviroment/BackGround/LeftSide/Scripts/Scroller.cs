using UnityEngine;

public class Scroller : MonoBehaviour
{
    [Header("Background Attributes")]
    [SerializeField] private float scrollSpeed = 0.5f;
    [SerializeField] private Renderer backgroundRenderer;

    public float GetScrollSpeed() 
    { 
        return scrollSpeed; 
    }

    void Update()
    {
        backgroundRenderer.material.mainTextureOffset += new Vector2(0, scrollSpeed * Time.deltaTime);
    }
}
