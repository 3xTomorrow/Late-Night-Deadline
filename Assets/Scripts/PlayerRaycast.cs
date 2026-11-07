using System;
using Interfaces;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerRaycast : MonoBehaviour
{
    [SerializeField, Min(0)] private float reachDistance;
    [SerializeField] private LayerMask interactLayer;
    [SerializeField] private TMP_Text screenText;
    
    private void Update()
    {
        
        Vector2 curPosition = new Vector2(transform.position.x, transform.position.y);
        RaycastHit2D interactRay = Physics2D.Raycast(curPosition, transform.up, reachDistance, interactLayer);
        Debug.DrawRay(curPosition, transform.up * reachDistance, Color.red);

        if (interactRay.collider != null)
        {
            screenText.enabled = true;
            screenText.text = "E - " + interactRay.collider.gameObject.name;
            if (Keyboard.current.eKey.wasPressedThisFrame)
            {
                if (interactRay.collider.gameObject.TryGetComponent(out IInteractable interactable))
                {
                    interactable.Interact();
                }
            }
        }
        else
        {
            screenText.enabled = false;
        }
    }
}
