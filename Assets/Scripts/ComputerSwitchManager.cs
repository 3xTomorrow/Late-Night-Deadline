using System;
using TMPro;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class ComputerSwitchManager : MonoBehaviour
{
    [SerializeField] private CinemachineCamera computerCamera; 
    [SerializeField] private PlayerMovement player;
    [SerializeField] private PlayerRaycast playerRaycast;
    [SerializeField] private Volume computerVolume;
    [SerializeField] private TMP_Text interactableText;
    [SerializeField] private Light2D globalLight;
    
    private void Start()
    {
        Computer.OnComputerInteract += ComputerOnComputerInteract;
        PowerButton.PowerButtonPressed += PowerButtonOnPowerButtonPressed;
        
        //Checks if lights are "on" for debug purposes
        if (Mathf.Approximately(globalLight.intensity, 1))
        {
            globalLight = null;
        }
    }
    
    private void ComputerOnComputerInteract(object sender, EventArgs e)
    {
        computerCamera.enabled = true;
        computerVolume.enabled = true;
        player.enabled = false;
        playerRaycast.enabled = false;
        interactableText.enabled = false;
        if(globalLight != null)
            globalLight.intensity = 1;
    }
    
    private void PowerButtonOnPowerButtonPressed(object sender, EventArgs e)
    {
        computerCamera.enabled = false;
        computerVolume.enabled = false;
        player.enabled = true;
        playerRaycast.enabled = true;
        interactableText.enabled = true;
        if(globalLight != null)
            globalLight.intensity = 0.05f;
    }


    private void OnDestroy()
    {
        Computer.OnComputerInteract -= ComputerOnComputerInteract;
        PowerButton.PowerButtonPressed -= PowerButtonOnPowerButtonPressed;
    }
}
