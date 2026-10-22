using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

public class ComputerSwitchManager : MonoBehaviour
{
    [SerializeField] private PlayerRaycast playerRaycast;
    [SerializeField] private Light2D globalLight;

    [SerializeField] private List<Behaviour> computerComponents;
    [SerializeField] private List<Behaviour> playerComponents;
    
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
        foreach (Behaviour computerComponent in computerComponents)
        {
            computerComponent.enabled = true;
        }
        foreach (Behaviour playerComponent in playerComponents)
        {
            if (playerComponent is Slider)
            {
                playerComponent.gameObject.SetActive(false);
            } else
                playerComponent.enabled = false;
        }
        playerRaycast.enabled = false;
        if(globalLight != null)
            globalLight.intensity = 1;
    }
    
    private void PowerButtonOnPowerButtonPressed(object sender, EventArgs e)
    {
        foreach (Behaviour computerComponent in computerComponents)
        {
            computerComponent.enabled = false;
        }
        foreach (Behaviour playerComponent in playerComponents)
        {
            if (playerComponent is Slider)
            {
                playerComponent.gameObject.SetActive(true);
            } else
                playerComponent.enabled = true;
        }
        playerRaycast.enabled = true;
        if(globalLight != null)
            globalLight.intensity = 0.05f;
    }


    private void OnDestroy()
    {
        Computer.OnComputerInteract -= ComputerOnComputerInteract;
        PowerButton.PowerButtonPressed -= PowerButtonOnPowerButtonPressed;
    }
}
