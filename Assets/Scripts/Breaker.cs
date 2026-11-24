using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Interfaces;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering.Universal;

public class Breaker : MonoBehaviour, IInteractable
{
    [Header("Breaker Settings")]
    [SerializeField, Min(0)] private int breakerTimer = 120;
    [SerializeField, Range(0, 100)] private int shutoffChance = 20; 
    
    [Header("Lights")]
    [SerializeField] private List<Light2D> exemptLights;
    private HashSet<Light2D> _exemptLightsSet;

    [Header("Sound")] 
    [SerializeField] private AudioClip shutoffSound;
    [SerializeField] private AudioClip turnOnSound;
    private AudioSource _audioSource;
    
    private List<Light2D> _allLights;


    public static event EventHandler BreakerShutoff;
    public static event EventHandler BreakerTurnedOn;

    private bool _isOn = true;

    private System.Random _rand;

    private void Awake()
    {
        _rand = new System.Random();
        _exemptLightsSet = new HashSet<Light2D>(exemptLights);
        _audioSource = GetComponent<AudioSource>();
    }
    
    private void Start()
    {
        _allLights = FindObjectsByType<Light2D>().ToList();
        for (int i = 0; i < _allLights.Count; i++)
        {
            Light2D light = _allLights[i];
            if (_exemptLightsSet.Contains(light))
            {
                _allLights.Remove(light);
                i--;
            }
        }
        
        StartCoroutine(Timer());
    }

    // private void Update()
    // {
    //     if (Keyboard.current.mKey.wasPressedThisFrame)
    //     {
    //         TurnOffAllLights();
    //     }
    //
    //     if (Keyboard.current.nKey.wasPressedThisFrame)
    //     {
    //         TurnOnAllLights();
    //     }
    // }
    
    public void Interact()
    {
        if (!_isOn)
        {
            TurnOnAllLights();
        }
    }

    private void BreakerEvent()
    {
        int randNum = _rand.Next(1,101);
        if(_isOn && randNum <= shutoffChance)
        {
            TurnOffAllLights();
        }
        StartCoroutine(Timer());
    }

    private void TurnOnAllLights()
    {
        _isOn = true;
        _audioSource.PlayOneShot(turnOnSound);
        BreakerTurnedOn?.Invoke(this, EventArgs.Empty);
        _allLights.ForEach(light => light.enabled = true);
    }

    private void TurnOffAllLights()
    {
        _isOn = false;
        _audioSource.PlayOneShot(shutoffSound);
        BreakerShutoff?.Invoke(this, EventArgs.Empty);
        _allLights.ForEach(light => light.enabled = false);
    }

    IEnumerator Timer()
    {
        yield return new WaitForSeconds(breakerTimer);
        BreakerEvent();
    }
}
