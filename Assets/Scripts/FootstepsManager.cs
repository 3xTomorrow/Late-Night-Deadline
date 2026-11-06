using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootstepsManager : MonoBehaviour
{
    [SerializeField] private float footstepSpeed;
    [SerializeField] private List<AudioClip> footsteps;
    
    private AudioSource _audioSource;
    private bool _isPlayingFootstep;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        PlayerMovement.Moving += PlayerMovementOnMoving;
    }

    private void PlayerMovementOnMoving(object sender, EventArgs e)
    {
        AudioClip randomFootstep = footsteps[UnityEngine.Random.Range(0, footsteps.Count)];
        if (!_isPlayingFootstep)
        {
            _isPlayingFootstep = true;
            StartCoroutine(PlayFootstep(randomFootstep));
        }
            
    }

    private void OnDestroy()
    {
        PlayerMovement.Moving -= PlayerMovementOnMoving;
    }

    IEnumerator PlayFootstep(AudioClip footstep)
    {
        _audioSource.PlayOneShot(footstep);
        yield return new WaitForSeconds(footstepSpeed);
        _isPlayingFootstep = false;
    }
}
