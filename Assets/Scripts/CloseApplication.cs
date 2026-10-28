using Interfaces;
using UnityEngine;

public class CloseApplication : MonoBehaviour, IScreenButton
{
    [SerializeField] private GameObject appWindow;
    
    private SpriteRenderer _spriteRenderer;
    private Color _black;
    private Color _gray;
    
    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _black = Color.black;
        _gray = new Color32(32,32,32,255);
    }
    
    public void Clicked()
    {
        _spriteRenderer.color = _gray;
    }

    public void ReleaseClick()
    {
        _spriteRenderer.color = _black;
        appWindow.SetActive(false);
        appWindow.transform.localPosition = Vector3.zero;
    }
}
