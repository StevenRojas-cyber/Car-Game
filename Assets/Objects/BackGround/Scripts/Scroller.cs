using UnityEngine;

public class Scroller : MonoBehaviour
{
    
    public float scrollSpeed = 0.5f;

    [SerializeField] 
    private Renderer backgroundRenderer;

    // Update is called once per frame
    void Update()
    {
        backgroundRenderer.material.mainTextureOffset += new Vector2(0, scrollSpeed * Time.deltaTime);
    }
}
