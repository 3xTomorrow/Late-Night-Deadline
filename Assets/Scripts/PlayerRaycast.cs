using System.Collections;
using Interfaces;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerRaycast : MonoBehaviour
{
    [SerializeField, Min(0)] private float interactCooldown = 0.5f;
    [SerializeField, Min(0)] private float reachDistance = 1f;
    [SerializeField] private LayerMask layerMask;
    [SerializeField] private TMP_Text screenText;

    private bool _canInteract = true;
    
    private void Update()
    {
        Vector2 curPosition = new Vector2(transform.position.x, transform.position.y);
        RaycastHit2D rayhit = Physics2D.Raycast(curPosition, transform.up, reachDistance, layerMask);
        Debug.DrawRay(curPosition, transform.up * reachDistance, Color.red);

        if (rayhit.collider != null)
        {
            screenText.enabled = true;
            screenText.text = "E - " + rayhit.collider.gameObject.name;
            if (InputManager.Interacted && _canInteract)
            {
                StartCoroutine(Cooldown());
                if (rayhit.collider.gameObject.TryGetComponent<IInteractable>(out IInteractable interactable))
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

    IEnumerator Cooldown()
    {
        _canInteract = false;
        yield return new WaitForSeconds(interactCooldown);
        _canInteract = true;
    }
}
