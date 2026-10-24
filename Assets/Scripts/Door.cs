using System;
using System.Collections;
using Interfaces;
using UnityEngine;

public class Door : MonoBehaviour, IInteractable
{
    [SerializeField] private float doorOpenSpeed = 2f;
    [SerializeField] private BoxCollider2D doorCollider;
    
    private Animator _doorAnimator;

    private Quaternion _closedRotation;
    private Quaternion _openRotation;
    
    private bool _open;
    
    private void Awake()
    {
        _closedRotation = transform.rotation;
        _openRotation = _closedRotation * Quaternion.Euler(0f,0f,90);
        _doorAnimator = GetComponent<Animator>();
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
        doorCollider.enabled = false;
        yield return new WaitForSeconds(1f / doorOpenSpeed);
        doorCollider.enabled = true;
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
