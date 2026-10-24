using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class AreaCover : MonoBehaviour
{
    [SerializeField] private float darkeningSpeed = 2f;
    
    private SpriteRenderer _spriteRenderer;
    private bool _inZone;
    
    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if(_inZone)
            _spriteRenderer.color = Color.Lerp(_spriteRenderer.color, new Color32(0,0,0,0), Time.deltaTime * darkeningSpeed);
        else if(!_inZone)
            _spriteRenderer.color = Color.Lerp(_spriteRenderer.color, new Color32(0,0,0,255), Time.deltaTime * darkeningSpeed);
    }

    private void OnTriggerStay2D(Collider2D other) { _inZone = true; }

    private void OnTriggerExit2D(Collider2D other) { _inZone = false; }
}
