using System;
using System.Collections;
using Interfaces;
using UnityEngine;

public class Door : MonoBehaviour, IInteractable
{
    [SerializeField] private float doorOpenSpeed = 2f;
    
    private Animator _doorAnimator;
    private BoxCollider2D _doorCollider;

    private Quaternion _closedRotation;
    private Quaternion _openRotation;
    
    private bool _open;
    
    private void Awake()
    {
        _closedRotation = transform.rotation;
        _openRotation = _closedRotation * Quaternion.Euler(0f,0f,90);
        _doorAnimator = GetComponent<Animator>();
        _doorCollider = GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        //transform.rotation = Quaternion.RotateTowards(transform.rotation, _openRotation, 100f * Time.deltaTime);
    }
    
    public void Interact()
    {
        if (!_open)
        {
            StartCoroutine(OpenDoor());
            StartCoroutine(RotateDoor(_closedRotation, _openRotation, 1/doorOpenSpeed));
            _open = true;
        }
        else
        {
            StartCoroutine(OpenDoor());
            StartCoroutine(RotateDoor(_openRotation, _closedRotation, 1/doorOpenSpeed));
            _open = false;
        }
    }

    IEnumerator OpenDoor()
    {
        _doorCollider.enabled = false;
        yield return new WaitForSeconds(1f / doorOpenSpeed);
        _doorCollider.enabled = true;
    }

    //Takes advantage of Coroutines being called over time
    //and uses a difference in time to rotate the door
    IEnumerator RotateDoor(Quaternion startRot, Quaternion endRot, float endTime)
    {
        float startTime = Time.time;
        while (Time.time < startTime + endTime)
        {
            //100f is just a multiplier for speeding up the rotation
            transform.rotation = Quaternion.RotateTowards(startRot, endRot, 100f * (Time.time - startTime) / endTime);
            yield return null;
        }
        transform.rotation = endRot;
    }
}
