using System;
using TMPro;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.Rendering;

public class ComputerSwitchManager : MonoBehaviour
{
    [SerializeField] private CinemachineCamera computerCamera; 
    [SerializeField] private PlayerMovement player;
    [SerializeField] private PlayerRaycast playerRaycast;
    [SerializeField] private Volume computerVolume;
    [SerializeField] private TMP_Text interactableText;
    
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
        interactableText.enabled = false;
    }
    
    private void PowerButtonOnPowerButtonPressed(object sender, EventArgs e)
    {
        computerCamera.enabled = false;
        computerVolume.enabled = false;
        player.enabled = true;
        playerRaycast.enabled = true;
        interactableText.enabled = true;
    }


    private void OnDestroy()
    {
        Computer.OnComputerInteract -= ComputerOnComputerInteract;
        PowerButton.PowerButtonPressed -= PowerButtonOnPowerButtonPressed;
    }
}
