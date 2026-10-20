using Interfaces;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerRaycast : MonoBehaviour
{
    [SerializeField] private float reachDistance = 1f;
    [SerializeField] private LayerMask layerMask;
    
    private void Update()
    {
        Vector2 curPosition = new Vector2(transform.position.x, transform.position.y);
        RaycastHit2D rayhit = Physics2D.Raycast(curPosition, transform.up, reachDistance, layerMask);
        Debug.DrawRay(new Vector2(transform.position.x, transform.position.y), transform.up, Color.red);

        if (rayhit.collider != null)
        {
            if (Keyboard.current.eKey.wasPressedThisFrame)
            {
                if (rayhit.collider.gameObject.TryGetComponent<IInteractable>(out IInteractable interactable))
                {
                    interactable.Interact();
                }
            }
        }
    }
}
