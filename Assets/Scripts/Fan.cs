using System;
using UnityEngine;

public class Fan : MonoBehaviour
{
    private Animator _animator;
    private AudioSource _audioSource;
    
    private bool _isOn = true;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _audioSource = GetComponent<AudioSource>();
    }
    
    private void Start()
    {
        Breaker.BreakerShutoff += BreakerOnBreakerShutoff;
        Breaker.BreakerTurnedOn += BreakerOnBreakerTurnedOn;
    }


    private void TurnOff()
    {
        if (_isOn)
        {
            _audioSource.Pause();
            _animator.speed = 0f;
            _isOn = false;
        }
    }

    private void TurnOn()
    {
        if (!_isOn)
        {
            _audioSource.Play();
            _animator.speed = 1f;
            _isOn = true;
        }
    }

    private void BreakerOnBreakerShutoff(object sender, EventArgs e) => TurnOff();
    private void BreakerOnBreakerTurnedOn(object sender, EventArgs e) => TurnOn();
    
    private void OnDestroy()
    {
        Breaker.BreakerShutoff -= BreakerOnBreakerShutoff;
        Breaker.BreakerTurnedOn -= BreakerOnBreakerTurnedOn;
    }
}
