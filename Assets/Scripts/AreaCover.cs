using System;
using UnityEngine;

public class AreaCover : MonoBehaviour
{
    private const string Player = "Player";
    
    private SpriteRenderer _spriteRenderer;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    //Make a delay for the darkness to appear
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == Player)
            _spriteRenderer.enabled = false;
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag == Player)
            _spriteRenderer.enabled = false;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == Player)
            _spriteRenderer.enabled = true;
    }
    
    
}
