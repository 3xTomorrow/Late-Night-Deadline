using System.Collections;
using Interfaces;
using UnityEngine;

public class Door : MonoBehaviour, IInteractable
{
    private const string DOOR_INTERACTED = "doorInteracted";
    
    private Animator _doorAnimator;
    private BoxCollider2D _doorCollider;
    
    private bool _open = false;

    private void Awake()
    {
        _doorAnimator = GetComponent<Animator>();
        _doorCollider = GetComponent<BoxCollider2D>();
    }
    
    public void Interact()
    {
        if (!_open)
        {
            StartCoroutine(OpenDoor());
            _doorAnimator.SetBool(DOOR_INTERACTED, true);
            _open = true;
        }
        else
        {
            StartCoroutine(OpenDoor());
            _doorAnimator.SetBool(DOOR_INTERACTED, false);
            _open = false;
        }
    }

    IEnumerator OpenDoor()
    {
        _doorCollider.enabled = false;
        yield return new WaitForSeconds(1f);
        _doorCollider.enabled = true;
    }
}
