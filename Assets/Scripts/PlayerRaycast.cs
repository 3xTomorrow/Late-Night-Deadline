using Interfaces;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerRaycast : MonoBehaviour
{
    [SerializeField, Min(0)] private float reachDistance;
    [SerializeField, Min(0)] private float collisionDampening;
    [SerializeField] private LayerMask layerMask;
    [SerializeField] private TMP_Text screenText;

    public static bool CanMove;
    
    private void Update()
    {
        Vector2 curPosition = new Vector2(transform.position.x, transform.position.y);
        RaycastHit2D interactRay = Physics2D.Raycast(curPosition, transform.up, reachDistance, layerMask);
        Debug.DrawRay(curPosition, transform.up * reachDistance, Color.red);

        if (interactRay.collider != null)
        {
            screenText.enabled = true;
            screenText.text = "E - " + interactRay.collider.gameObject.name;
            if (Keyboard.current.eKey.wasPressedThisFrame)
            {
                if (interactRay.collider.gameObject.TryGetComponent<IInteractable>(out IInteractable interactable))
                {
                    interactable.Interact();
                }
            }
        }
        else
        {
            screenText.enabled = false;
        }
        
        CanMove = !Physics2D.Raycast(curPosition, transform.up, (transform.lossyScale.x / 2) + collisionDampening);
        Debug.DrawRay(curPosition, transform.up * ((transform.lossyScale.x / 2) + collisionDampening), Color.green);

        print(CanMove);
    }
}
