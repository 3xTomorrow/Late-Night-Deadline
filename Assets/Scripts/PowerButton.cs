using System;
using Interfaces;
using UnityEngine;
using UnityEngine.InputSystem;

public class PowerButton : MonoBehaviour, IScreenButton
{
    public static event EventHandler PowerButtonPressed;

    private void Update()
    {
        if (Keyboard.current.cKey.wasPressedThisFrame)
        {
            PowerButtonPressed?.Invoke(this, EventArgs.Empty);
        }
    }
    
    public void Clicked()
    {
        
    }
    
    public void ReleaseClick() { }
}
