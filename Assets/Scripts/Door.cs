using System.Collections;
using Interfaces;
using UnityEngine;

public class Door : MonoBehaviour, IInteractable
{
    private const string DOOR_INTERACTED = "doorInteracted";
    
    private Animator _doorAnimator;
    private BoxCollider2D _doorCollider;
    
    private bool _open = false;
    private Quaternion closedRotation = Quaternion.Euler(0f, 0f, 0f);
    private Quaternion openRotation = Quaternion.Euler(0f, 0f, 90f);
    private float doorOpenSpeed = 5f;
    
    private void Awake()
    {
        _doorAnimator = GetComponent<Animator>();
        _doorCollider = GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        //Quaternion.Slerp(transform.rotation,openRotation,doorOpenSpeed * Time.deltaTime);
    }
    
    public void Interact()
    {
        if (!_open)
        {
            print("opening door");
            //StartCoroutine(OpenDoor());
            //_doorAnimator.SetBool(DOOR_INTERACTED, true);
            transform.rotation = Quaternion.Slerp(transform.rotation,openRotation,doorOpenSpeed * Time.deltaTime);
            _open = true;
        }
        else
        {
            print("closing door");
            //StartCoroutine(OpenDoor());
            //_doorAnimator.SetBool(DOOR_INTERACTED, false);
            transform.rotation = Quaternion.Slerp(transform.rotation,closedRotation,doorOpenSpeed * Time.deltaTime);
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
