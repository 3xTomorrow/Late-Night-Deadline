using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class AreaCover : MonoBehaviour
{
    private const string PLAYER = "Player";

    [SerializeField] private LayerMask playerMask;
    
    private float darkeningSpeed = 8f;
    private float lighteningSpeed = 4f;
    
    private SpriteRenderer _spriteRenderer;
    private bool _inZone;
    
    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        Vector2 topLeftPoint = new Vector2(transform.position.x - transform.lossyScale.x/2, transform.position.y + transform.lossyScale.y/2);
        Vector2 bottomRightPoint = new Vector2(transform.position.x + transform.lossyScale.x/2, transform.position.y - transform.lossyScale.y/2);
        
        Collider2D collider = Physics2D.OverlapArea(topLeftPoint, bottomRightPoint, playerMask);
        if (collider != null)
        {
            print(collider.name);
            _inZone = true;
        }
        else
        {
            _inZone = false;
        }
        
        if(_inZone)
            _spriteRenderer.color = Color.Lerp(_spriteRenderer.color, new Color32(0,0,0,0), Time.deltaTime * lighteningSpeed );
        else if(!_inZone)
            _spriteRenderer.color = Color.Lerp(_spriteRenderer.color, new Color32(0,0,0,255), Time.deltaTime * darkeningSpeed);
    }
}
