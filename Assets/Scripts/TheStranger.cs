using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TheStranger : MonoBehaviour
{
    private enum StrangerStates
    {
        WindowHid,
        WindowShowed,
        BathroomHid,
        BathroomShowed,
        ClosetPeek
    }

    private enum StrangerLocations
    {
        Window,
        Bathroom,
        Closet
    }

    [SerializeField] private List<Transform> positionTransforms;
    [SerializeField] private AudioClip glassTap;
    [SerializeField] private float waitTime = 100f;

    private bool _readyForStateChange = true;
    private bool _playedSfx;
    
    private StrangerStates _currentState;
    private SpriteRenderer _strangerSpriteRenderer;
    private AudioSource _audioSource;
    
    private void Awake()
    {
        _currentState = StrangerStates.WindowHid;
        _strangerSpriteRenderer = GetComponentInChildren<SpriteRenderer>();
        _audioSource = GetComponent<AudioSource>();
        _strangerSpriteRenderer.enabled = false;
    }
    
    private void Update()
    {
        //Debug purposes
        /*if (Keyboard.current.lKey.wasPressedThisFrame)
        {
            ChangeState(false);
        }*/

        if (_readyForStateChange)
        {
            StartCoroutine(ChangeStateTimer());
            _readyForStateChange = false;
        }
        
        switch (_currentState)
        {
            case StrangerStates.WindowHid:
                _strangerSpriteRenderer.enabled = false;
                transform.position = positionTransforms[(int)StrangerLocations.Window].position;
                transform.rotation = positionTransforms[(int)StrangerLocations.Window].rotation;
                break;
            case StrangerStates.WindowShowed:
                _playedSfx = false;
                _strangerSpriteRenderer.enabled = true;
                _audioSource.PlayOneShot(glassTap);
                break;
            case StrangerStates.BathroomHid:
                _strangerSpriteRenderer.enabled = false;
                transform.position = positionTransforms[(int)StrangerLocations.Bathroom].position;
                transform.rotation = positionTransforms[(int)StrangerLocations.Bathroom].rotation;
                break;
            case StrangerStates.BathroomShowed:
                _playedSfx = false;
                _strangerSpriteRenderer.enabled = true;
                break;
            case StrangerStates.ClosetPeek:
                _playedSfx = false;
                transform.position = positionTransforms[(int)StrangerLocations.Closet].position;
                transform.rotation = positionTransforms[(int)StrangerLocations.Closet].rotation;
                break;
            default:
                //Maybe add something here?
                break;
        }
    }

    public void ChangeState(bool resetToStartPosition)
    {
        if (resetToStartPosition)
        {
            _currentState = StrangerStates.WindowHid;
            transform.position = positionTransforms[(int)StrangerLocations.Window].position;
        }
        else
        {
            _currentState = (StrangerStates)((int)_currentState + 1);
            if (_currentState > StrangerStates.ClosetPeek)
            {
                _currentState = StrangerStates.ClosetPeek;
                print("Jumpscare");
            }
        }
    }

    IEnumerator ChangeStateTimer()
    {
        yield return new WaitForSeconds(waitTime);
        ChangeState(false);
        _readyForStateChange = true;
    }
}
