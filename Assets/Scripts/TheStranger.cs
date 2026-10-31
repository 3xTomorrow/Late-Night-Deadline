using System;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using Unity.Mathematics;
using UnityEditor.Toolbars;
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
    
    [SerializeField] private float waitTime = 100f;
    [SerializeField] private float shineThreshold = 3f;
    
    [Header("Audios")]
    [SerializeField] private AudioClip glassTap;
    [SerializeField] private AudioClip doorCreak;
    [SerializeField] private AudioClip closetShuffle;
    
    private bool _readyForStateChange = true;
    private bool _beingShined;
    private float _shineValue;
    
    private StrangerStates _currentState;
    private SpriteRenderer _strangerSpriteRenderer;
    private BoxCollider2D _boxCollider;
    private AudioSource _audioSource;
    
    private void Awake()
    {
        _currentState = StrangerStates.WindowHid;
        _strangerSpriteRenderer = GetComponentInChildren<SpriteRenderer>();
        _boxCollider = GetComponent<BoxCollider2D>();
        _audioSource = GetComponent<AudioSource>();
        _strangerSpriteRenderer.enabled = false;
    }
    
    private void Update()
    {
        //Debug purposes
        if (Keyboard.current.lKey.wasPressedThisFrame)
        {
            ChangeState(false);
        }
        
        //Handles how The Stranger changes states based on time
        if (_readyForStateChange)
        {
            StartCoroutine(ChangeStateTimer());
            _readyForStateChange = false;
        }
        
        //Decreases the shine value if there is no light on The Stranger
        if (!_beingShined && _shineValue > 0f)
        {
            _shineValue -= Time.deltaTime;
            if(_shineValue <= 0f) { _shineValue = 0f; }
        }
        
        print(_shineValue);
        if (_shineValue > shineThreshold)
        {
            ChangeState(true);
            _shineValue = 0f;
        }
        
        
        //Changes the variables of The Stranger based on his state
        switch (_currentState)
        {
            case StrangerStates.WindowHid:
                _strangerSpriteRenderer.enabled = false;
                _boxCollider.enabled = false;
                transform.position = positionTransforms[(int)StrangerLocations.Window].position;
                transform.rotation = positionTransforms[(int)StrangerLocations.Window].rotation;
                break;
            case StrangerStates.WindowShowed:
                _strangerSpriteRenderer.enabled = true;
                _boxCollider.enabled = true;
                break;
            case StrangerStates.BathroomHid:
                _strangerSpriteRenderer.enabled = false;
                _boxCollider.enabled = false;
                transform.position = positionTransforms[(int)StrangerLocations.Bathroom].position;
                transform.rotation = positionTransforms[(int)StrangerLocations.Bathroom].rotation;
                break;
            case StrangerStates.BathroomShowed:
                _strangerSpriteRenderer.enabled = true;
                _boxCollider.enabled = true;
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
    
    /// <summary>
    /// Changes the state of The Stranger
    /// Either "increases" the state or sets him back to the beginning
    /// </summary>
    /// <param name="resetToStartPosition">Whether you want to send The Stranger back to his original position</param>
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
            if (_currentState == StrangerStates.WindowShowed)
            {
                _audioSource.PlayOneShot(glassTap);
            }
            if (_currentState == StrangerStates.BathroomShowed)
            {
                _audioSource.PlayOneShot(doorCreak);
            }
            if (_currentState == StrangerStates.ClosetPeek)
            {
                _audioSource.PlayOneShot(closetShuffle);
            }
            if (_currentState > StrangerStates.ClosetPeek)
            {
                _currentState = StrangerStates.ClosetPeek;
                print("Jumpscare");
            }
        }
    }

    public void Shine()
    {
        _beingShined = true;
        _shineValue += Time.deltaTime;
    }
    public void Darken() { _beingShined = false; }

    IEnumerator ChangeStateTimer()
    {
        yield return new WaitForSeconds(waitTime);
        ChangeState(false);
        _readyForStateChange = true;
    }
}
