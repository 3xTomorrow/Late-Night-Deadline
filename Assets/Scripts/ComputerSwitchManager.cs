using System;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.Rendering;

public class ComputerSwitchManager : MonoBehaviour
{
    [SerializeField] private CinemachineCamera computerCamera; 
    [SerializeField] private PlayerMovement player;
    [SerializeField] private PlayerRaycast playerRaycast;
    [SerializeField] private Volume computerVolume;
    
    private void Start()
    {
        Computer.OnComputerInteract += ComputerOnComputerInteract;
        PowerButton.PowerButtonPressed += PowerButtonOnPowerButtonPressed;
    }
    
    private void ComputerOnComputerInteract(object sender, EventArgs e)
    {
        computerCamera.enabled = true;
        computerVolume.enabled = true;
        player.enabled = false;
        playerRaycast.enabled = false;
    }
    
    private void PowerButtonOnPowerButtonPressed(object sender, EventArgs e)
    {
        computerCamera.enabled = false;
        computerVolume.enabled = false;
        player.enabled = true;
        playerRaycast.enabled = true;
    }


    private void OnDestroy()
    {
        Computer.OnComputerInteract -= ComputerOnComputerInteract;
        PowerButton.PowerButtonPressed -= PowerButtonOnPowerButtonPressed;
    }
}
