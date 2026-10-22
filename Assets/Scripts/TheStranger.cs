using System;
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

    private StrangerStates _currentState;
    private SpriteRenderer _strangerSpriteRenderer;
    
    private void Awake()
    {
        _currentState = StrangerStates.WindowHid;
        _strangerSpriteRenderer = GetComponentInChildren<SpriteRenderer>();
        _strangerSpriteRenderer.enabled = false;
    }
    
    private void Update()
    {
        //Debug purposes
        /*if (Keyboard.current.lKey.wasPressedThisFrame)
        {
            ChangeState(false);
        }*/
        
        switch (_currentState)
        {
            case StrangerStates.WindowHid:
                _strangerSpriteRenderer.enabled = false;
                transform.position = positionTransforms[(int)StrangerLocations.Window].position;
                transform.rotation = positionTransforms[(int)StrangerLocations.Window].rotation;
                break;
            case StrangerStates.WindowShowed:
                _strangerSpriteRenderer.enabled = true;
                break;
            case StrangerStates.BathroomHid:
                _strangerSpriteRenderer.enabled = false;
                transform.position = positionTransforms[(int)StrangerLocations.Bathroom].position;
                transform.rotation = positionTransforms[(int)StrangerLocations.Bathroom].rotation;
                break;
            case StrangerStates.BathroomShowed:
                _strangerSpriteRenderer.enabled = true;
                break;
            case StrangerStates.ClosetPeek:
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
}
